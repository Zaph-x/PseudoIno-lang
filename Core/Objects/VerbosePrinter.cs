using System.Drawing;
using System;

namespace Core.Objects
{
    public class VerbosePrinter
    {
        CommandLineOptions Options { get; set; }
        public VerbosePrinter(CommandLineOptions options)
        {
            Options = options;
            if (Options.Verbose)
            {
                System.Console.WriteLine("Verbosity enabled.");
            }
        }

        public void Info(object obj)
        {
            if (Options.Verbose) Console.WriteLine(obj);
        }

        public void Error(object obj)
        {
            if (Options.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(obj);
                Console.ResetColor();
            }
        }
    }
}