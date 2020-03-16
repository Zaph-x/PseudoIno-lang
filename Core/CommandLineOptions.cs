namespace Core
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
    }
}