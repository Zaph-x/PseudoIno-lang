# P4 Program Repo [![Build Status](https://travis-ci.com/Zaph-x/P4-program.svg?token=1NahDsgHPV3GoDxtzAyp&branch=master)](https://travis-ci.com/Zaph-x/P4-program)

This is the program repository, for a 4th semester, P4 project.
The purpose is to create a compiler, that will compile a source language to an Arduino.

## Style guide

This is a style guide for this repository.

### Spaces or tabs

Do not use tabs in our source code. This is generally converted to spaces, by modern IDEs. We use 4 whitespaces instead of tabs.

### Accolades

Do use the language convention for accolades. This means that accolades begin on a new line.

```csharp
int func()
{
    return 1;
}
```

### Variable names

Do use the clean code conventions. This means that variable and function names, must be clear and concise.
Variables used for counting can be named `i`, `j`, `k`, etc. if it makes sense.

Function names are to start with a capital letter, such as `GetItemDescriptor()`.

### Documentation

The code must be documented well, such that it is understandable by everyone. This can be done with doc-comments.

```csharp
/// <summary>
/// This is a dummy function
/// </summary>
public void Dummy() {...
```
