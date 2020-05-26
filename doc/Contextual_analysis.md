# Contextual_analysis.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

# All types

|   |   |   |
|---|---|---|
| [TypeChecker Class](#typechecker-class) | [InvalidTypeException Class](#invalidtypeexception-class) | [NotDefinedException Class](#notdefinedexception-class) |
| [InvalidReturnException Class](#invalidreturnexception-class) | [MultipleDefinedException Class](#multipledefinedexception-class) | [OutOfRangeException Class](#outofrangeexception-class) |
# TypeChecker Class

Namespace: Contextual_analysis

Base class: Visitor

The type checker of the PseudoIno compiler. Here types are checked and held against eachother.

## Properties

| Name | Type | Summary |
|---|---|---|
| **HasError** | bool | A static bool value signifying an error has been encountered |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **IsOfTypes(TypeContext ctx, TokenType[] types)** | bool | This method compares a type context to a list of tokentypes |
| **Visit(TimesNode timesNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the TimesNode node in the AST. |
| **Visit(AssignmentNode assignmentNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the AssignmentNode node in the AST. |
| **Visit(WaitNode waitNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the WaitNode node in the AST. |
| **Visit(VarNode varNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the VarNode node in the AST. |
| **Visit(TimeSecondNode timeSecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the TimeSecondNode node in the AST. |
| **Visit(TimeMinuteNode timeMinuteNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the TimeMinutesNode node in the AST. |
| **Visit(TimeMillisecondNode timeMillisecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the TimeMillisecondNode node in the AST. |
| **Visit(TimeHourNode timeHourNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the TimeHourNode node in the AST. |
| **Visit(NumericNode numericNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the NumericNode node in the AST. |
| **Visit(EqualNode equalNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the EqualNode node in the AST. |
| **Visit(ProgramNode programNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ProgramNode node in the AST.<br>This is the entry point for the type checker |
| **Visit(CallNode callNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the CallNode node in the AST. |
| **Visit(AndNode andNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the AndNode node in the AST. |
| **Visit(APinNode apinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the APinNode node in the AST. |
| **Visit(DPinNode dpinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the DPinNode node in the AST. |
| **Visit(DivideNode divideNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the DivideNode node in the AST. |
| **Visit(BinaryExpression expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the BinaryExpression node in the AST. |
| **Visit(ParenthesisExpression expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ParenthesisExpression node in the AST. |
| **Visit(ExpressionTerm expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ExpressionTerm node in the AST. |
| **Visit(ForNode forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ForNode node in the AST. |
| **Visit(FuncNode funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the FuncNode node in the AST. |
| **Visit(GreaterNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the GreaterNode node in the AST. |
| **Visit(IfStatementNode ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the IfStatementNode node in the AST. |
| **Visit(LessNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the LessNode node in the AST. |
| **Visit(PlusNode plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the PlusNode node in the AST. |
| **Visit(MinusNode minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the MinusNode node in the AST. |
| **Visit(ModuloNode moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ModuloNode node in the AST. |
| **Visit(OrNode orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the OrNode node in the AST. |
| **Visit(StringNode stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the StringNode node in the AST. |
| **Visit(WhileNode whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the WhileNode node in the AST. |
| **Visit(ElseStatementNode elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ElseStatementNode node in the AST. |
| **Visit(ElseifStatementNode elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ElseIfStatementNode node in the AST. |
| **Visit(ReturnNode returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ReturnNode node in the AST. |
| **Visit(GreaterOrEqualNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the GreaterOrEqualNode node in the AST. |
| **Visit(LessOrEqualNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the LessOrEqualNode node in the AST. |
| **Visit(BoolNode boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the BoolNode node in the AST. |
| **Visit(ArrayNode arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ArrayNode node in the AST. |
| **Visit(ArrayAccessNode arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method type checks the ArrayAccessNode node in the AST. |
# InvalidReturnException Class

Namespace: Contextual_analysis.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception thrown when a range is invalid

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
| **InvalidReturnException(string message)** | An exception thrown when a range is invalid |
# InvalidTypeException Class

Namespace: Contextual_analysis.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

Raised when a type does not match its counterpart

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
| **InvalidTypeException(string message)** | Raised in the typechecker when two types to not match |
# MultipleDefinedException Class

Namespace: Contextual_analysis.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception raised when two functions with same amount of parameters and name has been defined

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
| **MultipleDefinedException(string message)** | An exception raised when two functions with the same amount of parameters and the same name are defined. This is handled in the typechecker |
# NotDefinedException Class

Namespace: Contextual_analysis.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception to raise when something is used but not defined

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
| **NotDefinedException(string message)** | An exception to raise when something is not defined but still used. This is handled in the typechecker |
# OutOfRangeException Class

Namespace: Contextual_analysis.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

An exception to raise when an array index is out of range

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
| **OutOfRangeException(string message)** | An exception to raise when an array is indexed with an index that is out of range. |
