# Lexer.dll v.1.0.0.0 API documentation

# All types

|   |   |   |
|---|---|---|
| [Program Class](#program-class) | [InvalidSyntaxException Class](#invalidsyntaxexception-class) | [TokenType Enum](#tokentype-enum) |
| [Recogniser Class](#recogniser-class) | [Keywords Class](#keywords-class) |   |
| [Tokenizer Class](#tokenizer-class) | [Token Class](#token-class) |   |
# Program Class

Namespace: Lexer


# Recogniser Class

Namespace: Lexer

The class responsible for recognising characters in the language

## Methods

| Name | Returns | Summary |
|---|---|---|
| **IsAcceptedCharacter(char character)** | bool | A function that checks if a char is a character in the alphabet. |
| **IsDigit(char character)** | bool | A function to check if a char is a digit. This is done by calling IsBetween. |
| **IsKeyword(string input)** | bool | A function that checks if a given string is a keyword. |
# Tokenizer Class

Namespace: Lexer

The class responsible for generating the tokens from the source language

## Properties

| Name | Type | Summary |
|---|---|---|
| **CurrentChar** | char | The current character in the sequencce |
| **NextChar** | char | The next character in the sequence |
| **Line** | int | The current line of the current character |
| **Offset** | int | The current offset of the current character |
| **BufferOffset** | long | The offset of the buffer. This is set such that the scanner can look ahead. |
## Constructors

| Name | Summary |
|---|---|
| **Tokenizer([StreamReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader) stream)** | The constructor for the Tokenizer class. This will set the iniitiate a reader and a recogniser. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **GenerateTokens()** | void | A function to to generate tokens. This is done by reading from the stream and using any of the scan functions. |
| **Peek()** | char | Peeks the next character in the stream and sets NextChar to the value |
| **Peek(int positions)** | char | Peeks the nth character in the stream and sets NextChar to the value of the character |
| **Pop()** | char | Sets current character as the next character in the stream and advances in the stream.<br>This is also responsible for counting up lines and the offset, to provide context for error handling. |
| **Token([TokenType](#tokentype-enum) type)** | [Token](#token-class) | A function to generate a token without a value.<br>This will be used to generate tokens where the value does not have to be carried over to the target language |
| **Token([TokenType](#tokentype-enum) type, string val)** | [Token](#token-class) | A function to generate a token with a value. <br>This will be used for tokens where it is imperative that the value is carried over to the target language. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Tokens** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[Token](#token-class)\> | The list of tokens generated when the source language is being scanned |
# InvalidSyntaxException Class

Namespace: Lexer.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

This exception will be thrown when the scanner or parser finds unexpexted syntax errors

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
| **InvalidSyntaxException(string message)** | The constructor of the exception, taking only one parameter. |
| **InvalidSyntaxException(string message, [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) innerException)** | The constructor of the exception, taking two parameters. |
# Keywords Class

Namespace: Lexer.Objects

A class containing a dictionary of keywords

## Fields

| Name | Type | Summary |
|---|---|---|
| **Keys** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<string, [TokenType](#tokentype-enum)\> | A dictionary of the possible keywords. |
# Token Class

Namespace: Lexer.Objects

A class representing a token in the source language.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | [TokenType](#tokentype-enum) | The type of the token |
| **Value** | string | The value of the token. |
| **Line** | int | The line the token is placed on |
| **Offset** | int | The offset of the value for the token |
## Constructors

| Name | Summary |
|---|---|
| **Token([TokenType](#tokentype-enum) type, int line, int offset)** | The constructor of a token with no value such as Operator tokens |
| **Token([TokenType](#tokentype-enum) type, string val, int line, int offset)** | The constructor for a token. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **ToString()** | string | A function to format tokens, when printed to the screen or in other ways used as a string |
# TokenType Enum

Namespace: Lexer.Objects

An enum to determine the token type of a given token

## Values

| Name | Summary |
|---|---|
| **ASSIGN** | Assignment token type |
| **SIZE_OF** | Size of function token type |
| **CALL** | Call token type |
| **FUNC** | Function token type |
| **END** | End token type |
| **ARRAYLEFT** | Left array initialisation token type |
| **ARRAYRIGHT** | Right array initialisation token type |
| **ARRAYINDEX** | Array index accessor token type |
| **FOR** | For loop token type |
| **WHILE** | While loop token type |
| **IF** | If statement token type |
| **ELSE** | Else statement token type |
| **OP_GREATER** | Bool greater than operator token type |
| **OP_EQUAL** | Bool equality operator token type |
| **OP_LESS** | Bool less than operator token type |
| **OP_AND** | Bool and token type |
| **OP_OR** | Bool or token type |
| **OP_NOT** | Bool not token type |
| **OP_QUESTIONMARK** | Bool questionmark than token type |
| **DPIN** | Predefined digital pin token type |
| **APIN** | Predefined analogue pin token type |
| **LOOP_FN** | Mandatory loop function token type |
| **COMMENT** | Comment token type |
| **MULT_COMNT** | Multiline comment token type |
| **NUMERIC_INT** | Numeric integer token type |
| **NUMERIC_FLOAT** | Numeric float token type |
| **STRING** | String token type |
| **BOOL** | Bool token type |
| **RANGE** | Range token type |
| **OP_PLUS** | Plus token type |
| **OP_MINUS** | Minus token type |
| **OP_TIMES** | Multiplication token type |
| **OP_DIVIDE** | Divide token type |
| **OP_MODULO** | Modulo token type |
| **OP_LPAREN** | Left parenthesis token type |
| **OP_RPAREN** | Right parenthesis token type |
| **VAR** | Variable token type |
| **VAL** | Value token type |
| **ERROR** | Error token type |
