# Lexer.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 27/05/2020

# All types

|   |   |   |
|---|---|---|
| [Recogniser Class](#recogniser-class) | [Keywords Class](#keywords-class) | [TokenTypeExpressions Class](#tokentypeexpressions-class) |
| [Tokeniser Class](#tokeniser-class) | [ScannerToken Class](#scannertoken-class) | [TypeContext Class](#typecontext-class) |
| [InvalidSyntaxException Class](#invalidsyntaxexception-class) | [Token Class](#token-class) |   |
| [InvalidTypeException Class](#invalidtypeexception-class) | [TokenType Enum](#tokentype-enum) |   |
# Recogniser Class

Namespace: Lexer

The class responsible for recognising characters in the language

## Methods

| Name | Returns | Summary |
|---|---|---|
| **IsAcceptedCharacter(char character)** | bool | A function that checks if a char is a character in the alphabet. |
| **IsDigit(char character)** | bool | A function to check if a char is a digit. This is done by calling IsBetween. |
| **IsKeyword(string input)** | bool | A function that checks if a given string is a keyword. |
# Tokeniser Class

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
| **HasError** | bool | A bool value to check if the tokeniser found any illegal syntax |
| **ParenthesisStack** | [Stack](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)\<[ScannerToken](#scannertoken-class)\> |  |
## Constructors

| Name | Summary |
|---|---|
| **Tokeniser([StreamReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader) stream)** | The constructor for the tokeniser class. This will set the iniitiate a reader and a recogniser. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **GenerateTokens()** | void | A function to to generate tokens. This is done by reading from the stream and using any of the scan functions. |
| **Peek()** | char | Peeks the next character in the stream and sets NextChar to the value |
| **Peek(int positions)** | char | Peeks the nth character in the stream and sets NextChar to the value of the character |
| **Pop()** | char | Sets current character as the next character in the stream and advances in the stream.<br>This is also responsible for counting up lines and the offset, to provide context for error handling. |
| **Token([TokenType](#tokentype-enum) type)** | [ScannerToken](#scannertoken-class) | A function to generate a token without a value.<br>This will be used to generate tokens where the value does not have to be carried over to the target language |
| **Token([TokenType](#tokentype-enum) type, string val)** | [ScannerToken](#scannertoken-class) | A function to generate a token with a value. <br>This will be used for tokens where it is imperative that the value is carried over to the target language. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Tokens** | [LinkedList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1)\<[ScannerToken](#scannertoken-class)\> | The list of tokens generated when the source language is being scanned |
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
# InvalidTypeException Class

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
| **InvalidTypeException(string message)** | The constructor of the exception, taking only one parameter. |
# Keywords Class

Namespace: Lexer.Objects

A class containing a dictionary of keywords

## Fields

| Name | Type | Summary |
|---|---|---|
| **Keys** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<string, [TokenType](#tokentype-enum)\> | A dictionary of the possible keywords. |
# ScannerToken Class

Namespace: Lexer.Objects

Base class: [Token](#token-class)

Scanner token class. Inherits from Token class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | [TokenType](#tokentype-enum) | The type of the token |
| **Value** | string | The value of the token. |
| **Line** | int | The line the token is placed on |
| **Offset** | int | The offset of the value for the token |
| **SymbolicType** | [TypeContext](#typecontext-class) | The type of the token that is later used in the typechecker. |
## Constructors

| Name | Summary |
|---|---|
| **ScannerToken([TokenType](#tokentype-enum) type, int line, int offset)** | The constructor of a token with no value such as Operator tokens |
| **ScannerToken([TokenType](#tokentype-enum) type, string val, int line, int offset)** | The constructor for a token. |
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
| **SymbolicType** | [TypeContext](#typecontext-class) | The type of the token that is later used in the typechecker. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **GetHashCode()** | int | Overrides the GetHashCode method and returns the value of the enum |
| **ToString()** | string | A function to format tokens, when printed to the screen or in other ways used as a string |
# TokenType Enum

Namespace: Lexer.Objects

An enum to determine the token type of a given token

## Values

| Name | Summary |
|---|---|
| **BEGIN** | Begin token, terminal |
| **ASSIGN** | Assignment token type |
| **CALL** | Call token type |
| **FUNC** | Function token type |
| **END** | End token type |
| **ARRAYLEFT** | Left array initialisation token type |
| **ARRAYRIGHT** | Right array initialisation token type |
| **ARRAYINDEX** | Array index accessor token type |
| **ARRAYACCESSING** | Array accessor token type |
| **INDEXER** | Array indexer token type |
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
| **OP_OREQUAL** | Bool or equal token type |
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
| **WAIT** | Wait token type |
| **TIME_MS** | Millisecond token type |
| **TIME_SEC** | Second token type |
| **TIME_MIN** | Minute token type |
| **TIME_HR** | Hour token type |
| **WITH** | With token, terminal |
| **EQUALS** | Equals token, terminal |
| **DO** | Do token, terminal |
| **IN** | In token, terminal |
| **NUMERIC** | Numeric token, terminal |
| **NEWLINE** | Newline token, terminal |
| **EOF** | End of file token, terminal |
| **PROG** | Program token, non terminal |
| **START** | Start token, non terminal |
| **STMNTS** | Statements token, non terminal |
| **STMNT** | Statement token, non terminal |
| **EXPR** | Expression token, non terminal |
| **MATHEXPR** | Math expression token, non terminal |
| **MATH_OP** | Math operation token, non terminal |
| **ARRINIT** | Array init token, non terminal |
| **BOOLEXPR** | Boolean expression token, non terminal |
| **BOOL_OP** | Boolean operation token, non terminal |
| **IFSTMNT** | If statement token, non terminal |
| **ELSESTMNT** | ELSE statement token, non terminal |
| **ELSEIFSTMNT** | Else If statement token, non terminal |
| **PIN** | Pin token, non terminal |
| **FUNCCALL** | Function call token, non terminal |
| **FUNCDECL** | Function declaration token, non terminal |
| **ARGLIST** | Argument list token, non terminal |
| **FUNCTION** | Function token, non terminal |
| **CODEBLOCK** | Code block token, non terminal |
| **ENDFUNC** | End function token, non terminal |
| **BEGINSTMNT** | Begin statement token, non terminal |
| **LOOPW** | Loop while token, non terminal |
| **LOOPF** | Loop for token, non terminal |
| **ENDWHILE** | End while token, non terminal |
| **ENDFOR** | End for token, non terminal |
| **ARRAYACCESSOR** | Accessor Value for Arrays |
| **ASSIGNMENT** | Assignment token, non terminal |
| **TYPE** | Type token, non terminal |
| **DECLPARAM** | Declaration parameter token, terminal |
| **DECLPARAMS** | Optional arguments token, terminal |
| **BEGINABLE** | Beginable token, non terminal |
| **ARR** | Array token, non terminal |
| **CALLPARAM** | First paramter in function call, non terminal |
| **CALLPARAMS** | Additional parameters in function call, non terminal |
| **WAITSTMNT** | Wait statement, non terminal |
| **EPSILON** | Epsilon transition, non terminal |
| **OP_RANGE** | Range operator, terminal |
| **TIME_MOD** | Time modeifer, non terminal |
| **SEPARATOR** | Comman seperator, terminal |
| **RETURN** | return token type |
| **RETSTMNT** | Nonterminal for return statement |
| **ENDIF** | Endif nonterminal token type |
| **NT_COMMENT** | Nonterminal comment token type |
| **ASSIGNSTMNT** | Assignment statement nonterminal token type |
| **TERM** | Term token type |
| **FOLLOWTERM** | follow term for expression token type |
| **FACTOR** | Factor for expression token type |
| **FOLLOWFACTOR** | Followfactor for expressions token type |
| **TERMOP** | Operator token type for terms |
| **FACTOROP** | Factor operator token type |
| **OP_GEQ** | Greater or equal token type |
| **OP_LEQ** | Less or equal token type |
| **ERROR** | Error token type |
# TokenTypeExpressions Class

Namespace: Lexer.Objects

A class with static classes to operate on token types.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **IsBlock([TokenType](#tokentype-enum) type)** | bool |  |
| **IsNonTerminal([TokenType](#tokentype-enum) type)** | bool | This function will determine if a given token is a non-terminal |
| **IsOperator([TokenType](#tokentype-enum) type)** | bool | Determines whether a token is an operator |
| **IsTerminal([TokenType](#tokentype-enum) type)** | bool | This function will determine if a given token is terminal |
# TypeContext Class

Namespace: Lexer.Objects

The type context used in the program to check types in the typechecker

## Properties

| Name | Type | Summary |
|---|---|---|
| **_tokenType** | [TokenType](#tokentype-enum) | The tokentype of the |
| **Type** | [TokenType](#tokentype-enum) | A getter for the private type property |
| **_float** | bool | A boolean value signifying if a numeric is a float |
| **IsFloat** | bool | Getter setter property specifying if a value is a float. |
## Constructors

| Name | Summary |
|---|---|
| **TypeContext([TokenType](#tokentype-enum) type)** | The constructor for the typecontext |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Equals([Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) obj)** | bool |  |
| **ToString()** | string |  |
