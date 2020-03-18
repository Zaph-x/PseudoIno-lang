using System.IO;
using System;
using System.Reflection;
using Lexer;

namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineOptions options = ParseOptions(args);
            if (options?.InputFile == null)
            {
                Help();
                return;
            }
            using (StreamReader reader = new StreamReader(options.InputFile))
            {
                try
                {
                    FileChecker.CheckEncoding(reader);
                }
                catch (System.Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
                Tokenizer tokennizer = new Tokenizer(reader);
                tokennizer.GenerateTokens();
            }

        }


        public static void Help()
        {
            System.Console.WriteLine("---===### PIC, PseudoIno Compiler ###===---");
            System.Console.WriteLine("-------------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("    pic <source code> <options>");
            System.Console.WriteLine("    <source code>        The path to the code, that is to be compiled.");
            System.Console.WriteLine("");
            System.Console.WriteLine("Optional Parameters:");
            System.Console.WriteLine("    -d | --DryRun        Runs the compiler without producing an output.");
            System.Console.WriteLine("    -o | --Output        Tells the compiler not to write to the Arduino, and instead produce a file.");
            System.Console.WriteLine("    -v | --Verbose       Prints additional information when compiling.");
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
