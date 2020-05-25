# SymbolTable.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

# All types

|   |   |   |
|---|---|---|
| [Symbol Class](#symbol-class) | [SymbolTableObject Class](#symboltableobject-class) |   |
| [SymbolTableBuilder Class](#symboltablebuilder-class) | [SymbolNotFoundException Class](#symbolnotfoundexception-class) |   |
# Symbol Class

Namespace: SymbolTable


## Properties

| Name | Type | Summary |
|---|---|---|
| **Name** | string |  |
| **TokenType** | TokenType |  |
| **AstNode** | AstNode |  |
| **IsRef** | bool |  |
## Constructors

| Name | Summary |
|---|---|
| **Symbol(string name, TokenType type, bool isRef, AstNode astNode)** |  |
# SymbolTableBuilder Class

Namespace: SymbolTable


## Constructors

| Name | Summary |
|---|---|
| **SymbolTableBuilder([SymbolTableObject](#symboltableobject-class) global)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **AddArray(ArrayNode arrNode)** | void |  |
| **AddRef(AstNode node)** | void |  |
| **AddSymbol(AstNode node)** | void |  |
| **CloseScope()** | void |  |
| **GetNameFromRef(AstNode node)** | string |  |
| **OpenScope(TokenType type, string name)** | void |  |
## Fields

| Name | Type | Summary |
|---|---|---|
| **CurrentSymbolTable** | [SymbolTableObject](#symboltableobject-class) |  |
| **GlobalSymbolTable** | [SymbolTableObject](#symboltableobject-class) |  |
| **TopOfScope** | [Stack](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)\<[SymbolTableObject](#symboltableobject-class)\> |  |
# SymbolTableObject Class

Namespace: SymbolTable

Symbol table node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Children** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[SymbolTableObject](#symboltableobject-class)\> |  |
| **_parent** | [SymbolTableObject](#symboltableobject-class) |  |
| **FunctionDefinitions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<FuncNode\> |  |
| **FunctionCalls** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<CallNode\> |  |
| **DeclaredArrays** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<ArrayNode\> |  |
| **PredefinedFunctions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<FuncNode\> |  |
| **Parent** | [SymbolTableObject](#symboltableobject-class) |  |
| **Type** | TokenType |  |
| **Name** | string |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **AddCallReference(CallNode call)** | void |  |
| **FindArray(string arrName)** | ArrayNode |  |
| **FindChild(string name)** | [SymbolTableObject](#symboltableobject-class) |  |
| **FindSymbol(VarNode var)** | TypeContext |  |
| **GetEnclosingFunction()** | [SymbolTableObject](#symboltableobject-class) |  |
| **HasDeclaredVar(AstNode node)** | bool |  |
| **IsInFunction()** | bool |  |
| **ToString()** | string |  |
| **UpdateFuncVar(VarNode node, TypeContext rhs, string scopeName)** | void |  |
| **UpdateTypedef(VarNode leftHand, TypeContext rhs, string scopeName, bool goback)** | void |  |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Symbols** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[Symbol](#symbol-class)\> |  |
| **DeclaredVars** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> |  |
# SymbolNotFoundException Class

Namespace: SymbolTable.Exceptions

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
| **SymbolNotFoundException(string message)** |  |
