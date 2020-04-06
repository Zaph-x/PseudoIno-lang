namespace Core.Objects
{
    public class CommandLineOptions
    {
        /// <summary>
        /// The file to write to. This will produce an output without uploading it to the Arduino.
        /// </summary>
        /// <value>False if the flag is not set.</value>
        public bool OutputFile { get; set; }
        /// <summary>
        /// The source code for the compiler.
        /// </summary>
        /// <value>Has no default value.</value>
        public string InputFile { get; set; }
        /// <summary>
        /// If this is set to true, the compiler will not produce a file.
        /// </summary>
        /// <value>False</value>
        public bool DryRun { get; set; }
        /// <summary>
        /// If this is set to true, the compiler will print additional information
        /// </summary>
        /// <value>False</value>
        public bool Verbose {get;set;}
        /// <summary>
        /// A file to write tokens and other log things to.
        /// </summary>
        /// <value>null</value>
        public string LogFile {get;set;}
    }
}