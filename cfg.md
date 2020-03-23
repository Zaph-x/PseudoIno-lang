Prog -> Stmts .

Type -> string
| numeric
| bool
| array
| Pin.

Stmts -> Stmt Stmts
| .

FuncStmts -> FuncStmt FuncStmts
| .

Stmt -> id assign Assignment
| IfStmnt
| FuncCall .

FuncStmt -> id assign Val Expr
| IfStmnt 
| BeginStmt .

Assignment -> Val Expr
| [numeric] Arr .

Expr -> Math_Op Val Expr
| Bool_Op Val Expr
| .

Math_Op -> +
| -
| *
| /
| % .

Bool_Op -> equals
| and
| or
| Greater OrEqual 
| Less OrEqual .

OrEqual -> Or Equal
| .

Val -> id
| numeric
| string
| Pin .

Arr -> [numeric] Arr
| .

Pin -> dpin
| apin .

IfStmnt -> if Val Bool_Op Val Epxr FuncStmts end if.

FuncCall -> func id Optnl_Args  FuncStmts end id.

BeginStmt -> begin Beginable.

Beginable -> LoopW
| LoopF.

LoopW -> while Val BoolExpr do FuncStmts end while .

LoopF -> For id In Range do FuncStmts end for.

Optnl_Args -> with Arg ArgList
| .

ArgList -> Arg ArgList 
| .

Arg -> Type id.
