Current CFG
```
Start -> Stmnts.

Stmnts -> Stmnt Stmnts .
Stmnts -> Trmnl .

Stmnt -> Identifier is Expr .
Stmnt -> BeginStmnt .
Stmnt -> IfStmnt .
Stmnt -> Function .


Expr -> Numeric .
Expr -> Numeric' MathExpr .
Expr -> Value' BoolExpr .
Expr -> String .
Expr -> Pin .
Expr -> ArrInit .

MathExpr -> MathOp Numeric .

MathOp -> + .
MathOp -> - .
MathOp -> * .
MathOp -> / .
MathOp -> % .

LeftArr -> [ .

RightArr -> ] .

ArrInit -> LeftArr Numeric RightArr .

BoolExpr -> BoolOp Value .

BoolOp -> EQL .
BoolOp -> AND .
BoolOp -> OR .
BoolOp -> LE .
BoolOp -> GR .
BoolOp -> LEQ .
BoolOp -> GRQ .

IfStmnt -> if BoolExpr .

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
