# Core.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

# All types

|   |   |   |
|---|---|---|
| [Program Class](#program-class) | [EncodingNotSupportedException Class](#encodingnotsupportedexception-class) | [FileChecker Class](#filechecker-class) |
| [ShellHelper Class](#shellhelper-class) | [CommandLineOptions Class](#commandlineoptions-class) | [VerbosePrinter Class](#verboseprinter-class) |
# Program Class

Namespace: Core

The core of the compiler.
This is where every step of the compiler is called from.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **GetPWMSet()** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> | Gets the set of PWM pins on an arduino, based on the model provided, when invoking the compiler. |
| **Help()** | void | Prints the help list to the terminal.<br>This is automatically called, if no file path is provided when the compiler is invoked. |
| **Main([string[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) args)** | int | The entry point of the compiler.<br>Here the user must provide a set of compiler flags, in order for the compiler to produce a satisfying product.<br>The user must provide a filepath to the file they wish to compile. Otherwise the compiler will halt and exit with an exit code of 1.<br>Compiler flags can be provided to activate additional functionality.<br>**Compiler flags**<br>`-d` or `--dryrun` Runs the compiler without producing an output.<br>`-o` or `--output` Tells the compiler not to write to the Arduino, and instead produce a file.<br>`-v` or `--verbose` Prints additional information when compiling.<br>`-b` or `--boilerplate` Generates a boilerplate file for your code.<br>`-l` or `--logfile` (Must be followed by a file path) Prints additional information when compiling.<br>`-p` or `--port` (Must be followed by a port number) Specifies the port to upload to.<br>`-a` or `--arduino` (Must be followed by an Arduino model) Specifies the arduino model you're uploading to. (Default: UNO)<br>`-pr` or `--proc` (Must be followed by a valid processor) Specifies the arduino processor you're uploading to. (Default: atmega328p)<br>`-pp` or `--prettyprinter` Print the abstract syntax tree.<br>**Compiler exit code**<br>`0` Compilation finished with no errors.<br>`1` A file path was not provided to the compiler for compilation.<br>`20` The file provided was not encoded as a UTF-8 file.<br>`5` An error was encountered while scanning the input program. This is usually caused by an unclosed string, comment, or parenthesis.<br>`4` An error was encountered in the parser. This is usually due to an invalidly structured program.<br>`3` An error was encountered in the type checker. This happens when two types are mismatched, either on assignment or within an expression. Furthermore, this can be caused by not defining a called function, or a function being defined multiple times.<br>`2` This error is encountered when the code generator can not find the output file for the intermediate representation code.<br>`23` This error is encountered when the dryrun flag is invoked, but the compiler can not find the output file for the intermediate representation code. |
| **ParseOptions([string[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) args)** | [CommandLineOptions](#commandlineoptions-class) | This function will parse the flags passed to the compiler.<br>This is done using a switch case, which will then set the correct flags and values in the compiler options. |
# ShellHelper Class

Namespace: Core

An extension class to extend the functionality of strings. 
This class is used to add better shell integration, when calling commands in the shell, from the compiler.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Bash(string cmd)** | string | Calls a command in /bin/bash on linux. |
| **Cmd(string cmd)** | string | Calls a command in CMD.exe on windows. |
# EncodingNotSupportedException Class

Namespace: Core.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception thrown when a file is wrongly encoded.

## Properties

| Name | Type | Summary |
|---|---|---|
| **TargetSite** | [MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) |  |
| **StackTrace** | string |  |
| **Message** | string |  |
| **Data** | [IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary) |  |
| **InnerException** | [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) |  |
| **HelpLink** | string |  |
| **Source** | string |  |
| **HResult** | int |  |
## Constructors

| Name | Summary |
|---|---|
| **EncodingNotSupportedException(string message)** | An exception which will be thrown when a file encoding is not supported in the compiler. |
# CommandLineOptions Class

Namespace: Core.Objects

The base class for the command line options that will be checked on compiler invokation.

## Properties

| Name | Type | Summary |
|---|---|---|
| **OutputFile** | bool | The file to write to. This will produce an output without uploading it to the Arduino. |
| **InputFile** | string | The source code for the compiler. |
| **DryRun** | bool | If this is set to true, the compiler will not produce a file. |
| **Verbose** | bool | If this is set to true, the compiler will print additional information |
| **LogFile** | string | A file to write tokens and other log things to. |
| **Boilerpate** | bool | A boolean value to specify if the compiler should generate a boilerplate file for the PseudoIno language |
| **PrettyPrinter** | bool | A Boolean value to specify if the compiler should print the AST. |
| **Port** | string | An integer value for the given COMPort the arduino is attached to. |
| **Arduino** | string | A string value that specifies the arduino processor you're uploading to. |
| **Processor** | string | A string value to specify the onboard processor. |
# FileChecker Class

Namespace: Core.Objects

The class responsible for checking if a given file is correctly encoded.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **CheckEncoding([StreamReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader) stream)** | bool | The method for checking if a file is ecoded as either UTF8 or ASCII |
# VerbosePrinter Class

Namespace: Core.Objects

The verbose printer invoked when the compiler is called with the -v

## Properties

| Name | Type | Summary |
|---|---|---|
| **Options** | [CommandLineOptions](#commandlineoptions-class) | The options passed to the compiler. |
## Constructors

| Name | Summary |
|---|---|
| **VerbosePrinter([CommandLineOptions](#commandlineoptions-class) options)** | The constructor of the VerbosePrinter.<br>This constructor determines whether we're in verbose mode or not.<br>If the \<c\>-v\</c\> flag is present, verbose mode will be enabled. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Error([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | void | This method will log an error to the console, as well as writes it to the logfile |
| **Info([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | void | This method will log an info message to the console. |
| **InfoInline([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | void | This method will log an info message to the console, without appending newline at the end |
| **Log([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | void | The method reponsible for logging. If a logfile is provided, the object passed to the method will be logged to the logfile. |
| **LogInline([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | void | This method will log an object to a file without appending a newline character at the end of the string. If a logfile is provided, the object passed to the method will be logged to the logfile. |
