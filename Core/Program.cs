using System.Collections.Generic;
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

namespace Core
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            CommandLineOptions options = ParseOptions(args);
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
                // ASTHelper ast = new ASTHelper(tokens);
                // PrettyPrinter pprint = new PrettyPrinter();
                // pprint.Visit(parsenizer.Root);
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

                if (options.OutputFile)
                {
                    string path = Directory.GetCurrentDirectory()+"/";
                    try
                    {
                        parsenizer.Root.Accept(new CodeGenerationVisitor($"{path}Core/PrecompiledBinaries/tmp/sketch/output.ino.cpp.o"));
                    }
                    catch (Exception e)
                    {
                        verbosePrinter.Error("Encountered an error in code generation. Stopping.");
                        return 123456789;
                    }
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Console.WriteLine("We're on Linux!");
                        
                        List<string> cmds = new List<string>();
                        cmds.Add($"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-gcc -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu=atmega328p -o {path}Core/PrecompiledBinaries/tmp/output.ino.elf {path}Core/PrecompiledBinaries/tmp/sketch/output.ino.cpp.o /tmp/../core/core_arduino_avr_pro_cpu_16MHzatmega328_db62bc5f977f010e956e85fb47a0c0b7.a -L/tmp/ -lm");
                        cmds.Add($"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 /tmp/output.ino.elf /tmp/output.ino.eep");
                        cmds.Add($"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -R .eeprom /tmp/output.ino.elf /tmp/output.ino.hex");
                        RunCommandsUnix(cmds,"");
                        //Console.WriteLine(path);
                        /*string strCmdText =
                            $"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-gcc -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu=atmega328p -o {path}Core/PrecompiledBinaries/tmp/output.ino.elf {path}Core/PrecompiledBinaries/tmp/sketch/output.ino.cpp.o /tmp/../core/core_arduino_avr_pro_cpu_16MHzatmega328_db62bc5f977f010e956e85fb47a0c0b7.a -L/tmp/ -lm";
                        var p = System.Diagnostics.Process.Start("/bin/bash",strCmdText);
                        
                        using (StreamWriter sw = p.StandardInput)
                        {
                            if (sw.BaseStream.CanWrite)
                            {
                                //sw.WriteLine("mysql -u root -p");
                                sw.WriteLine($"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 /tmp/output.ino.elf /tmp/output.ino.eep");
                                sw.WriteLine($"{path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -R .eeprom /tmp/output.ino.elf /tmp/output.ino.hex");
                            }
                        }*/
                        // {path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-gcc -w -Os -g -flto -fuse-linker-plugin -Wl,--gc-sections -mmcu=atmega328p -o /tmp/arduino_build_815641/BlinkWithoutDelay.ino.elf /tmp/arduino_build_815641/sketch/BlinkWithoutDelay.ino.cpp.o /tmp/arduino_build_815641/../arduino_cache_826307/core/core_arduino_avr_pro_cpu_16MHzatmega328_db62bc5f977f010e956e85fb47a0c0b7.a -L/tmp/arduino_build_815641 -lm
                        // {path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -j .eeprom --set-section-flags=.eeprom=alloc,load --no-change-warnings --change-section-lma .eeprom=0 /tmp/arduino_build_815641/BlinkWithoutDelay.ino.elf /tmp/arduino_build_815641/BlinkWithoutDelay.ino.eep
                        // {path}Core/PrecompiledBinaries/unix/hardware/tools/avr/bin/avr-objcopy -O ihex -R .eeprom /tmp/arduino_build_815641/BlinkWithoutDelay.ino.elf /tmp/arduino_build_815641/BlinkWithoutDelay.ino.hex
                        
                        // avrdude -CC:\Program Files (x86)\Arduino\hardware\tools\avr/etc/avrdude.conf -v -patmega328p -carduino -PCOM5 -b115200 -D -Uflash:w:C:\Users\flufg\AppData\Local\Temp\arduino_build_22316/Blink.ino.hex:i
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WriteLine("We're on Windows!");
                        string strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
                        System.Diagnostics.Process.Start("CMD.exe",strCmdText);
                    }
                    else
                    {
                        verbosePrinter.Error("OS not supported! Stopping.");
                    }
                    
                }
                
                //TODO further compilation
                //"C:\Program Files (x86)\Arduino\hardware\tools\avr/bin/avr-g++" -c -g -Os -std=gnu++11 -fpermissive -fno-exceptions -ffunction-sections -fdata-sections -fno-threadsafe-statics -Wno-error=narrowing -MMD -flto -mmcu=atmega328p -DF_CPU=16000000L -DARDUINO=10812 -DARDUINO_AVR_UNO -DARDUINO_ARCH_AVR "-IC:\Program Files (x86)\Arduino\hardware\arduino\avr\cores\arduino" "-IC:\Program Files (x86)\Arduino\hardware\arduino\avr\variants\standard" "C:\Users\bruger\Documents\aau\4semester\p4\github\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp" -o "C:\Users\bruger\Documents\aau\4semester\p4\github\P4-program\Tests\CodeGeneration.Tests\bin\Debug\netcoreapp3.1\Codegen_output.cpp.o"
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
            process.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            using (StreamWriter sw = process.StandardInput)
            {
                foreach (var cmd in cmds)
                {
                    sw.WriteLine (cmd);
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
            System.Console.WriteLine("    -pp| --prettyprinter    Print the abstract syntax tree.");
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
                        if (args.Length >= i + 1 && !args[i + 1].StartsWith('-'))
                        {
                            ++i;
                            options.LogFile = args[i];
                        }
                        else
                        {
                            Console.Error.WriteLineAsync($"Error: Log file not provided");
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
