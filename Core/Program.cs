﻿using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Lexer;
using Core.Objects;
using Core.Exceptions;
using AbstractSyntaxTree.Objects;
using CodeGeneration;
using Lexer.Objects;
using Contextual_analysis;

namespace Core
{
    /// <summary>
    /// The core of the compiler.
    /// This is where every step of the compiler is called from.
    /// </summary>
    public class Program
    {
        static VerbosePrinter verbosePrinter;
        static CommandLineOptions options;

        /// <summary>
        /// The entry point of the compiler.
        /// Here the user must provide a set of compiler flags, in order for the compiler to produce a satisfying product.
        /// The user must provide a filepath to the file they wish to compile. Otherwise the compiler will halt and exit with an exit code of 1.
        /// Compiler flags can be provided to activate additional functionality.
        /// **Compiler flags**
        /// `-d` or `--dryrun` Runs the compiler without producing an output.
        /// `-o` or `--output` Tells the compiler not to write to the Arduino, and instead produce a file.
        /// `-v` or `--verbose` Prints additional information when compiling.
        /// `-b` or `--boilerplate` Generates a boilerplate file for your code.
        /// `-l` or `--logfile` (Must be followed by a file path) Prints additional information when compiling.
        /// `-p` or `--port` (Must be followed by a port number) Specifies the port to upload to.
        /// `-a` or `--arduino` (Must be followed by an Arduino model) Specifies the arduino model you're uploading to. (Default: UNO)
        /// `-pr` or `--proc` (Must be followed by a valid processor) Specifies the arduino processor you're uploading to. (Default: atmega328p)
        /// `-pp` or `--prettyprinter` Print the abstract syntax tree.
        /// **Compiler exit code**
        /// `0` Compilation finished with no errors.
        /// `1` A file path was not provided to the compiler for compilation.
        /// `20` The file provided was not encoded as a UTF-8 file.
        /// `5` An error was encountered while scanning the input program. This is usually caused by an unclosed string, comment, or parenthesis.
        /// `4` An error was encountered in the parser. This is usually due to an invalidly structured program.
        /// `3` An error was encountered in the type checker. This happens when two types are mismatched, either on assignment or within an expression. Furthermore, this can be caused by not defining a called function, or a function being defined multiple times.
        /// `2` This error is encountered when the code generator can not find the output file for the intermediate representation code.
        /// `23` This error is encountered when the dryrun flag is invoked, but the compiler can not find the output file for the intermediate representation code.
        /// </summary>
        /// <param name="args">The arguments passed to the compiler from the terminal.</param>
        /// <returns>An exit code representing the state the compiler exited in.</returns>
        public static int Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            options = ParseOptions(args);
            verbosePrinter = new VerbosePrinter(options);
            if (options?.InputFile == null)
            {
                Help();
                verbosePrinter.Error("Input file was not specified.");
                return 1;
            }
            if (options.Boilerpate)
            {
                using (StreamReader reader = new StreamReader(AppContext.BaseDirectory + "/boilerplate"))
                {
                    verbosePrinter.Info("Reading boilerplate template");
                    string content = reader.ReadToEnd();
                    using (StreamWriter writer = new StreamWriter(options.InputFile))
                    {
                        verbosePrinter.Info("Writing boilerplate template to file");
                        writer.Write(content);
                    }
                }
                verbosePrinter.Info("Done.");
                return 0;
            }

