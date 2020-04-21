using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.Reflection;
using System.Linq;
using Lexer;
using Core.Objects;
using Core.Exceptions;
using Lexer.Exceptions;
using Parser;
using AbstractSyntaxTree.Objects;
using Lexer.Objects;

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
                string debugMessage;
                List<Token> tokenList = parsenizer.Parse(out debugMessage);
                verbosePrinter.Info(debugMessage);
                if (Parsenizer.HasError) {
                    return 4;
                }
                ASTHelper _ASTHelper = new ASTHelper(tokenList);
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
                    case "-l":
                    case "--logfile":
                        if (args.Length - 1 <= i + 1 && !args[i].StartsWith('-'))
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
