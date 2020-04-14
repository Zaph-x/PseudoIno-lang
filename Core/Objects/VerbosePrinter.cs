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
            if (File.Exists(Options.LogFile))
            {
                File.WriteAllText(Options.LogFile, "");
            }
            else
            {
                using (var stream = File.Create(Options.LogFile))
                { }
                File.AppendAllText(Options.LogFile, "");
            }
        }

        public void Log(object obj)
        {
            if (!string.IsNullOrEmpty(Options.LogFile))
            {
                if (File.Exists(Options.LogFile))
                {
                    File.AppendAllText(Options.LogFile, obj.ToString() + "\n");
                }
            }
        }
        public void LogInline(object obj)
        {
            if (!string.IsNullOrEmpty(Options.LogFile))
            {
                if (File.Exists(Options.LogFile))
                {
                    File.AppendAllText(Options.LogFile, obj.ToString());
                }
            }
        }

        public void Info(object obj)
        {
            if (Options.Verbose)
            {
                Console.WriteLine(obj);
            }
            Log(obj);
        }
        
        public void InfoInline(object obj)
        {
            if (Options.Verbose)
            {
                Console.Write(obj);
            }
            LogInline(obj);
        }


        public void Error(object obj)
        {
            if (Options.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(obj);
                Console.ResetColor();
            }
            Log("ERROR: " + obj);
        }
    }
}