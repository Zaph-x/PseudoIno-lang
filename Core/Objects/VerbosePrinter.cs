using System.Net;
using System.Drawing;
using System;
using System.IO;

namespace Core.Objects
{
    /// <summary>
    /// The verbose printer invoked when the compiler is called with the -v
    /// </summary>
    public class VerbosePrinter
    {
        /// <summary>
        /// The options passed to the compiler.
        /// </summary>
        /// <value>Always set on compiler invokation.</value>
        CommandLineOptions Options { get; set; }
        /// <summary>
        /// The constructor of the VerbosePrinter.
        /// This constructor determines whether we're in verbose mode or not.
        /// If the <c>-v</c> flag is present, verbose mode will be enabled.
        /// </summary>
        /// <param name="options">The options used to check if verbose mode should be enabled.</param>
        public VerbosePrinter(CommandLineOptions options)
        {
            Options = options;
            if (Options.Verbose)
            {
                System.Console.WriteLine("Verbosity enabled.");
            }
            if (Options.LogFile != null)
            {
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
        }

        /// <summary>
        /// The method reponsible for logging. If a logfile is provided, the object passed to the method will be logged to the logfile.
        /// </summary>
        /// <param name="obj">The object to log</param>
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
        /// <summary>
        /// This method will log an object to a file without appending a newline character at the end of the string. If a logfile is provided, the object passed to the method will be logged to the logfile.
        /// </summary>
        /// <param name="obj">The object to log</param>
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
        /// <summary>
        /// This method will log an info message to the console.
        /// </summary>
        /// <param name="obj">The object to log</param>
        public void Info(object obj)
        {
            if (Options.Verbose)
            {
                Console.WriteLine(obj);
            }
            Log(obj);
        }
        /// <summary>
        /// This method will log an info message to the console, without appending newline at the end
        /// </summary>
        /// <param name="obj">The object to log</param>
        public void InfoInline(object obj)
        {
            if (Options.Verbose)
            {
                Console.Write(obj);
            }
            LogInline(obj);
        }

        /// <summary>
        /// This method will log an error to the console, as well as writes it to the logfile
        /// </summary>
        /// <param name="obj">The object to log</param>
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