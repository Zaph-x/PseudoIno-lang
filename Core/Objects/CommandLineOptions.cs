namespace Core.Objects
{
    /// <summary>
    /// The base class for the command line options that will be checked on compiler invokation.
    /// </summary>
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
        public bool Verbose { get; set; }
        /// <summary>
        /// A file to write tokens and other log things to.
        /// </summary>
        /// <value>null</value>
        public string LogFile { get; set; }
        /// <summary>
        /// A boolean value to specify if the compiler should generate a boilerplate file for the PseudoIno language
        /// </summary>
        /// <value>false</value>
        public bool Boilerpate { get; set; }
        /// <summary>
        /// A Boolean value to specify if the compiler should print the AST.
        /// </summary>
        public bool PrettyPrinter { get; set; }
        /// <summary>
        /// An integer value for the given COMPort the arduino is attached to.
        /// </summary>
        /// <value>0 if no other value is assigned. The compiler will try to figure out what port to use if no port is defined</value>
        public string Port {get;set;} = "COM0";
        /// <summary>
        /// A string value that specifies the arduino processor you're uploading to.
        /// </summary>
        /// <value>UNO</value>
        public string Arduino {get;set;} = "UNO";
        /// <summary>
        /// A string value to specify the onboard processor.
        /// </summary>
        /// <value>atmega328p</value>
        public string Processor {get;set;} = "atmega328p";

    }
}