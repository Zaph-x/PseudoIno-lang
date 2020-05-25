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


## Properties

| Name | Type | Summary |
|---|---|---|
| **HasError** | bool |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **IsOfTypes(TypeContext ctx, TokenType[] types)** | bool |  |
| **Visit(TimesNode timesNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(AssignmentNode assignmentNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(WaitNode waitNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(VarNode varNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(TimeSecondNode timeSecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(TimeMinuteNode timeMinuteNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(TimeMillisecondNode timeMillisecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(TimeHourNode timeHourNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(NumericNode numericNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(EqualNode equalNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ProgramNode programNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(CallNode callNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(AndNode andNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(APinNode apinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(DPinNode dpinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(DivideNode divideNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(BinaryExpression expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ParenthesisExpression expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ExpressionTerm expressionNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ForNode forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(FuncNode funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(GreaterNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(IfStatementNode ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(LessNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(PlusNode plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(MinusNode minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ModuloNode moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(OrNode orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(StringNode stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(WhileNode whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ElseStatementNode elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ElseifStatementNode elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ReturnNode returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(GreaterOrEqualNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(LessOrEqualNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(BoolNode boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ArrayNode arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ArrayAccessNode arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
# InvalidReturnException Class

Namespace: Contextual_analysis.Exceptions

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
| **InvalidReturnException(string message)** |  |
# InvalidTypeException Class

Namespace: Contextual_analysis.Exceptions

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
| **InvalidTypeException(string message)** |  |
# MultipleDefinedException Class

Namespace: Contextual_analysis.Exceptions

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
| **MultipleDefinedException(string message)** |  |
# NotDefinedException Class

Namespace: Contextual_analysis.Exceptions

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
| **NotDefinedException(string message)** |  |
# OutOfRangeException Class

Namespace: Contextual_analysis.Exceptions

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
| **OutOfRangeException(string message)** |  |
