# CodeGeneration.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 27/05/2020

# All types

|   |   |   |
|---|---|---|
| [CodeGenerationVisitor Class](#codegenerationvisitor-class) | [InvalidCodeException Class](#invalidcodeexception-class) |   |
# CodeGenerationVisitor Class

Namespace: CodeGeneration

Base class: Visitor

This is the class for the codegeneration visitor
It inherits from the visitor class

## Properties

| Name | Type | Summary |
|---|---|---|
| **HasError** | bool | This sets and return the boolean value for has error |
| **Header** | string | This set and returns the value of the string for the header file |
| **Declarations** | string | This set and returns the value for the string of declaration |
| **Global** | string | This set and returns the value of the string of global variables |
| **Prototypes** | string | This set and returns the value of the string of prototypes for all functions(C and C++ feature) |
| **Setup** | string | This sets and returns the value of the strig for setup function |
| **Funcs** | string | This sets and returns the value of the string for functions  |
| **Loop** | string | This sets and returns the value of the string for loops |
| **PWM** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> | This sets and returns the value of the list for PWM pins |
| **FileName** | string | This set and returns the value of the string for file names |
## Constructors

| Name | Summary |
|---|---|
| **CodeGenerationVisitor(string fileName, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<string\> pwm)** | This is the constructor for the code generation visitor |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **PrintStringToFile()** | void | This method writes header, global, prototypes, declarations, setup, functions and loops in a ".cpp" file<br>It creats a list of unique pin definitions |
| **Visit(TimesNode timesNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits the times node |
| **Visit(AssignmentNode assignmentNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method first checks if the left side of the assignment is a Dpin, and Apin has the same input as output<br>Create the Apin or Dpin as the lefthand side is accepted<br>If it's a PWM pin, its an Apin otherwise its a Dpin<br>It then accepts the righthand side and if its numeric, the value is accepted<br>It can also be assigned to true, high or low<br>The method can also accept the righthand side is an array |
| **Visit(WaitNode waitNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a wait node<br>First assigns a string that prints delay, and then accepts the amount of time and the time modifier<br>Then closes the parenthesis |
| **Visit(VarNode varNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This methods visits a var node<br>If the var is a numeric it's checked if it is a float or integer<br>Then it's checked if it's a bool or string var<br>The var is assigned to an ID |
| **Visit(TimeSecondNode timeSecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a time second node <br>the result is multiplied by 1000 to convert it to milliseconds |
| **Visit(TimeMinuteNode timeMinuteNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a time minute node <br>the result is multiplied by 60000 to convert it to milliseconds |
| **Visit(TimeMillisecondNode timeMillisecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a time milleseconds node |
| **Visit(TimeHourNode timeHourNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a time minute node<br>the result is multiplied by 3600000 to convert it to milliseconds |
| **Visit(NumericNode numericNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a numeric node<br>If the value is modulo 1 is not 0 then the float or integer value is converted to a string |
| **Visit(EqualNode equalNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits the equal node |
| **Visit(ProgramNode programNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a program node<br>The headers files are assigned to Header<br>It checks that the function definitions and function calls Id and parameter count corresponds to the given Id and parameter count<br>The Id and parameters are retreived from the symbol table<br>The void setup is set in the global scope<br>It then accepts statements, assignments and declerations<br>It then makes the setup function<br>Then it accepts loop functions and create loop function<br>Finally it calls the method to write to the .cpp file |
| **Visit(CallNode callNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits the call node<br>It loops through the number of input parameters and prints them to the callString<br>The callString can be called by setup or loop |
| **Visit(AndNode andNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an and node |
| **Visit(APinNode apinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an Apin node<br>It checks if the analog pin is defined or is a part of an expression<br>If its a part of an expression it prints the Apin and the ID |
| **Visit(DPinNode dpinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an Apin node<br>It checks if the digital pin is defined or is a part of an expression<br>If its a part of an expression it prints the Dpin and the ID |
| **Visit(DivideNode divideNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a divide node |
| **Visit(ForNode forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a for loop node<br>It first checks count value start is smaller then the amount of loop required and then inserts the symbols<br>It either increments or decrements<br>The second if statements accepts the statements in the forloop |
| **Visit(FuncNode funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a function node<br>The switch case checks for the function type<br>Then the input parameters are accepted<br>Then the function is assigned to a new scope and the statements are accepted |
| **Visit(GreaterNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a greater node |
| **Visit(GreaterOrEqualNode greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a greater or equal node |
| **Visit(IfStatementNode ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an if statement node<br>It first write "if"<br>Then it checks if there is an espression to accepts<br>Then it accepts the statements |
| **Visit(LessNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a less node |
| **Visit(LessOrEqualNode lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a less or equal node |
| **Visit(PlusNode plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a plus node |
| **Visit(MinusNode minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a minus node |
| **Visit(ModuloNode moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a modulo node |
| **Visit(OrNode orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an or node |
| **Visit(StringNode stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a string node |
| **Visit(WhileNode whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a while node<br>It first writes "while and accepts the expression<br>Then it accepts the any statements |
| **Visit(ElseStatementNode elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an else statement node<br>If accepts any statements  |
| **Visit(ElseifStatementNode elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an elseif statement node<br>It checks if there is an expression to accepts<br>Then it accepts any statement |
| **Visit(ReturnNode returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This node visits a return node<br>It accetps the return value and write "return, the value and add a semicolom  |
| **Visit(ExpressionTerm expressionTermNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an expressionterm node<br>First it checks the type on the lefthand side of the expression is either Apin or Dpin<br>If also checks if an input is attempted to be uses as an output <br>The input for Apin or Dpin is then accepted and written to the file<br>Lastly the lefthand side of the expression is accepted |
| **Visit(BinaryExpression noParenExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a binary expression with no parenthesis<br>It accepts the left and right hand side and checks if there is an operator to accept also |
| **Visit(ParenthesisExpression parenthesisExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a parenthesis espression<br>It accepts the left and right hand side and checks if there is an operator to accept also |
| **Visit(BoolNode boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a bool node<br>It checks if the value is either true or false |
| **Visit(ArrayNode arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visit an array node<br>It checks for the dementions in the array and assign the value to the dementions |
| **Visit(ArrayAccessNode arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits an array access node<br>It checks what array is being accessed, and accesses it with the according dimensional access. |
# InvalidCodeException Class

Namespace: CodeGeneration.Exceptions

Base class: [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)

Raised when a piece of code is invalid

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
| **InvalidCodeException(string message)** | Raised when a combination is undersired in the code generator |