            verbosePrinter.Info("Initialising file.");
            using (StreamReader reader = new StreamReader(options.InputFile))
            {
                try
                {
                    verbosePrinter.Info("Checking encoding...");
                    FileChecker.CheckEncoding(reader);
                    verbosePrinter.Info($" Encoding was {reader.CurrentEncoding}.");
                }
                catch (EncodingNotSupportedException e)
                {
                    verbosePrinter.Error("File not encoded correctly.");
                    Console.Error.WriteLine(e.Message);
                    return 20;
                }
                Tokeniser tokeniser = new Tokeniser(reader);
                verbosePrinter.Info("Generating tokens...");
                tokeniser.GenerateTokens();
                if (Tokeniser.HasError)
                {
                    verbosePrinter.Error("Encountered syntax errors. Stopping.");
                    return 5;
                }
                verbosePrinter.Info($" Generated {tokeniser.Tokens.Count} tokens.");
                if (options.Verbose)
                {
                    foreach (var token in tokeniser.Tokens)
                    {
                        verbosePrinter.Info(token);
                    }
                }
                verbosePrinter.Info("Generating parse table");
                List<ScannerToken> tokens = tokeniser.Tokens.ToList();
                Parser.Parser parser = new Parser.Parser(tokens);
                string debugMessage = "";
                parser.Parse(out debugMessage);
                verbosePrinter.Info(debugMessage);
                if (Parser.Parser.HasError)
                {
                    verbosePrinter.Error("Encountered an error state in the parser. Stopping.");
                    return 4;
                }
                if (options.PrettyPrinter)
                {
                    parser.Root.Accept(new PrettyPrinter());
                }
                parser.Root.Accept(new TypeChecker());
                if (TypeChecker.HasError)
                {
                    verbosePrinter.Error("Encountered an error in the type checker. Stopping.");
                    return 3;
                }

                string path = AppContext.BaseDirectory;

                try
                {
                    if (File.Exists($"{path}PrecompiledBinaries/tmp/sketch/output.cpp"))
                        File.Delete($"{path}PrecompiledBinaries/tmp/sketch/output.cpp");
                    parser.Root.Accept(new CodeGenerationVisitor($"{path}PrecompiledBinaries/tmp/sketch/output.cpp", GetPWMSet()));
                }
                catch (FileNotFoundException e)
                {
                    verbosePrinter.Error("Encountered an error in code generation. Stopping.");
                    Console.Error.WriteLine(e.Message);
                    return 2;
                }
                if (CodeGenerationVisitor.HasError) return 10;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                {
                    Console.WriteLine("We're on Linux!");
                    if (options.Port == "COM0")
                    {
                        Console.Error.WriteLine($"Error: No Port Provided. The compiler will try to guess the port.");
                        string[] devices = "ls /dev/tty*".Bash().Split("\n");
                        if (devices.Any(str => str.Contains("ACM")))
                            options.Port = devices.First(str => str.Contains("ACM"));
                    }
                    path = path.Replace(" ", "\\ ");
                    $"chmod -R a+x {path}".Bash();
                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-g++ {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}PrecompiledBinaries/arduino/avr/cores/arduino -I{path}PrecompiledBinaries/hardware/arduino/avr/variants/standard {path}PrecompiledBinaries/tmp/sketch/output.cpp -o /dev/null".Bash();

                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-g++ {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}PrecompiledBinaries/arduino/avr/cores/arduino -I{path}PrecompiledBinaries/hardware/arduino/avr/variants/standard {path}PrecompiledBinaries/tmp/sketch/output.cpp -o {path}PrecompiledBinaries/tmp/Preproc/ctags_target_for_gcc_minus_e.cpp".Bash();

                    $"{path}PrecompiledBinaries/tools-builder/ctags/5.8-arduino11/ctags -u --language-force=c++ -f - --c++-kinds=svpf --fields=KSTtzns --line-directives {path}PrecompiledBinaries/tmp/Preproc/ctags_target_for_gcc_minus_e.cpp".Bash();

                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-g++ {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}PrecompiledBinaries/arduino/avr/cores/arduino -I{path}PrecompiledBinaries/hardware/arduino/avr/variants/standard {path}PrecompiledBinaries/tmp/sketch/output.cpp -o {path}PrecompiledBinaries/tmp/sketch/output.cpp.o".Bash();

                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-gcc {(options.Verbose ? "-v" : "")} -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu={options.Processor} -o {path}PrecompiledBinaries/tmp/output.cpp.elf {path}PrecompiledBinaries/tmp/sketch/output.cpp.o {path}PrecompiledBinaries/randomAFile.a -L{path}PrecompiledBinaries/tmp -lm".Bash();

                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-objcopy {(options.Verbose ? "-v" : "")} -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 {path}PrecompiledBinaries/tmp/output.cpp.elf {path}PrecompiledBinaries/tmp/output.cpp.eep".Bash();

                    $"{path}PrecompiledBinaries/hardware/tools/avr/bin/avr-objcopy {(options.Verbose ? "-v" : "")} -O ihex -R .eeprom {path}PrecompiledBinaries/tmp/output.cpp.elf {path}PrecompiledBinaries/tmp/output.cpp.hex".Bash();

                    if (!options.OutputFile || options.DryRun) $"{path}PrecompiledBinaries/unix/avrdude {(options.Verbose ? "-v" : "")} -C{path}PrecompiledBinaries/etc/avrdude.conf -v -p{options.Processor} -carduino -P{options.Port} -b115200 -D -Uflash:w:{path}PrecompiledBinaries/tmp/output.cpp.hex:i".Bash();

                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.WriteLine("We're on Windows!");
                    if (options.Port == "COM0")
                    {
                        Console.Error.WriteLine($"Error: No Port Provided. The compiler will try to find one available");
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "powershell";
                        psi.UseShellExecute = false;
                        psi.RedirectStandardOutput = true;

                        psi.Arguments = "[System.IO.Ports.SerialPort]::getportnames()";
                        Process p = Process.Start(psi);
                        string[] strOutput = p.StandardOutput.ReadToEnd().Split("\n");
                        p.WaitForExit();
                        options.Port = strOutput[1];
                    }
                    path = path.Replace('/', '\\');


                    $"\"{path}PrecompiledBinaries\\win\\avr-g++\" {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o nul".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\avr-g++\" {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o \"{path}PrecompiledBinaries\\tmp\\Preproc\\ctags_target_for_gcc_minus_e.cpp\"".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\ctags.exe\" {(options.Verbose ? "-v" : "")} -u --language-force=c++ -f - --c++-kinds=svpf --fields=KSTtzns --line-directives \"{path}PrecompiledBinaries\\tmp\\Preproc\\ctags_target_for_gcc_minus_e.cpp\"".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\avr-g++.exe\" {(options.Verbose ? "-v" : "")} -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o \"{path}PrecompiledBinaries\\tmp\\sketch\\output.cpp.o\"".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\avr-gcc.exe\" {(options.Verbose ? "-v" : "")} -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu={options.Processor} -o \"{path}PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}PrecompiledBinaries\\tmp\\sketch\\output.cpp.o\" \"{path}PrecompiledBinaries\\randomAFile.a\" \"-L{path}PrecompiledBinaries\\tmp\" -lm".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\avr-objcopy.exe\" {(options.Verbose ? "-v" : "")} -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 \"{path}PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}PrecompiledBinaries\\tmp\\output.cpp.eep\"".Cmd();

                    $"\"{path}PrecompiledBinaries\\win\\avr-objcopy.exe\" {(options.Verbose ? "-v" : "")} -O ihex -R .eeprom \"{path}PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}PrecompiledBinaries\\tmp\\output.cpp.hex\"".Cmd();

                    if (!options.OutputFile || options.DryRun) $"\"{path}PrecompiledBinaries\\win\\avrdude\" {(options.Verbose ? "-v" : "")} \"-C{path}PrecompiledBinaries\\etc\\avrdude.conf\" -v -p{options.Processor} -carduino -P{options.Port} -b115200 -D \"-Uflash:w:{path}PrecompiledBinaries\\tmp\\output.cpp.hex:i\"".Cmd();
                }
                else
                {
                    verbosePrinter.Error("OS not supported! Stopping.");
                }
                if (options.DryRun)
                {
                    try
                    {
                        File.Delete($"{path}PrecompiledBinaries/tmp/sketch/output.cpp");
                        return 0;
                    }
                    catch (FileNotFoundException e)
                    {
                        verbosePrinter.Error("Encountered an error in code generation. Stopping.");
                        Console.Error.WriteLine(e.Message);
                        return 23;
                    }
                }

                //TODO further compilation
                //"C:\\Program Files (x86)\Arduino\hardware\tools\avr/bin/avr-g++" -c -g -Os -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu=atmega328p -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_UNO -DARDUINO_ARCH_AVR "-IC:\\Program Files (x86)\Arduino\hardware\arduino\avr\cores\arduino" "-IC:\\Program Files (x86)\Arduino\hardware\arduino\avr\variants\standard" "C:\Users\bruger\Documents\aau\4semester\\P4\github\\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp" -o "C:\Users\bruger\Documents\aau\4semester\\P4\github\\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp.o"
            }

