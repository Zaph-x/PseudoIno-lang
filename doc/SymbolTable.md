# SymbolTable.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 27/05/2020

# All types

|   |   |   |
|---|---|---|
| [Symbol Class](#symbol-class) | [SymbolTableObject Class](#symboltableobject-class) |   |
| [SymbolTableBuilder Class](#symboltablebuilder-class) | [SymbolNotFoundException Class](#symbolnotfoundexception-class) |   |
# Symbol Class

Namespace: SymbolTable

The symbol object being passed to the symbol table

## Properties

| Name | Type | Summary |
|---|---|---|
| **Name** | string | The name of the symbol |
| **TokenType** | TokenType | The type of the symbol |
| **AstNode** | AstNode | The AST node the symbol is representing |
| **IsRef** | bool | Is the symbol a reference |
## Constructors

| Name | Summary |
|---|---|
| **Symbol(string name, TokenType type, bool isRef, AstNode astNode)** | The constructor of a symbol |
# SymbolTableBuilder Class

Namespace: SymbolTable

A builder for the symboltable

## Constructors

| Name | Summary |
|---|---|
| **SymbolTableBuilder([SymbolTableObject](#symboltableobject-class) global)** | The constructor of the symboltable builder. Here the global scope is set |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **AddArray(ArrayNode arrNode)** | void | This method will add an array to the current scope |
| **AddRef(AstNode node)** | void | This method will add a reference to a node |
| **AddSymbol(AstNode node)** | void | This method will add an AST node to the current scope |
| **CloseScope()** | void | This method will close a scope, and update the current scope |
| **GetNameFromRef(AstNode node)** | string | This method will get the name of a reference |
| **OpenScope(TokenType type, string name)** | void | A method that opens a new scope and symbol table. This symboltable is marked as a child of the current scope |
## Fields

| Name | Type | Summary |
|---|---|---|
| **CurrentSymbolTable** | [SymbolTableObject](#symboltableobject-class) | The current symbol table |
| **GlobalSymbolTable** | [SymbolTableObject](#symboltableobject-class) | The global symbol table |
| **TopOfScope** | [Stack](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)\<[SymbolTableObject](#symboltableobject-class)\> | A stack containin each parent scope of the current scope |
# SymbolTableObject Class

Namespace: SymbolTable

The symbol table class object

## Properties

| Name | Type | Summary |
|---|---|---|
| **Children** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[SymbolTableObject](#symboltableobject-class)\> | The child scopes of the symbol table |
| **_parent** | [SymbolTableObject](#symboltableobject-class) | The parent scope of the current scope |
| **FunctionDefinitions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<FuncNode\> | A static list of function definitions |
| **FunctionCalls** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<CallNode\> | A static list of function calls to remove unused functions |
| **DeclaredArrays** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<ArrayNode\> | A list of declared arrays in a given scope |
| **PredefinedFunctions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<FuncNode\> | A static list of predefined functions |
| **Parent** | [SymbolTableObject](#symboltableobject-class) | The parent scope of the current scope |
| **Type** | TokenType | The type of the scope. This is set on creation |
| **Name** | string | The name of the scope to search for, when looking for a child scope |
## Constructors

| Name | Summary |
|---|---|
| **SymbolTableObject()** | The constructor for a symboltable. This will set predefined functions and constants |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **FindArray(string arrName)** | ArrayNode | This method finds an array in the declared array of the current scope. |
| **FindChild(string name)** | [SymbolTableObject](#symboltableobject-class) | This method will find a child scope given the name of it |
| **FindSymbol(VarNode var)** | TypeContext | This method try to find a variable recursively, in the current scope and its parents |
| **GetEnclosingFunction()** | [SymbolTableObject](#symboltableobject-class) | Gets the enclosing function scope, of the current scope |
| **HasDeclaredVar(AstNode node)** | bool | This method will check if a given scope has declared a variable. |
| **IsInFunction()** | bool | Check if a given scope is a child of a function. |
| **ToString()** | string |  |
| **UpdateFuncVar(VarNode node, TypeContext rhs, string scopeName)** | void | This method will update function parameters of an enclosing function |
| **UpdateTypedef(VarNode leftHand, TypeContext rhs, string scopeName, bool goback)** | void | This method will update a type definition for a variable in all scopes |
## Fields

| Name | Type | Summary |
|---|---|---|
| **Symbols** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[Symbol](#symbol-class)\> | The list of symbols in the symboltable |
| **DeclaredVars** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> | The declared variables in the current scope |
# SymbolNotFoundException Class

Namespace: SymbolTable.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception raised if a symbol is not found in the symbol table

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
| **SymbolNotFoundException(string message)** | The constructor of the exception to raise if a symbol is not found |
