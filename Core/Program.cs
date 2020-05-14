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

                if (!options.OutputFile)
                {
                    try
                    {
                        parsenizer.Root.Accept(new CodeGenerationVisitor("Codegen_output.cpp"));
                        if (options.DryRun) File.Delete("Codegen_output.cpp");
                    }
                    catch (Exception e)
                    {
                        verbosePrinter.Error("Encountered an error in code generation. Stopping.");
                        return 2;
                    }
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                    {
                        Console.WriteLine("We're on Linux!");
                        string strCmdText =
                            $"Core/PrecompiledBinaries/unix/arduino-builder -dump-prefs -logger=machine -hardware Core/PrecompiledBinaries/unix/hardware -tools /home/padi/Documents/apps/arduino-1.8.11/tools-builder -tools Core/PrecompiledBinaries/unix/hardware/tools/avr -built-in-libraries Core/PrecompiledBinaries/unix/libraries -libraries /home/padi/Arduino/libraries -fqbn=arduino:avr:pro:cpu=16MHzatmega328 -ide-version=10811 -build-path /tmp/arduino_build_815641 -warnings=none -build-cache Core/PrecompiledBinaries/tmp/arduino_cache -prefs=build.warn_data_percentage=75 -prefs=runtime.tools.avrdude.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -prefs=runtime.tools.avrdude-6.3.0-arduino17.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -prefs=runtime.tools.arduinoOTA.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -prefs=runtime.tools.arduinoOTA-1.3.0.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -prefs=runtime.tools.avr-gcc.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -prefs=runtime.tools.avr-gcc-7.3.0-atmel3.6.1-arduino5.path=Core/PrecompiledBinaries/unix/hardware/tools/avr -verbose Codegen_output.cpp";// Core/PrecompiledBinaries/unix/{options.InputFile}";
                        System.Diagnostics.Process.Start("/bin/bash",strCmdText);
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
