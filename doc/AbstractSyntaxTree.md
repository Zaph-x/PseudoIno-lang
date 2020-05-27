# AbstractSyntaxTree.dll v.1.0.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 25/05/2020

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
| **AndNode(ScannerToken token)** | The constructor for the And node |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# APinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [PinNode](#pinnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string |  |
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
| **APinNode(string pinNum, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ArrayAccessNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Accesses** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[ValNode](#valnode-class)\> |  |
| **Actual** | [ArrayNode](#arraynode-class) |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
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
| **ArrayAccessNode([ArrayNode](#arraynode-class) array, int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ArrayNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Dimensions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[NumericNode](#numericnode-class)\> |  |
| **ActualId** | [VarNode](#varnode-class) |  |
| **FirstAccess** | [AssignmentNode](#assignmentnode-class) |  |
| **HasBeenAccessed** | bool |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
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
| **ArrayNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **_firstAccess** | [AssignmentNode](#assignmentnode-class) |  |
# AssignmentNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **RightHand** | [IExpr](#iexpr-class) |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
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
| **AssignmentNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# BinaryExpression Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ExpressionNode](#expressionnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) |  |
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
| **BinaryExpression(ScannerToken token)** |  |
| **BinaryExpression(TokenType type, ScannerToken token)** |  |
| **BinaryExpression(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string |  |
# BoolNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Value** | bool |  |
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
| **BoolNode(string value, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# BoolOperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [OperatorNode](#operatornode-class)


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
| **BoolOperatorNode(ScannerToken token)** |  |
| **BoolOperatorNode(TokenType type, int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# CallNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | [VarNode](#varnode-class) |  |
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[ValNode](#valnode-class)\> |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
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
| **CallNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# DivideNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **DivideNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# DPinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [PinNode](#pinnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string |  |
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
| **DPinNode(string pinNum, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ElseifStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Val** | [ValNode](#valnode-class) |  |
| **Expression** | [ExpressionNode](#expressionnode-class) |  |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **ElseifStatementNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ElseStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **ElseStatementNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# EqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)


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
| **EqualNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ExpressionNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **_Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
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


## Properties

| Name | Type | Summary |
|---|---|---|
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) |  |
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
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
| **ExpressionTerm(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ForNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **CountingVariable** | [VarNode](#varnode-class) |  |
| **From** | [NumericNode](#numericnode-class) |  |
| **To** | [NumericNode](#numericnode-class) |  |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **ForNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# FuncNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
| **Name** | [VarNode](#varnode-class) |  |
| **FunctionParameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[VarNode](#varnode-class)\> |  |
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
| **FuncNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# GreaterNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **GreaterNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# GreaterOrEqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)


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
| **GreaterOrEqualNode([OperatorNode](#operatornode-class) node)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# IExpr Class

Namespace: AbstractSyntaxTree.Objects.Nodes


## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **IsType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool |  |
# IfStatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Expression** | [ExpressionNode](#expressionnode-class) |  |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **IfStatementNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ITerm Class

Namespace: AbstractSyntaxTree.Objects.Nodes


## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) |  |
| **IsType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool |  |
# LessNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **LessNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# LessOrEqualNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [BoolOperatorNode](#booloperatornode-class)


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
| **LessOrEqualNode([OperatorNode](#operatornode-class) node)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# MathOperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [OperatorNode](#operatornode-class)


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
| **MathOperatorNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# MinusNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **MinusNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ModuloNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **ModuloNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# NumericNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **FValue** | float |  |
| **IValue** | int |  |
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
| **NumericNode(string value, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# OperatorNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


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
| **OperatorNode(ScannerToken token)** |  |
| **OperatorNode(TokenType type, int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# OrNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **OrNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ParenthesisExpression Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ExpressionNode](#expressionnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **LeftHand** | [ITerm](#iterm-class) |  |
| **Operator** | [OperatorNode](#operatornode-class) |  |
| **RightHand** | [IExpr](#iexpr-class) |  |
| **Parent** | [ExpressionNode](#expressionnode-class) |  |
| **Child** | [ExpressionNode](#expressionnode-class) |  |
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
| **ParenthesisExpression(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string |  |
# PinNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string |  |
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
| **PinNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# PlusNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **PlusNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ProgramNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **ProgramNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **FunctionDefinitons** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[FuncNode](#funcnode-class)\> |  |
| **LoopFunction** | [FuncNode](#funcnode-class) |  |
# ReturnNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **ReturnValue** | [ExpressionNode](#expressionnode-class) |  |
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
| **ReturnNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# StatementNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


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
| **StatementNode(ScannerToken token)** |  |
| **StatementNode(TokenType type, ScannerToken token)** |  |
| **StatementNode(TokenType type, int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# StringNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Value** | string |  |
| **Type** | TokenType | The type of token. |
| **SymbolType** | TypeContext | Symboltype helps the typechecker set the type of symbols |
| **Line** | int | The line of the token |
| **Offset** | int | The offset of the value for the token |
| **Visited** | bool | Determans if the node has been visited |
| **Parent** | [AstNode](#astnode-class) | Set the node as a parent node. A parent node have children |
## Constructors

| Name | Summary |
|---|---|
| **StringNode(string value, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeHourNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)


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
| **TimeHourNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeMillisecondNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)


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
| **TimeMillisecondNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeMinuteNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)


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
| **TimeMinuteNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


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
| **TimeNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimeSecondNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [TimeNode](#timenode-class)


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
| **TimeSecondNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# TimesNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [MathOperatorNode](#mathoperatornode-class)


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
| **TimesNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# ValNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [AstNode](#astnode-class)


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
| **ValNode(ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# VarNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [ValNode](#valnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Id** | string |  |
| **Declaration** | bool |  |
| **IsArray** | bool |  |
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
| **VarNode(string id, ScannerToken token)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
| **ToString()** | string |  |
# WaitNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **TimeModifier** | [TimeNode](#timenode-class) |  |
| **TimeAmount** | [NumericNode](#numericnode-class) |  |
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
| **WaitNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
# WhileNode Class

Namespace: AbstractSyntaxTree.Objects.Nodes

Base class: [StatementNode](#statementnode-class)


## Properties

| Name | Type | Summary |
|---|---|---|
| **Expression** | [ExpressionNode](#expressionnode-class) |  |
| **Statements** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[StatementNode](#statementnode-class)\> |  |
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
| **WhileNode(int line, int offset)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Accept([Visitor](#visitor-class) visitor)** | [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) | An accept method that accepts the AST node in the tree when the node is visited. |
