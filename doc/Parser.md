# Parser.dll v.1.0.0.0 API documentation

# All types

|   |   |   |
|---|---|---|
| [DivisionByZeroException Class](#divisionbyzeroexception-class) | [Parser Class](#parser-class) | [TokenStream Class](#tokenstream-class) |
| [InvalidTokenException Class](#invalidtokenexception-class) | [ParseAction Class](#parseaction-class) |   |
| [NoScopeException Class](#noscopeexception-class) | [ParseTable Class](#parsetable-class) |   |
# DivisionByZeroException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)


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
| **DivisionByZeroException(string message)** |  |
# InvalidTokenException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)


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
| **InvalidTokenException(string message)** |  |
# NoScopeException Class

Namespace: 

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)


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
| **NoScopeException(string message)** |  |
# Parser Class

Namespace: Parser


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
| **GetOrEqualNode(OperatorNode node)** | OperatorNode |  |
| **Parse(out string verbosity)** | void | The main method of the parser, which parses the provided tokens. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Scopes** | [Stack](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)\<AstNode\> | The scope stack, to put items into the correct scopes. |
# ParseAction Class

Namespace: Parser.Objects


## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | int |  |
| **Product** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<TokenType\> |  |
## Constructors

| Name | Summary |
|---|---|
| **ParseAction(int type, TokenType[] types)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Error()** | [ParseAction](#parseaction-class) |  |
| **ToString()** | string |  |
# ParseTable Class

Namespace: Parser.Objects


## Properties

| Name | Type | Summary |
|---|---|---|
| **Table** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<TokenType, [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<TokenType, [ParseAction](#parseaction-class)\>\> |  |
| **Item** | [ParseAction](#parseaction-class) |  |
| **Item** | [ParseAction](#parseaction-class) |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **InitTable()** | void |  |
# TokenStream Class

Namespace: Parser.Objects


## Properties

| Name | Type | Summary |
|---|---|---|
| **Index** | int |  |
| **Length** | int |  |
| **EOF** | ScannerToken |  |
| **PROG** | ScannerToken |  |
## Constructors

| Name | Summary |
|---|---|
| **TokenStream([IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<ScannerToken\> tokens)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Advance()** | void |  |
| **AtEnd()** | bool |  |
| **Current()** | ScannerToken |  |
| **Peek()** | ScannerToken |  |
| **Peek(int lookAhead)** | ScannerToken |  |
| **Prev()** | void |  |
