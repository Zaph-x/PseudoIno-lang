# AbstractSyntaxTree.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 27/05/2020

# All types

|   |   |   |
|---|---|---|
| [AstNode Class](#astnode-class) | [ElseifStatementNode Class](#elseifstatementnode-class) | [OrNode Class](#ornode-class) |
| [IAssginable Class](#iassginable-class) | [ElseStatementNode Class](#elsestatementnode-class) | [ParenthesisExpression Class](#parenthesisexpression-class) |
| [IAssignment Class](#iassignment-class) | [EqualNode Class](#equalnode-class) | [PinNode Class](#pinnode-class) |
| [IScope Class](#iscope-class) | [ExpressionNode Class](#expressionnode-class) | [PlusNode Class](#plusnode-class) |
| [ITyped Class](#ityped-class) | [ExpressionTerm Class](#expressionterm-class) | [ProgramNode Class](#programnode-class) |
| [ParseContext Class](#parsecontext-class) | [ForNode Class](#fornode-class) | [ReturnNode Class](#returnnode-class) |
| [PrettyPrinter Class](#prettyprinter-class) | [FuncNode Class](#funcnode-class) | [StatementNode Class](#statementnode-class) |
| [Visitor Class](#visitor-class) | [GreaterNode Class](#greaternode-class) | [StringNode Class](#stringnode-class) |
| [AndNode Class](#andnode-class) | [GreaterOrEqualNode Class](#greaterorequalnode-class) | [TimeHourNode Class](#timehournode-class) |
| [APinNode Class](#apinnode-class) | [IExpr Class](#iexpr-class) | [TimeMillisecondNode Class](#timemillisecondnode-class) |
| [ArrayAccessNode Class](#arrayaccessnode-class) | [IfStatementNode Class](#ifstatementnode-class) | [TimeMinuteNode Class](#timeminutenode-class) |
| [ArrayNode Class](#arraynode-class) | [ITerm Class](#iterm-class) | [TimeNode Class](#timenode-class) |
| [AssignmentNode Class](#assignmentnode-class) | [LessNode Class](#lessnode-class) | [TimeSecondNode Class](#timesecondnode-class) |
| [BinaryExpression Class](#binaryexpression-class) | [LessOrEqualNode Class](#lessorequalnode-class) | [TimesNode Class](#timesnode-class) |
| [BoolNode Class](#boolnode-class) | [MathOperatorNode Class](#mathoperatornode-class) | [ValNode Class](#valnode-class) |
| [BoolOperatorNode Class](#booloperatornode-class) | [MinusNode Class](#minusnode-class) | [VarNode Class](#varnode-class) |
| [CallNode Class](#callnode-class) | [ModuloNode Class](#modulonode-class) | [WaitNode Class](#waitnode-class) |
| [DivideNode Class](#dividenode-class) | [NumericNode Class](#numericnode-class) | [WhileNode Class](#whilenode-class) |
| [DPinNode Class](#dpinnode-class) | [OperatorNode Class](#operatornode-class) |   |
# AstNode Class

Namespace: AbstractSyntaxTree.Objects

The AST node class containin the information about the construction of an AST node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **AstNode(ScannerToken token)** | The base constructor for the AST nodes, operator, statement, time, val nodes |
| **AstNode(TokenType type, ScannerToken token)** | The constructor for an AST node. This constructor is used for the terminal statement node |
| **AstNode(TokenType type, int line, int offset)** | The base constructor for an AST nodes array, operator, program and statement nodes |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **IsType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool | A Boolean method that checks if the instance of type can be assigned to the current type of the token |
| **ToString()** | string | A method that converts the type of the token to a string |
# IAssginable Class

Namespace: AbstractSyntaxTree.Objects

Using this interface implements an ID on pins and and an accept method for visiting the nodes

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string | The Identification of the Pins. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# IAssignment Class

Namespace: AbstractSyntaxTree.Objects

Using this interface, implments an accept method for an assignment
The assignment for the right side of the assignment

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# IScope Class

Namespace: AbstractSyntaxTree.Objects

Using this interface, get and set the value of the statementlist
It is used when scopes are opend and closed

## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | Get the list of statement. |
# ITyped Class

Namespace: AbstractSyntaxTree.Objects

Using this interface properties for checking if the type is checked

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of the token |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
# ParseContext Class

Namespace: AbstractSyntaxTree.Objects

This class get and set the list of arrays

## Properties

| Name | Type | Summary |
|---|---|---|
| **DeclaredArrays** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[ArrayNode](#arraynode-class)\> | A list of declared arrays which is then inserted into a list of arraynodes |
# PrettyPrinter Class

Namespace: AbstractSyntaxTree.Objects

Base class: [Visitor](#visitor-class)

This class prints the AST nodes
It inherits the visitor class to implement the visit method for each node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Indent** | int | This creat indentation on the outprinted AST<br>When a new scope is created, the an indentation is made  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Visit([TimesNode](#timesnode-class) timesNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the time node and make an indentation |
| **Visit([AssignmentNode](#assignmentnode-class) assignmentNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the assignmentNode and make an indentation<br>It accepts the lefthand side and righthands side of the assignment<br>Then outdent |
| **Visit([WaitNode](#waitnode-class) waitNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the waitNode and make an indentation<br>It accepts a visit of TimeAmount and Timemodifier<br>Then outdent |
| **Visit([VarNode](#varnode-class) varNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the varNode  |
| **Visit([TimeSecondNode](#timesecondnode-class) timeSecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the timeSecondNode  |
| **Visit([TimeMinuteNode](#timeminutenode-class) timeMinuteNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the timeMinueNode  |
| **Visit([TimeMillisecondNode](#timemillisecondnode-class) timeMillisecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the timeMillisecondNode  |
| **Visit([TimeHourNode](#timehournode-class) timeHourNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the timeHourNode  |
| **Visit([NumericNode](#numericnode-class) numericNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the numericNode  |
| **Visit([EqualNode](#equalnode-class) equalNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the equalNode  |
| **Visit([ProgramNode](#programnode-class) programNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the programNode and make an indentation<br>Then if there exist any function definitions or statements in the program, they will be accepted<br>Then it make outdent |
| **Visit([CallNode](#callnode-class) callNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the callNode and make an indentation<br>It accepts the ID number of the node and all the parameters of the call node<br>Then outdent |
| **Visit([AndNode](#andnode-class) andNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the andNode  |
| **Visit([APinNode](#apinnode-class) apinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the apinNode  |
| **Visit([DPinNode](#dpinnode-class) dpinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the dpinNode  |
| **Visit([DivideNode](#dividenode-class) divideNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the divideNode  |
| **Visit([ForNode](#fornode-class) forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the forNode and make an indentation<br>It accepts the counting variable, the value which it count from and the variable it counts to<br>Then it accepts all the statements in the for loop node<br>Last it outdent |
| **Visit([FuncNode](#funcnode-class) funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the funcNode and make an indentation<br>It accepts the statements, the name of the function and the parameters |
| **Visit([GreaterNode](#greaternode-class) greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the greaterNode  |
| **Visit([IfStatementNode](#ifstatementnode-class) ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the ifStatementNode and make an indentation<br>It accepts Expresstion if there is any and also accepts all ifStatement nodes<br>Then outdent |
| **Visit([LessNode](#lessnode-class) lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the lessNode  |
| **Visit([PlusNode](#plusnode-class) plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the plusNode  |
| **Visit([MinusNode](#minusnode-class) minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the minusNode  |
| **Visit([ModuloNode](#modulonode-class) moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the moduloNode  |
| **Visit([OrNode](#ornode-class) orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the orNode  |
| **Visit([StringNode](#stringnode-class) stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the stringNode  |
| **Visit([WhileNode](#whilenode-class) whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the whileNode and make an indentation<br>It accepts expressions and all statements in the while node<br>Then outdent |
| **Visit([ElseStatementNode](#elsestatementnode-class) elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the elseStatement and make an indentation<br>It then accepts all the statement and then outdent |
| **Visit([ElseifStatementNode](#elseifstatementnode-class) elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the elseifStatementNode and make an indentation<br>It accepts the Val and Expressions if there exist any<br>Then it accepts all the statements and then lastly outdent |
| **Visit([ReturnNode](#returnnode-class) returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the returnNode and make an indentation<br>Then accepts the value of the return<br>Then it outdent |
| **Visit([GreaterOrEqualNode](#greaterorequalnode-class) greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the greaterNode  |
| **Visit([LessOrEqualNode](#lessorequalnode-class) lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the lessNode  |
| **Visit([ExpressionTerm](#expressionterm-class) expressionTermNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the expressionTermNode and make an indentation<br>Then accepts the lefthand of the expression and then outdent |
| **Visit([BinaryExpression](#binaryexpression-class) binaryExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the binaryExpresstion and make an indentation<br>It accepts the lef-and righthandside of the expresstion<br>It accepts the operator if there exist any<br>Then it outdent |
| **Visit([ParenthesisExpression](#parenthesisexpression-class) parenthesisExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the paraenthesisExpression and then make an indentation<br>It accepts the lef-and righthandside of the expresstion<br>It accepts the operator if there exist any<br>Then it outdent |
| **Visit([BoolNode](#boolnode-class) boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the boolNode  |
| **Visit([ArrayNode](#arraynode-class) arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the arrayNode and make an indentation<br>Then it accepts all dimensions of the array<br>Then it outdent |
| **Visit([ArrayAccessNode](#arrayaccessnode-class) arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method prints the arrayAccess and make an indentation<br>Then it accepts all the array accesses <br>Then outdent |
# Visitor Class

Namespace: AbstractSyntaxTree.Objects

The base visitor class

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Visit([TimesNode](#timesnode-class) timesNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method visits a Time node |
| **Visit([AssignmentNode](#assignmentnode-class) assignmentNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This vitits a Assignment node |
| **Visit([WaitNode](#waitnode-class) waitNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Wait node |
| **Visit([VarNode](#varnode-class) varNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Var node |
| **Visit([TimeSecondNode](#timesecondnode-class) timeSecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a TimeSecond node |
| **Visit([TimeMinuteNode](#timeminutenode-class) timeMinuteNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a TimeMinute node |
| **Visit([TimeMillisecondNode](#timemillisecondnode-class) timeMillisecondNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits TimeMilliSecond node |
| **Visit([TimeHourNode](#timehournode-class) timeHourNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a TimeHour node |
| **Visit([NumericNode](#numericnode-class) numericNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Numeric node |
| **Visit([EqualNode](#equalnode-class) equalNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an Equal node |
| **Visit([ProgramNode](#programnode-class) programNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Program node |
| **Visit([CallNode](#callnode-class) callNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Call node |
| **Visit([AndNode](#andnode-class) andNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an And node |
| **Visit([APinNode](#apinnode-class) apinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an APin node |
| **Visit([DPinNode](#dpinnode-class) dpinNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a DPin node |
| **Visit([DivideNode](#dividenode-class) divideNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Divide node |
| **Visit([ForNode](#fornode-class) forNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a For node |
| **Visit([FuncNode](#funcnode-class) funcNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Func node |
| **Visit([GreaterNode](#greaternode-class) greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Greater node |
| **Visit([GreaterOrEqualNode](#greaterorequalnode-class) greaterNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Greater or equal node |
| **Visit([IfStatementNode](#ifstatementnode-class) ifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an If statement node |
| **Visit([LessNode](#lessnode-class) lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a less node |
| **Visit([LessOrEqualNode](#lessorequalnode-class) lessNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a less or equal node |
| **Visit([PlusNode](#plusnode-class) plusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a plus node |
| **Visit([MinusNode](#minusnode-class) minusNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a minus node |
| **Visit([ModuloNode](#modulonode-class) moduloNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Modulo node |
| **Visit([OrNode](#ornode-class) orNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an Or node |
| **Visit([StringNode](#stringnode-class) stringNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a string node |
| **Visit([WhileNode](#whilenode-class) whileNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a while node |
| **Visit([ElseStatementNode](#elsestatementnode-class) elseStatement)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an Else statement |
| **Visit([ElseifStatementNode](#elseifstatementnode-class) elseifStatementNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an ElseIf statement |
| **Visit([ReturnNode](#returnnode-class) returnNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Return node |
| **Visit([ExpressionTerm](#expressionterm-class) expressionTermNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an ExpressionTerm node |
| **Visit([BinaryExpression](#binaryexpression-class) noParenExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an no Paranthesis Expression node |
| **Visit([ParenthesisExpression](#parenthesisexpression-class) parenthesisExpression)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Paranthesis Expression node |
| **Visit([BoolNode](#boolnode-class) boolNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits a Boolean node |
| **Visit([ArrayNode](#arraynode-class) arrayNode)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an Array node |
| **Visit([ArrayAccessNode](#arrayaccessnode-class) arrayAccess)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This visits an Array Access node  |
# AndNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

The class for And node
inherits math operator node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **AndNode(ScannerToken token)** | The constructor for And node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# APinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [PinNode](#pinnode-class)

The class for Analog pin node
Inherits from the pin node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string | This sets and returns the ID |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **APinNode(string pinNum, ScannerToken token)** | The constructor for APin node<br>Id is set to the pin number |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ArrayAccessNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

The array access node class
It inherits Ast node
It implements IAssignment, IExpr and ITerm interfaces

## Properties

| Name | Type | Summary |
|---|---|---|
| **Accesses** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[ValNode](#valnode-class)\> | This is a list of accessers of the array with the type Valnode |
| **Actual** | [ArrayNode](#arraynode-class) | This is the actual array |
| **LeftHand** | [ITerm](#iterm-class) | Inerited property not used |
| **Operator** | [OperatorNode](#operatornode-class) | Inerited property not used |
| **RightHand** | [IExpr](#iexpr-class) | Inerited property not used |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ArrayAccessNode([ArrayNode](#arraynode-class) array, int line, int offset)** | The constructor for Array access nodes<br>Actual is the actual array and has been accessed is set to true when visited. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ArrayNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

The Array node class
It inherits Ast node
It uses IAssignment, IExpr and ITerm interfaces

## Properties

| Name | Type | Summary |
|---|---|---|
| **Dimensions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[NumericNode](#numericnode-class)\> | is a list of the dimensions of the array, of the type numeric node |
| **ActualId** | [VarNode](#varnode-class) | The Id of the array |
| **FirstAccess** | [AssignmentNode](#assignmentnode-class) | This sets and returns the first access of the assignment node |
| **HasBeenAccessed** | bool | This sets and returns the boolean value of has been accessed |
| **LeftHand** | [ITerm](#iterm-class) | Inerited property not used |
| **Operator** | [OperatorNode](#operatornode-class) | Inerited property not used |
| **RightHand** | [IExpr](#iexpr-class) | Inerited property not used |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ArrayNode(int line, int offset)** | This is the constructor of array node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **_firstAccess** | [AssignmentNode](#assignmentnode-class) | This is the first access of the array |
# AssignmentNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the assignment node class
It inherits statement node and the expression interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **RightHand** | [IExpr](#iexpr-class) | This is the right hand side of an expresstion |
| **LeftHand** | [ITerm](#iterm-class) | This is the lefthand side of an expression, which is the term  |
| **Operator** | [OperatorNode](#operatornode-class) | This is returns and sets the operator |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **AssignmentNode(int line, int offset)** | This is the constructor for the assignment node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# BinaryExpression Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ExpressionNode](#expressionnode-class)

This is the class for binary expression
It inherits an expression node and the expression interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) | Returns and sets the left hands side of the expression |
| **Operator** | [OperatorNode](#operatornode-class) | Returns and sets the operater of the expression |
| **RightHand** | [IExpr](#iexpr-class) | Returns and sets the right hand side of the expression |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) | This returns and sets the value of the child to the expression |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **BinaryExpression(ScannerToken token)** | This is the constructor of binaryesxpressions |
| **BinaryExpression(TokenType type, ScannerToken token)** | This is the constructor for binaryexpressions |
| **BinaryExpression(int line, int offset)** | This is the constructor for binaryexpressions |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string | This method converts the left, right and operator of the expression to a string |
# BoolNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)

This is the node class for bool node
It inherits a value node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Value** | bool | This returns and set s a value |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **BoolNode(string value, ScannerToken token)** | This is the constructor of the bool node<br>It set the value to a bool |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# BoolOperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [OperatorNode](#operatornode-class)

This is the booloperator node class
It inherits from the operator node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **BoolOperatorNode(ScannerToken token)** | This is the bool operator node constructor. |
| **BoolOperatorNode(TokenType type, int line, int offset)** | This is the constructor for bool operator node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# CallNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the class for call node
It inherits statement node and implements assignment, expression and term interfaces

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | [VarNode](#varnode-class) | This is the ID of the var node |
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[ValNode](#valnode-class)\> | This is a list of the parameters of the val node |
| **LeftHand** | [ITerm](#iterm-class) | This is the lefthand side of an expression |
| **Operator** | [OperatorNode](#operatornode-class) | This is the operator |
| **RightHand** | [IExpr](#iexpr-class) | This is the righthand side of an expression |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **CallNode(int line, int offset)** | This is the constructor for a call node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# DivideNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the divide node class
It inherits math operator node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **DivideNode(ScannerToken token)** | This is the constructor for divide node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# DPinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [PinNode](#pinnode-class)

This is the DPin node class
It inherits the Pin node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string | This sets and returns the ID |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **DPinNode(string pinNum, ScannerToken token)** | This is the DPin constructor<br>The ID is set to the pin number |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ElseifStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the else if statement node class
It inherits from statement class and implements the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Val** | [ValNode](#valnode-class) | This returns the value and sets the value  |
| **Expression** | [ExpressionNode](#expressionnode-class) | This returns and sets the expression |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This returns and sets a list of statements |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ElseifStatementNode(int line, int offset)** | This is the constructor for else if statements<br>The statements is assigned to a list of statement |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ElseStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the else statement node class
Is inherits from statement node class and implements the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This returns and sets the list of statement |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ElseStatementNode(int line, int offset)** | This is the constructor for else statements<br>The statements is assigned to a list of type statement |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# EqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)

This is the equal node class
It inherits the bool operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **EqualNode(ScannerToken token)** | This is the constructor for equal node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ExpressionNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the expression node class
It inherits the statement node class and the assignment, term and expression interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **_Parent** | [ExpressionNode](#expressionnode-class) | This returns and set the value of the expression node parent |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) | This returns and sets the value of the child to the expression |
| **LeftHand** | [ITerm](#iterm-class) | This return and sets the value of the lefthand sideof the expression |
| **Operator** | [OperatorNode](#operatornode-class) | This return and sets the value of the operator |
| **RightHand** | [IExpr](#iexpr-class) | This return and sets the value of the righthand sideof the expression |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ExpressionTerm Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ExpressionNode](#expressionnode-class)

This is the expression term node
It inherits from the expression node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) | This returns and sets the value of the child to the expression |
| **LeftHand** | [ITerm](#iterm-class) | This return and sets the value of the lefthand sideof the expression |
| **Operator** | [OperatorNode](#operatornode-class) | This return and sets the value of the operator |
| **RightHand** | [IExpr](#iexpr-class) | This return and sets the value of the righthand sideof the expression |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ExpressionTerm(ScannerToken token)** | This is the constructor for expression term node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ForNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the class for For loop node
It inherits the statement node class and implement the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **CountingVariable** | [VarNode](#varnode-class) | This sets and returns the counter variable of the for loop |
| **From** | [NumericNode](#numericnode-class) | This sets and returns the from value of the for loop |
| **To** | [NumericNode](#numericnode-class) | This sets and returns the to value of the for loop |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This sets and returns the list of statments |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ForNode(int line, int offset)** | This is the constructor for a for loop<br>Statements is assigned to a list of type statementnode  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# FuncNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the class for func node
It inherits the statement node class and implement the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This sets and returns a list of statement nodes |
| **Name** | [VarNode](#varnode-class) | This sets and returns the name of var node |
| **FunctionParameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[VarNode](#varnode-class)\> | This sets and returns the function parameters with the type var node |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **FuncNode(int line, int offset)** | This is the constructor for func node<br>Statement is assigned to a list of statementnodes |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# GreaterNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the greater node class
It inherits the math operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **GreaterNode(ScannerToken token)** | This is the greater node constructor |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# GreaterOrEqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)

This is the class for greater or equal node class
It inherits the bool operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **GreaterOrEqualNode([OperatorNode](#operatornode-class) node)** | This is the constructor for greater or equal node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# IExpr Class

Namespace: AbstractSyntaxTree.Objects.Nodes

This is the interface for an expresstion
In implements the interface of typed

## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) | This sets and returns the value of the lefthand of an expression |
| **Operator** | [OperatorNode](#operatornode-class) | This sets and returns the value of operator node |
| **RightHand** | [IExpr](#iexpr-class) |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This accept the object  |
| **IsType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool | This checks if it has been typed checked |
# IfStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the if statement node class
It inherits a statement class and implements the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Expression** | [ExpressionNode](#expressionnode-class) | This sets and returns the value of expression node |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This sets and returns the value of the list of expression node |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **IfStatementNode(int line, int offset)** | This is the constructor for if statements<br>Statements is assigned to a list of statement |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ITerm Class

Namespace: AbstractSyntaxTree.Objects.Nodes

This is the interface for terms of expressions
In implements the interface typed

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | This method accepts the visited node |
| **IsType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool | This method checks if the node has been type checked |
# LessNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the class for less node
It inherits the math operator node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **LessNode(ScannerToken token)** | This is the construcor for less node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# LessOrEqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)

This is the class for less or equal node
It inherits the bool operator node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **LessOrEqualNode([OperatorNode](#operatornode-class) node)** | This is the contructor for the less or equal node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# MathOperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [OperatorNode](#operatornode-class)

This is the math operator node class
It inherits the operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **MathOperatorNode(ScannerToken token)** | This is the constructor for math operator |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# MinusNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the minus node class
It inherits the math operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **MinusNode(ScannerToken token)** | This is the constructor for minus node  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ModuloNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the class for modulo node
It inherits the math operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ModuloNode(ScannerToken token)** | This is the constructor for modulo node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# NumericNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)

This is the numeric node class
It inherits the val node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **FValue** | float | This is sets and returns the floating point value  |
| **IValue** | int | This sets and returns the integer value |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **NumericNode(string value, ScannerToken token)** | This is the constructor for numeric node<br>It enables the program to use period seperator for floating points <br>FValue is set to be floating points and IValue is set to be integer |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# OperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

This is the operator node class
It inherits the Ast node class 

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **OperatorNode(ScannerToken token)** | This is the constructor for operator node |
| **OperatorNode(TokenType type, int line, int offset)** | This is the constructor for operator node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# OrNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the or node class
It inherits the mathoperator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **OrNode(ScannerToken token)** | This is the constructor for or node  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ParenthesisExpression Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ExpressionNode](#expressionnode-class)

This is the class for parenthesis expression node
It inherits the expression node class and implements the expression interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) | This sets and returns the value of lefthand side of expressions |
| **Operator** | [OperatorNode](#operatornode-class) | This sets and returns the value of operators |
| **RightHand** | [IExpr](#iexpr-class) | This sets and returns the value of right hands side of expressions |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) | This returns and sets the value of the child to the expression |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ParenthesisExpression(int line, int offset)** | This is the constructor of parenthesis espressions |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string |  |
# PinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)

This is the Pin node class
It inherits the val node class and the assignable interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string | This sets and returns the ID |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **PinNode(ScannerToken token)** | this is the constructor of Pin node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# PlusNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the plus node class
It inherits from the math operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **PlusNode(ScannerToken token)** | This is the constructor for plus node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ProgramNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

This is the program node class
It inherits the Ast node class and implements the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This sets and returns a list of statements of the type statementnode |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ProgramNode(int line, int offset)** | This is the constructor for program node<br>Statements is assigned to a list of statement nodes |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **FunctionDefinitons** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[FuncNode](#funcnode-class)\> | This sets and returns a list of function definitions with the type funcnode |
| **LoopFunction** | [FuncNode](#funcnode-class) | This is the loopfunction |
# ReturnNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the return node class
It inherits from statement node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **ReturnValue** | [ExpressionNode](#expressionnode-class) | This sets and returns the value for retunr value |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ReturnNode(int line, int offset)** | This is the constructor for return node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# StatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

This is the class for statement node
It inherits the Ast node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **StatementNode(ScannerToken token)** | This is the constructor for statement node |
| **StatementNode(TokenType type, ScannerToken token)** | This is the constructor for statement node |
| **StatementNode(TokenType type, int line, int offset)** | This is the constructor for statement node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# StringNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)

This is the string node class
It inherits from val node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Value** | string | This sets and returns the value |
| **Type** | TokenType | The type of token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **StringNode(string value, ScannerToken token)** | This is the constructor for string node<br>Value is assigned to value |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeHourNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)

This is the class time hour node
It inherits from time node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimeHourNode(ScannerToken token)** | This is the time hour node constructor |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeMillisecondNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)

This is the time millisecond node class
It inherits from the time node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimeMillisecondNode(ScannerToken token)** | This is the constructor for time millisecond node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeMinuteNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)

This is the time minute node class
It inherits from time node

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimeMinuteNode(ScannerToken token)** | This is the constructor for time minuse node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

This is the time node class
It inherits from the Ast node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimeNode(ScannerToken token)** | This is the constructor for time node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeSecondNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)

This is the time second node class
It inherits from the time node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimeSecondNode(ScannerToken token)** | This is the constructor for time second node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimesNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)

This is the times node class
It inherits from the math operator node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **TimesNode(ScannerToken token)** | This is the constructor for times node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ValNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)

This is the val node class
It inherits the Ast node class and the term interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **ValNode(ScannerToken token)** | This is the constructor for val node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# VarNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)

This is the var node class
It inherits from val node class and implements the assignable interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string | This sets and returns the value of the ID |
| **Declaration** | bool | This sets and returns the boolean value of decleration, |
| **IsArray** | bool | This sets and returns the boolean value for isarray |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **VarNode(string id, ScannerToken token)** | This is the constructor for var node<br>Id is assigned to id |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string | This method converts the ID to a string |
# WaitNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is teh wait node class
It inherits the statement node class

## Properties

| Name | Type | Summary |
|---|---|---|
| **TimeModifier** | [TimeNode](#timenode-class) | This sets and returns the time modifier with the type time node |
| **TimeAmount** | [NumericNode](#numericnode-class) | This sets and returns the time amount with the type numeric node |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **WaitNode(int line, int offset)** | This is the constructor for wait node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# WhileNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)

This is the whilenode class
It inherits the statementnode and implements the scope interface

## Properties

| Name | Type | Summary |
|---|---|---|
| **Expression** | [ExpressionNode](#expressionnode-class) | This sets and returns the value of the expression |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> | This sets and returns the value of the list of statements |
| **Type** | TokenType | The type of token. |
| **Value** | string | Value of the token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **WhileNode(int line, int offset)** | This is the while node constructor<br>Statements are assigned to a list of statementnodes |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
