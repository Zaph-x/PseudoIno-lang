# P4 Program Repo [![Build Status](https://travis-ci.com/Zaph-x/P4-program.svg?token=1NahDsgHPV3GoDxtzAyp&branch=master)](https://travis-ci.com/Zaph-x/P4-program)

This is the program repository, for a 4th semester, P4 project.
The purpose is to create a compiler, that will compile a source language to an Arduino.

## Prerequisites

In order to build this project, you must have [dotnet core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed.
Furthermore, in order to generate the documentation, you must have [PowerShell Core](https://github.com/PowerShell/PowerShell/releases/tag/v7.0.1) installed.
In order to generate the coverage report, you must use either PowerShell Core or Bash, to run one of the 

## Usage

#### Running from dotnet

To run the compiler from the root folder of the project use the following syntax:
`dotnet run -p ./Core/Core.csproj -- PATH-TO-FILE`

```pi
Optional Parameters:
    -d  | --DryRun          Runs the compiler without producing an output.
    -o  | --Output          Tells the compiler not to write to the Arduino, and instead produce a file.
    -v  | --Verbose         Prints additional information when compiling.
    -b  | --boilerplate     Generates a boilerplate file for your code.
    -l  | --logfile <path>  Prints additional information when compiling.
    -p  | --port <number>   Specifies the port to upload to.
    -a  | --arduino <model> Specifies the arduino model you're uploading to. (Default: UNO)
    -pr | --proc <model>    Specifies the arduino processor you're uploading to. (Default: atmega328p)
    -pp | --prettyprinter   Print the abstract syntax tree.
```

#### Building and running

Building the program is done by executing `dotnet build` from the command line.
The compiled compiler will be placed in `./Core/bin/Debug/netcoreapp3.1/` where it can be run.

### Documentation

The documented code of the compiler can be [here](https://zaph-x.github.io/doc/PseudoIno/index.html)

### Example program 1 (Blink)

```pi
#Builtin led is on digital pin 13
func blink
  dpin13 is on
  wait 1s
  dpin13 is off
  wait 1s
end blink

func loop
  call blink
end loop
```

### Example program 2 (Fade)

```pi
brightness is 0
amountToAdd is 5

func loop
  dpin9 is brightness
  brightness is amountToAdd + brightness
  
  if (brightness less or equal 0) or (brightness greater or equal 255) do
    brightness is amountToAdd * -1
  end if
  wait 30ms
end loop
```
