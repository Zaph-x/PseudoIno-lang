using System.Net;
using System.Drawing;
using System;
using System.IO;

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

        public void log(object obj)
        {
            if (!string.IsNullOrEmpty(Options.LogFile))
            {
                if (File.Exists(Options.LogFile))
                {
                    File.AppendAllText(Options.LogFile, obj.ToString()+"\n");
                }
                else
                {
                    using (var stream = File.Create(Options.LogFile))
                    { }
                    File.AppendAllText(Options.LogFile, obj.ToString()+"\n");
                }
            }
        }

        public void Info(object obj)
        {
            if (Options.Verbose)
            {
                Console.WriteLine(obj);
                log(obj);
            }
        }

        public void Error(object obj)
        {
            if (Options.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(obj);
                Console.ResetColor();
                log("ERROR: " + obj);
            }
        }
    }
}