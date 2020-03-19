Current CFG
```
Start -> Stmnts.

Stmnts -> Stmnt Stmnts .
Stmnts -> .

Stmnt -> Identifier is Expr Trmnl  .
Stmnt -> BeginStmnt Trmnl .
Stmnt -> IfStmnt Trmnl .
Stmnt -> Function Trmnl .


Expr -> Numeric .
Expr -> Numeric' MathExpr .
Expr -> Value' BoolExpr .
Expr -> String .
Expr -> Pin .
Expr -> ArrInit .

MathExpr -> OP_PLUS Numeric .
MathExpr -> OP_MINUS Numeric .
MathExpr -> OP_TIMES Numeric .
MathExpr -> OP_DIVIDE Numeric .
MathExpr -> OP_MODULO Numeric .

OP_PLUS -> + .
OP_MINUS -> - .
OP_TIMES -> * .
OP_DIVIDE -> / .
OP_MODULO -> % .

LeftArr -> [ .

RightArr -> ] .

ArrInit -> LeftArr Numeric RightArr .

BoolExpr -> OP_GREATER Value .
BoolExpr -> OP_EQUAL Value .
BoolExpr -> OP_LESS Value .
BoolExpr -> OP_AND Value .
BoolExpr -> OP_OR Value .

OP_EQUAL -> equal .
OP_AND -> and .
OP_OR -> or .
OP_LESS -> less .
OP_GREATER -> greater .

IfStmnt -> if Value BoolExpr .

Pin -> DPin Numeric.
Pin -> APin Numeric.

FuncCall -> Func Identifier with ArgList Terminal .

ArgList -> .
ArgList -> Type Identifier ArgList .

Function -> FuncCall CodeBlock EndFunc .

CodeBlock -> Stmnts .

EndFunc -> End Func .

BeginStmnt -> Begin .

Begin -> LoopF .
Begin -> LoopW .

LoopW -> While BoolExpr Do CodeBlock EndWhile .

EndWhile -> End While .

LoopF -> For Identifier In Range Do CodeBlock EndFor .

EndFor -> End For .

Range -> Identifier LeftArr Numeric to Numeric RightArr .
Range -> LeftArr Numeric to Numeric RightArr .

Value -> Identifier .
Value -> Numeric .

Trmnl -> LINEBREAK .
```
