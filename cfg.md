```
Prog -> Stmts .

Type -> 'string'
| 'numeric'
| 'bool'
| 'array'
| Pin .

Stmts -> Stmt Stmts
| FuncDecl Stmnts
| .

Stmt -> ID 'is' Assignment
| IfStmnt
| FuncCall
| WaitStmnt
| BeginStmt .

Assignment -> Val Expr
| '[' Numeric ']' Arr .

Expr -> Math_Op Val Expr
| Bool_Op Val Expr
| .

Math_Op -> '+'
| '-'
| '*'
| '/'
| '%' .

Bool_Op -> 'equals'
| 'and'
| 'or'
| 'greater' OrEqual  
| 'less' OrEqual .

OrEqual -> 'or' 'equal'
| .

Val -> ID
| Numeric
| String
| Pin .

Arr -> '[' Numeric ']' Arr
| .

Pin -> 'dpin'
| 'apin' .

IfStmnt -> 'if' Val Expr 'do' Stmts ElseStmnt 'end' 'if' .

ElseStmnt -> 'else' ElseIfStmnt Stmnts
| .

ElseIfStmnt -> 'if' Val Expr 'do' ElseStmnt
| .

FuncCall -> 'call' ID CallParam .

FuncDecl -> 'func' ID Optnl_Args  Stmts 'end' ID .

BeginStmt -> 'begin' Beginable .

Beginable -> LoopW
| LoopF .

LoopW -> 'while' Val Expr 'do' Stmts 'end' 'while' .

LoopF -> 'for' ID 'in' Range 'do' Stmts 'end' 'for' .

Optnl_Args -> 'with' Arg ArgList
| .

ArgList -> Arg ArgList 
| .

Range -> Numeric '**' Numeric .

Arg -> Type ID .

WaitStmnt -> 'wait' Numeric TimeOperator .

TimeOperator -> 'h'
| 'm'
| 's'
|'ms' .

CallParam -> 'with' '(' ID CallParams ')' .
CallParams -> ',' ID CallParams
| .
```