            timer.Stop();
            verbosePrinter.Info($"\nCompilation took {timer.Elapsed.TotalSeconds} seconds.");
            return 0;
        }
        /// <summary>
        /// Prints the help list to the terminal.
        /// This is automatically called, if no file path is provided when the compiler is invoked.
        /// </summary>
        public static void Help()
        {
            System.Console.WriteLine("---===### PIC, PseudoIno Compiler ###===---");
            System.Console.WriteLine("-------------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("    pic <source code> [options]");
            System.Console.WriteLine("    <source code>           The path to the code, that is to be compiled.");
            System.Console.WriteLine("    [options]               See Optional Paramters");
            System.Console.WriteLine("");
            System.Console.WriteLine("Optional Parameters:");
            System.Console.WriteLine("    -d  | --DryRun          Runs the compiler without producing an output.");
            System.Console.WriteLine("    -o  | --Output          Tells the compiler not to write to the Arduino, and instead produce a file.");
            System.Console.WriteLine("    -v  | --Verbose         Prints additional information when compiling.");
            System.Console.WriteLine("    -b  | --boilerplate     Generates a boilerplate file for your code.");
            System.Console.WriteLine("    -l  | --logfile <path>  Prints additional information when compiling.");
            System.Console.WriteLine("    -p  | --port <number>   Specifies the port to upload to.");
            System.Console.WriteLine("    -a  | --arduino <model> Specifies the arduino model you're uploading to. (Default: UNO)");
            System.Console.WriteLine("    -pr | --proc <model>    Specifies the arduino processor you're uploading to. (Default: atmega328p)");
            System.Console.WriteLine("    -pp | --prettyprinter   Print the abstract syntax tree.");
            System.Console.WriteLine("");
        }

        /// <summary>
        /// This function will parse the flags passed to the compiler.
        /// This is done using a switch case, which will then set the correct flags and values in the compiler options.
        /// </summary>
        /// <param name="args">The arguments provided to the compiler on invokation</param>
        /// <returns>A CommandLineOptions object, containing the options specified by the user.</returns>
        public static CommandLineOptions ParseOptions(string[] args)
        {
            CommandLineOptions options = new CommandLineOptions();
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLowerInvariant())
                {
                    case "-d":
                    case "--dryrun":
                        options.DryRun = true;
                        break;
                    case "-o":
                    case "--output":
                        options.OutputFile = true;
                        break;
                    case "-v":
                    case "--verbose":
                        options.Verbose = true;
                        break;
                    case "-b":
                    case "--boilerplate":
                        options.Boilerpate = true;
                        break;
                    case "-pp":
                    case "--prettyprinter":
                        options.PrettyPrinter = true;
                        break;
                    case "-l":
                    case "--logfile":
                        if (args.Length >= i + 2 && !args[i + 1].StartsWith('-'))
                        {
                            ++i;
                            options.LogFile = args[i];
                        }
                        else
                        {
                            Console.Error.WriteLineAsync($"Error: Log file not provided");
                        }
                        break;
                    case "-pr":
                    case "--proc":
                        if (args.Length >= i + 2 && !args[i + 1].StartsWith('-'))
                        {
                            ++i;
                            switch (args[i].ToLower())
                            {
                                case "atmega32u4":
                                case "atmega328p":
                                case "atmega2560":
                                case "atmega1280":
                                case "atmega168":
                                case "attiny85":
                                    options.Processor = args[i];
                                    break;
                                default:
                                    {
                                        Console.Error.WriteLine("Invalid processor specified. Attempting to determine processor.");
                                        switch (options.Arduino.ToUpper())
                                        {
                                            case "YUN":
                                            case "YUNMINI":
                                            case "LEONARDO":
                                            case "LEONARDO_ETH":
                                            case "MICRO":
                                            case "ESPLORA":
                                            case "LILYPAD_USB":
                                            case "ROBOT_CONTROL":
                                            case "ROBOT_MOTOR":
                                            case "CIRCUITPLAY":
                                            case "INDUSTRIAL101":
                                            case "LININO_ONE":
                                                options.Processor = "atmega32u4";
                                                break;
                                            case "UNO":
                                            case "UNO_WIFI_DEV_ED":
                                            case "NANO":
                                            case "DIECIMILA":
                                            case "DUEMILANOVE":
                                            case "MINI":
                                            case "FIO":
                                            case "BT":
                                            case "ETHERNET":
                                            case "LILYPAD":
                                            case "PRO":
                                                options.Processor = "atmega328p";
                                                break;
                                            case "ADK":
                                            case "MEGA2560":
                                                options.Processor = "atmega2560";
                                                break;
                                            case "MEGA":
                                                options.Processor = "atmega1280";
                                                break;
                                            case "NG":
                                                options.Processor = "atmega168";
                                                break;
                                            case "GEMMA":
                                                options.Processor = "attiny85";
                                                break;
                                            default:
                                                Console.Error.WriteLine("Could not determine model. Exiting!");
                                                return null;
                                        }
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Console.Error.WriteLineAsync($"Error: Processor not provided. Defaulting");
                        }
                        break;
                    case "-a":
                    case "--arduino":
                        if (args.Length >= i + 2 && !args[i + 1].StartsWith('-'))
                        {
                            ++i;
                            options.Arduino = args[i].ToUpper();
                            switch (args[i].ToUpper())
                            {
                                case "YUN":
                                case "YUNMINI":
                                case "LEONARDO":
                                case "LEONARDO_ETH":
                                case "MICRO":
                                case "ESPLORA":
                                case "LILYPAD_USB":
                                case "ROBOT_CONTROL":
                                case "ROBOT_MOTOR":
                                case "CIRCUITPLAY":
                                case "INDUSTRIAL101":
                                case "LININO_ONE":
                                    options.Processor = "atmega32u4";
                                    break;
                                case "UNO":
                                case "UNO_WIFI_DEV_ED":
                                case "NANO":
                                case "DIECIMILA":
                                case "DUEMILANOVE":
                                case "MINI":
                                case "FIO":
                                case "BT":
                                case "ETHERNET":
                                case "LILYPAD":
                                case "PRO":
                                    options.Processor = "atmega328p";
                                    break;
                                case "ADK":
                                case "MEGA2560":
                                    options.Processor = "atmega2560";
                                    break;
                                case "MEGA":
                                    options.Processor = "atmega1280";
                                    break;
                                case "NG":
                                    options.Processor = "atmega168";
                                    break;
                                case "GEMMA":
                                    options.Processor = "attiny85";
                                    break;
                                default:
                                    {
                                        Console.Error.WriteLine("Model not supported. Defaulting to UNO");
                                        options.Arduino = "UNO";
                                        options.Processor = "atmega328p";
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine($"Error: Model was not defined. Defaulting to UNO");
                        }
                        break;
                    case "-p":
                    case "--port":
                        if (args.Length >= i + 2 && !args[i + 1].StartsWith('-'))
                        {
                            i++;

                            options.Port = args[i];

                        }
                        else
                        {
                            Console.Error.WriteLine($"Error: No Port Provided. The compiler will try to guess the port.");
                            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            {
                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.FileName = "powershell";
                                psi.UseShellExecute = false;
                                psi.RedirectStandardOutput = true;

                                psi.Arguments = "[System.IO.Ports.SerialPort]::getportnames()";
                                Process p = Process.Start(psi);
                                string[] strOutput = p.StandardOutput.ReadToEnd().Split("\n");
                                p.WaitForExit();
                                if (strOutput.Length > 1)
                                    options.Port = strOutput[1];
                            }
                            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                            {
                                Console.Error.WriteLine($"Error: No Port Provided. The compiler will try to guess the port.");
                                string[] devices = "ls /dev/tty*".Bash().Split("\n");
                                if (devices.Any(str => str.Contains("ACM")))
                                    options.Port = devices.Last(str => str.Contains("ACM"));
                            }
                        }
                        break;
                    default:
                        if (args[i].StartsWith('-'))
                        {
                            Console.Error.WriteLine($"Error: Unknown parameter '{args[i]}'.");
                            return null;
                        }
                        if (options.InputFile == null)
                        {
                            options.InputFile = args[i];
                        }
                        break;
                }
            }
            return options;
        }
        
        /// <summary>
        /// Gets the set of PWM pins on an arduino, based on the model provided, when invoking the compiler.
        /// </summary>
        /// <returns>A list of PWM pin IDs</returns>
        public static List<string> GetPWMSet()
        {
            switch (options.Arduino.ToLower())
            {
                case "uno":
                case "nano":
                case "mini":
                    return new List<string>() {"3","5","6","9","10","11"};
                case "mega":
                    return new List<string>() {"2","3","4","5","6","7","8","9","10","11","12","13","44","45","46"};
                case "yun":
                case "micro":
                case "leonardo":
                    return new List<string>() {"3","5","6","9","10","11","13"};
                case "uno_wifi_dev_ed":
                    return new List<string>() {"3","5","6","9","10"};
                default:
                    return new List<string>();
            }
        }
    }

    /// <summary>
    /// An extension class to extend the functionality of strings. 
    /// This class is used to add better shell integration, when calling commands in the shell, from the compiler.
    /// </summary>
    public static class ShellHelper
    {
        /// <summary>
        /// Calls a command in /bin/bash on linux.
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <returns>The result of the command</returns>
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
        /// <summary>
        /// Calls a command in CMD.exe on windows.
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <returns>The result of the command</returns>
        public static string Cmd(this string cmd)
        {
            var process = new Process()
            {

                StartInfo = new ProcessStartInfo
                {
                    FileName = "CMD.exe",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                }
            };
            process.Start();
            using (StreamWriter sw = process.StandardInput)
                sw.WriteLine(cmd);
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }

}
