﻿using System;
using System.Diagnostics;
using System.IO;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Lexer;
using Core.Objects;
using Core.Exceptions;
using Lexer.Exceptions;
using Parser;
using AbstractSyntaxTree.Objects;
using CodeGeneration;
using Lexer.Objects;
using Contextual_analysis;
using System.Collections.Generic;
using System.IO.Ports;

namespace Core
{
    public class Program
    {
        public static int Main(string[] args)
        {

            Stopwatch timer = new Stopwatch();
            timer.Start();
            CommandLineOptions options = ParseOptions(args);
            return 0;
            VerbosePrinter verbosePrinter = new VerbosePrinter(options);
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
                Tokenizer tokenizer = new Tokenizer(reader);
                verbosePrinter.Info("Generating tokens...");
                tokenizer.GenerateTokens();
                if (Tokenizer.HasError)
                {
                    verbosePrinter.Error("Encountered syntax errors. Stopping.");
                    return 5;
                }
                verbosePrinter.Info($" Generated {tokenizer.Tokens.Count} tokens.");
                if (options.Verbose)
                {
                    foreach (var token in tokenizer.Tokens)
                    {
                        verbosePrinter.Info(token);
                    }
                }
                verbosePrinter.Info("Generating parse table");
                List<ScannerToken> tokens = tokenizer.Tokens.ToList();
                Parsenizer parsenizer = new Parsenizer(tokens);
                string debugMessage = "";
                parsenizer.Parse(out debugMessage);
                verbosePrinter.Info(debugMessage);
                if (Parsenizer.HasError)
                {
                    verbosePrinter.Error("Encountered an error state in the parser. Stopping.");
                    return 4;
                }
                if (options.PrettyPrinter)
                {
                    parsenizer.Root.Accept(new PrettyPrinter());
                }
                parsenizer.Root.Accept(new TypeChecker());
                if (TypeChecker.HasError)
                {
                    verbosePrinter.Error("Encountered an error in the type checker. Stopping.");
                    return 3;
                }

                string path = AppContext.BaseDirectory;

                try
                {
                    File.Delete($"{path}/PrecompiledBinaries/tmp/sketch/output.cpp");
                    parsenizer.Root.Accept(new CodeGenerationVisitor($"{path}/PrecompiledBinaries/tmp/sketch/output.cpp"));
                    if (options.DryRun) File.Delete($"{path}/PrecompiledBinaries/tmp/sketch/output.cpp");
                }
                catch (FileNotFoundException e)
                {
                    verbosePrinter.Error("Encountered an error in code generation. Stopping.");
                    Console.Error.WriteLine(e.Message);
                    return 2;
                }
                if (!options.OutputFile)
                {

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                    {
                        Console.WriteLine("We're on Linux!");
                        path = path.Replace(" ", "\\ ");
                        List<string> cmds = new List<string>();

                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-g++ -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/cores/arduino -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/variants/standard {path}/PrecompiledBinaries/tmp/sketch/output.cpp -o /dev/null");
                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-g++ -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/cores/arduino -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/variants/standard {path}/PrecompiledBinaries/tmp/sketch/output.cpp -o {path}/PrecompiledBinaries/tmp/preproc/ctags_target_for_gcc_minus_e.cpp");
                        cmds.Add($"{path}PrecompiledBinaries/unix/tools-builder/ctags/5.8-arduino11/ctags -u --language-force=c++ -f - --c++-kinds=svpf --fields=KSTtzns --line-directives {path}/PrecompiledBinaries/tmp/preproc/ctags_target_for_gcc_minus_e.cpp");

                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-g++ -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10811 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/cores/arduino -I{path}/PrecompiledBinaries/unix/hardware/arduino/avr/variants/standard {path}/PrecompiledBinaries/tmp/sketch/output.cpp -o {path}/PrecompiledBinaries/tmp/sketch/output.cpp.o");
                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-gcc -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu={options.Processor} -o {path}/PrecompiledBinaries/tmp/output.cpp.elf {path}/PrecompiledBinaries/tmp/sketch/output.cpp.o {path}/PrecompiledBinaries/randomAFile.a -L{path}/PrecompiledBinaries/tmp -lm");
                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 {path}/PrecompiledBinaries/tmp/output.cpp.elf {path}/PrecompiledBinaries/tmp/output.cpp.eep");
                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -R .eeprom {path}/PrecompiledBinaries/tmp/output.cpp.elf {path}/PrecompiledBinaries/tmp/output.cpp.hex");
                        cmds.Add($"{path}PrecompiledBinaries/unix/hardware/tools/avr/bin/avrdude -C{path}/PrecompiledBinaries/etc/avrdude.conf -v -p{options.Processor} -carduino -P/dev/ttyACM{options.Port} -b115200 -D -Uflash:w:{path}/PrecompiledBinaries/tmp/output.cpp.hex:i");
                        RunCommandsUnix(cmds, "");
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WriteLine("We're on Windows!");
                        path = path.Replace('/', '\\');

                        List<string> cmds = new List<string>();
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-g++\" -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}\\PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o nul");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-g++\" -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -flto -w -x c++ -E -CC -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}\\PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o \"{path}\\PrecompiledBinaries\\tmp\\Preproc\\ctags_target_for_gcc_minus_e.cpp\"");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\ctags.exe\" -u --language-force=c++ -f - --c++-kinds=svpf --fields=KSTtzns --line-directives \"{path}\\PrecompiledBinaries\\tmp\\Preproc\\ctags_target_for_gcc_minus_e.cpp\"");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-g++.exe\" -c -g -Os -w -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu={options.Processor} -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_{options.Arduino.ToUpper()} -DARDUINO_ARCH_AVR \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\cores\\arduino\" \"-I{path}\\PrecompiledBinaries\\arduino\\avr\\variants\\standard\" \"{path}\\PrecompiledBinaries\\tmp\\sketch\\output.cpp\" -o \"{path}\\PrecompiledBinaries\\tmp\\sketch\\output.cpp.o\"");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-gcc.exe\" -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu={options.Processor} -o \"{path}\\PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}\\PrecompiledBinaries\\tmp\\sketch\\output.cpp.o\" \"{path}\\PrecompiledBinaries\\randomAFile.a\" \"-L{path}\\PrecompiledBinaries\\tmp\" -lm");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-objcopy.exe\" -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 \"{path}\\PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}\\PrecompiledBinaries\\tmp\\output.cpp.eep\"");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avr-objcopy.exe\" -O ihex -R .eeprom \"{path}\\PrecompiledBinaries\\tmp\\output.cpp.elf\" \"{path}\\PrecompiledBinaries\\tmp\\output.cpp.hex\"");
                        cmds.Add($"\"{path}\\PrecompiledBinaries\\win\\avrdude\" \"-C{path}\\PrecompiledBinaries\\etc\\avrdude.conf\" -v -p{options.Processor} -carduino -PCOM{options.Port} -b115200 -D \"-Uflash:w:{path}\\PrecompiledBinaries\\tmp\\output.cpp.hex:i\"");
                        RunCommandWindows(cmds, "");
                    }
                    else
                    {
                        verbosePrinter.Error("OS not supported! Stopping.");
                    }

                }

                //TODO further compilation
                //"C:\\Program Files (x86)\Arduino\hardware\tools\avr/bin/avr-g++" -c -g -Os -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu=atmega328p -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_UNO -DARDUINO_ARCH_AVR "-IC:\\Program Files (x86)\Arduino\hardware\arduino\avr\cores\arduino" "-IC:\\Program Files (x86)\Arduino\hardware\arduino\avr\variants\standard" "C:\Users\bruger\Documents\aau\4semester\\P4\github\\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp" -o "C:\Users\bruger\Documents\aau\4semester\\P4\github\\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp.o"
            }

            timer.Stop();
            verbosePrinter.Info($"\nCompilation took {timer.Elapsed.TotalSeconds} seconds.");
            return 0;
        }

        static void RunCommandsUnix(List<string> cmds, string workingDirectory = "")
        {
            var process = new Process();
            var psi = new ProcessStartInfo();
            psi.FileName = "/bin/bash";
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.WorkingDirectory = workingDirectory;
            process.StartInfo = psi;
            process.Start();
            // process.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            using (StreamWriter sw = process.StandardInput)
            {
                foreach (var cmd in cmds)
                {
                    sw.WriteLine(cmd);
                }
            }
            process.WaitForExit();
        }

        static void RunCommandWindows(List<string> cmds, string workingDirectory = "")
        {
            var process = new Process();
            var psi = new ProcessStartInfo();
            psi.FileName = "CMD.exe";
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.WorkingDirectory = workingDirectory;
            process.StartInfo = psi;
            process.Start();
            // process.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            using (StreamWriter sw = process.StandardInput)
            {
                foreach (var cmd in cmds)
                {
                    sw.WriteLine(cmd);
                }
            }
            process.WaitForExit();
        }

        public static void Help()
        {
            System.Console.WriteLine("---===### PIC, PseudoIno Compiler ###===---");
            System.Console.WriteLine("-------------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("    pic <source code> [options]");
            System.Console.WriteLine("    <source code>        The path to the code, that is to be compiled.");
            System.Console.WriteLine("    [options]            See Optional Paramters");
            System.Console.WriteLine("");
            System.Console.WriteLine("Optional Parameters:");
            System.Console.WriteLine("    -d | --DryRun          Runs the compiler without producing an output.");
            System.Console.WriteLine("    -o | --Output          Tells the compiler not to write to the Arduino, and instead produce a file.");
            System.Console.WriteLine("    -v | --Verbose         Prints additional information when compiling.");
            System.Console.WriteLine("    -b | --boilerplate     Generates a boilerplate file for your code.");
            System.Console.WriteLine("    -l | --logfile <path>  Prints additional information when compiling.");
            System.Console.WriteLine("    -p | --port <number>   Specifies the port to upload to.");
            System.Console.WriteLine("    -a | --arduino <model> Specifies the arduino model you're uploading to. (Default: UNO)");
            System.Console.WriteLine("    -pr| --proc <model>    Specifies the arduino processor you're uploading to. (Default: atmega328p)");
            System.Console.WriteLine("    -pp| --prettyprinter   Print the abstract syntax tree.");
            System.Console.WriteLine("");
        }

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
                            options.Processor = args[i].ToUpper();
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
                            if (int.TryParse(args[i], out int result))
                            {
                                options.Port = result;
                            }
                            else
                            {
                                Console.Error.WriteLine($"Error: Unknown parameter '{args[i]}'.");
                                return null;
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine($"Error: No Port Provided. The compiler will try to find one available");
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
                                {
                                    int.TryParse(strOutput[1].Substring(3), out int result);
                                    options.Port = result;
                                }
                            }
                            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                            {
                                throw new NotImplementedException("This feature is currently not supported on Linux. Please specify a port yourself.");
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
    }
}
