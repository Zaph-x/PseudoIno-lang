# Parser.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

# All types

|   |   |   |
|---|---|---|
| [DivisionByZeroException Class](#divisionbyzeroexception-class) | [Parser Class](#parser-class) | [TokenStream Class](#tokenstream-class) |
| [InvalidTokenException Class](#invalidtokenexception-class) | [ParseAction Class](#parseaction-class) |   |
| [NoScopeException Class](#noscopeexception-class) | [ParseTable Class](#parsetable-class) |   |
# DivisionByZeroException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

Raised when a number is divided by zero.

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
| **DivisionByZeroException(string message)** | Raised if a number is divided by zero. This does not take variables into account. |
# InvalidTokenException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

This method is raised when a token is invalid

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
| **InvalidTokenException(string message)** | Raised when a token is unexpected and thus invalid |
# NoScopeException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

This exception is raised when a scope is not defined

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
| **NoScopeException(string message)** | This exception is raised when a scope is not found or defined |
# Parser Class

Namespace: Parser

This is the main class for the parser. Here be parsing.

## Properties

| Name | Type | Summary |
|---|---|---|
| **ParseTable** | [ParseTable](#parsetable-class) | The parse table used to parse. |
| **HasError** | bool | A static boolean representing the error state of the parser. |
| **Root** | ProgramNode | The root of the AST |
| **CurrentAction** | [ParseAction](#parseaction-class) | The current action to use in the parser. This has a number and a set of production rules to follow. |
| **_builder** | SymbolTableBuilder | The symboltable builder used to insert items into the symbol table |
## Constructors

| Name | Summary |
|---|---|
| **Parser([List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<ScannerToken\> tokens)** | The constructor for the parser.<br>Here the parsetable is created, along with the global scope being set up.<br>Furthermore, the tokens are being passed from the scanner. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **AddToAstNode(TokenType token)** | void | This will add a new nonterminal to the AST.<br>This is done by adding the nonterminal to the AST.<br>The type of terminal is decided based on the current parse action.<br>This method is also responsible for opening and closing scopes when encountered in the parser. |
| **DecorateAstNode(ScannerToken token)** | void | This method is used to decorate the AST. This is done from the production rules of the ParseAction.<br>This is done by using a switch case to determine which node to add to what parent node. |
| **GetOrEqualNode(OperatorNode node)** | OperatorNode | This method will convert a numeric comparison operator to a Greater or Equal, or a Less or Equal node. |
| **Parse(out string verbosity)** | void | The main method of the parser, which parses the provided tokens. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Scopes** | [Stack](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)\<AstNode\> | The scope stack, to put items into the correct scopes. |
# ParseAction Class

Namespace: Parser.Objects

A transition rule in the parse table

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | int | The current action type signified by an integer value |
| **Product** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<TokenType\> | The product of the transition. This is set in the parse table |
## Constructors

| Name | Summary |
|---|---|
| **ParseAction(int type, TokenType[] types)** | The constructor of a parse action. This contains the type and the production list. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Error()** | [ParseAction](#parseaction-class) | Returns an ERROR transition. Used when initialising the parse table. |
| **ToString()** | string |  |
# ParseTable Class

Namespace: Parser.Objects

The parse table of the parser. This is where the parse rules are set.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Table** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<TokenType, [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<TokenType, [ParseAction](#parseaction-class)\>\> | This is the table of the parser. This is where all transition rules are stored. |
| **Item** | [ParseAction](#parseaction-class) | Access of table using scanner tokens |
| **Item** | [ParseAction](#parseaction-class) | Access of table using tokentypes |
## Constructors

| Name | Summary |
|---|---|
| **ParseTable()** | The constructor for the parse table. Here the table is initialised and created. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **InitTable()** | void | This method fills in all the transition rules in the table. |
# TokenStream Class

Namespace: Parser.Objects

A stream of tokens that can not be indexed, but rather peeked or popped.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Index** | int | The index of the underlying list |
| **Length** | int | The length of the token stream |
| **EOF** | ScannerToken | A token signifying the end of file (EOF) |
| **PROG** | ScannerToken | A token signifying the start of a program (PROG) |
## Constructors

| Name | Summary |
|---|---|
| **TokenStream([IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<ScannerToken\> tokens)** | Constructs a tokenstream. This is what is used in the parser, to parse from |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Advance()** | void | Advances the stream one step |
| **AtEnd()** | bool | Checks if the stream has reached the end. |
| **Current()** | ScannerToken | This method gets the current token of the current location in the stream. |
| **Peek()** | ScannerToken | Peeks the next token in the stream |
| **Peek(int lookAhead)** | ScannerToken | This method peeks with a lookahead |
| **Prev()** | void | Steps back in the stream |
