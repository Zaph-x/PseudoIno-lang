# CodeGeneration.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

# All types

|   |   |   |
|---|---|---|
| [CodeGenerationVisitor Class](#codegenerationvisitor-class) | [InvalidCodeException Class](#invalidcodeexception-class) |   |
# CodeGenerationVisitor Class

Namespace: CodeGeneration

Base class: Visitor


## Properties

| Name | Type | Summary |
|---|---|---|
| **HasError** | bool |  |
| **Header** | string |  |
| **Declarations** | string |  |
| **Global** | string |  |
| **Prototypes** | string |  |
| **Setup** | string |  |
| **Funcs** | string |  |
| **Loop** | string |  |
| **PWM** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> |  |
| **FileName** | string |  |
## Constructors

| Name | Summary |
|---|---|
| **CodeGenerationVisitor(string fileName, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> pwm)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **PrintStringToFile()** | void |  |
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
| **Visit(ForNode forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(FuncNode funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(GreaterNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(GreaterOrEqualNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(IfStatementNode ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(LessNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(LessOrEqualNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(PlusNode plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(MinusNode minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ModuloNode moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(OrNode orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(StringNode stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(WhileNode whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ElseStatementNode elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ElseifStatementNode elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ReturnNode returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ExpressionTerm expressionTermNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(BinaryExpression noParenExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ParenthesisExpression parenthesisExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(BoolNode boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ArrayNode arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **Visit(ArrayAccessNode arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
# InvalidCodeException Class

Namespace: CodeGeneration.Exceptions

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
| **InvalidCodeException(string message)** |  |
