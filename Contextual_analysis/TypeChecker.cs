using System.Runtime.CompilerServices;
using System;
using System.Reflection.Metadata;
using AbstractSyntaxTree.Objects.Nodes;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Lexer.Objects;
using static Lexer.Objects.TokenType;
using SymbolTable;
using Contextual_analysis.Exceptions;

namespace Contextual_analysis
{
    public class TypeChecker : Visitor
    {
        private SymbolTableObject GlobalScope = SymbolTableBuilder.GlobalSymbolTable;
        private SymbolTableObject CurrentScope = SymbolTableBuilder.GlobalSymbolTable;
        public static bool HasError { get; set; } = false;

        public override object Visit(TimesNode timesNode)
        {
            return new TypeContext(OP_TIMES);
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            TypeContext lhs = (TypeContext)assignmentNode.LeftHand.Accept(this);
            TypeContext rhs;
            if (assignmentNode.RightHand.GetType().IsAssignableFrom(typeof(ArrayNode)))
            {
                ArrayNode arr = (ArrayNode)assignmentNode.RightHand;
                if (arr.HasBeenAccessed)
                {
                    if (lhs.Type == VAR)
                    {
                        rhs = (TypeContext)arr.FirstAccess.RightHand.Accept(this);
                        assignmentNode.LeftHand.SymbolType = rhs;
                        assignmentNode.LeftHand.Type = rhs.Type;
                    }
                    else
                        rhs = (TypeContext)arr.FirstAccess.RightHand.Accept(this);
                }
                else
                {
                    new InvalidTypeException($"Array type never set. Error at {arr.Line}:{arr.Offset}");
                    return null;
                }
            }
            else
            {
                rhs = (TypeContext)assignmentNode.RightHand.Accept(this);
            }

            if (lhs.Type == VAR)
            {
                if (!CurrentScope.HasDeclaredVar(assignmentNode.LeftHand as AstNode))
                {
                    (assignmentNode.LeftHand as VarNode).Declaration = true;
                    CurrentScope.DeclaredVars.Add((assignmentNode.LeftHand as VarNode).Id);
                }

                if (assignmentNode.LeftHand.GetType().IsAssignableFrom(typeof(VarNode)))
                {
                    if (CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode).Type == VAR)
                    {
                        CurrentScope.UpdateTypedef(assignmentNode.LeftHand as VarNode, rhs, CurrentScope.Name, true);
                    }
                    lhs = CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode);
                }
                else if (assignmentNode.LeftHand.GetType().IsAssignableFrom(typeof(ArrayAccessNode)))
                {
                    if (CurrentScope.FindArray((assignmentNode.LeftHand as ArrayAccessNode).Actual.ActualId.Id).Type == ARR)
                    {
                        ArrayAccessNode node = (assignmentNode.LeftHand as ArrayAccessNode);
                        ArrayNode arr = CurrentScope.FindArray(node.Actual.ActualId.Id);
                        if (!(arr.Dimensions.Count >= node.Accesses.Count))
                        {
                            new OutOfRangeException($"Illegal access to {arr.Dimensions.Count} dimensional array. Error at {node.Line}:{node.Offset}");
                            return null;
                        }
                        if (arr.Dimensions.Count > node.Accesses.Count && node.Accesses.Count > 1)
                        {
                            AssignmentNode declaringStatement = new AssignmentNode(node.Line, node.Offset);
                            declaringStatement.LeftHand =
                                new VarNode($"protected_declaration_{arr.ActualId.Id}_{node.Accesses.Count - 1}", new ScannerToken(node.Type, $"protected_declaration_{arr.ActualId.Id}_{node.Accesses.Count - 1}", node.Line, node.Offset))
                                { IsArray = true, SymbolType = new TypeContext(node.Type) };
                            ArrayNode declaringArray = new ArrayNode(node.Line, node.Offset);
                            for (int i = 0; i < node.Accesses.Count; i++)
                            {
                                declaringArray.Dimensions.Add(arr.Dimensions[i]);
                            }
                            ((IScope)assignmentNode.Parent).Statements.Insert(((IScope)assignmentNode.Parent).Statements.IndexOf(assignmentNode), declaringStatement);


                        }
                        node.Actual.FirstAccess.LeftHand.SymbolType = rhs;
                        node.Actual.FirstAccess.LeftHand.Type = rhs.Type;
                        arr.SymbolType = rhs;
                        arr.Type = rhs.Type;
                        lhs = rhs;
                    }
                }

            }
            if (lhs.Type != rhs.Type)
            {
                if ((lhs.Type != DPIN && lhs.Type != APIN) || (rhs.Type != NUMERIC && rhs.Type != BOOL))
                {
                    new InvalidTypeException($"Type {rhs.Type} is not assignable toType {lhs.Type} at {assignmentNode.Line}:{assignmentNode.Offset}");
                }
            }


            return null;
        }

        public override object Visit(WaitNode waitNode)
        {
            return null;
        }

        public override object Visit(VarNode varNode)
        {
            return (CurrentScope ?? GlobalScope).FindSymbol(varNode);
        }

        public override object Visit(ValNode valNode)
        {
            return null;
        }

        public override object Visit(TimeSecondNode timeSecondNode)
        {
            return null;
        }

        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            return null;
        }

        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            return null;
        }

        public override object Visit(TimeHourNode timeHourNode)
        {
            return null;
        }
        public override object Visit(NumericNode numericNode)
        {
            return numericNode.SymbolType;
        }

        public override object Visit(EqualNode equalNode)
        {
            return new TypeContext(OP_EQUAL);

        }
        public override object Visit(ProgramNode programNode)
        {
            programNode.FunctionDefinitons.ForEach(func =>
            {
                if (!(GlobalScope.FunctionDefinitions.Where(fn => fn.Name.Id == func.Name.Id && func.FunctionParameters.Count == fn.FunctionParameters.Count).Count() > 1))
                {
                    if (SymbolTableObject.FunctionCalls.Any(cn => cn.Id.Id == func.Name.Id && cn.Parameters.Count == func.FunctionParameters.Count))
                    {
                        CallNode cn = SymbolTableObject.FunctionCalls.First(cn => cn.Id.Id == func.Name.Id && cn.Parameters.Count == func.FunctionParameters.Count);
                        FuncNode declaredFunc = GlobalScope.FunctionDefinitions.First(fn => fn.Name.Id == func.Name.Id);
                        SymbolTableObject scope = GlobalScope.FindChild($"func_{declaredFunc.Name.Id}_{declaredFunc.Line}");
                        for (int i = 0; i < cn.Parameters.Count; i++)
                        {
                            if (cn.Parameters[i].SymbolType.Type == VAR)
                                continue;
                            scope.UpdateTypedef(declaredFunc.FunctionParameters[i], cn.Parameters[i].SymbolType, scope.Name, true);
                        }
                    }
                    func.Accept(this);
                }
                else
                {
                    new MultipleDefinedException($"A function '{func.Name.Id}' with {func.FunctionParameters.Count} parameters has already been defined. Error at {func.Line}:{func.Offset}");
                }
            });
            programNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            if (programNode.LoopFunction == null)
            {
                new NotDefinedException("Loop function was not defined.");
                return null;
            }
            programNode.LoopFunction.Accept(this);
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            try
            {
                if (GlobalScope.FunctionDefinitions.Any(func => func.Name.Id == callNode.Id.Id && func.FunctionParameters.Count == callNode.Parameters.Count))
                {
                    TypeContext ctx = GlobalScope.FunctionDefinitions.First(node => node.Name.Id == callNode.Id.Id && node.FunctionParameters.Count == callNode.Parameters.Count).SymbolType;
                    return ctx;
                }
                else
                {
                    new NotDefinedException($"A function '{callNode.Id.Id}' with {callNode.Parameters.Count} parameters has not been defined. Error at {callNode.Line}:{callNode.Offset} ");
                    return null;
                }
            }
            catch
            {
                new NotDefinedException($"Undefined function call at {callNode.Line}:{callNode.Offset}");
                return null;
            }
        }

        public override object Visit(AndNode andNode)
        {
            return new TypeContext(OP_AND);

        }

        public override object Visit(APinNode apinNode)
        {
            return new TypeContext(APIN);
        }

        public override object Visit(DPinNode dpinNode)
        {
            return new TypeContext(DPIN);
        }

        public override object Visit(DivideNode divideNode)
        {
            return new TypeContext(OP_DIVIDE);
        }
        public override object Visit(BinaryExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
            if (lhs.Type == VAR)
            {
                if (opctx == null)
                {
                }
                else if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                lhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode);

            }
            if (rhs?.Type == VAR)
            {
                if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                rhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode);

            }
            if (opctx == null)
            {
                return expressionNode.SymbolType = lhs;
            }
            if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_LEQ, OP_GEQ, OP_LESS, OP_GREATER, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, BOOL) && IsOfTypes(rhs, BOOL) && IsOfTypes(opctx, OP_EQUAL, OP_AND, OP_OR))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_PLUS, OP_MODULO, OP_MINUS, OP_TIMES, OP_DIVIDE))
            {
                return expressionNode.SymbolType = new TypeContext(NUMERIC) { IsFloat = (lhs.IsFloat || rhs.IsFloat) };
            }
            else if (IsOfTypes(opctx, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            new InvalidTypeException($"Expression {lhs} {opctx} {rhs} is invalid (types) at {expressionNode.Line}:{expressionNode.Offset}");
            return null;
        }
        public override object Visit(ParenthesisExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
            if (lhs.Type == VAR)
            {
                if (opctx == null) { }
                else if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                lhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode);

            }
            if (rhs?.Type == VAR)
            {
                if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                rhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode);

            }
            if (rhs == null && opctx == null)
            {
                return expressionNode.SymbolType = lhs;
            }
            if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_LEQ, OP_GEQ, OP_LESS, OP_GREATER, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, BOOL) && IsOfTypes(rhs, BOOL) && IsOfTypes(opctx, OP_EQUAL, OP_AND, OP_OR))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_PLUS, OP_MODULO, OP_MINUS, OP_TIMES, OP_DIVIDE))
            {
                return expressionNode.SymbolType = new TypeContext(NUMERIC) { IsFloat = (lhs.IsFloat || rhs.IsFloat) };
            }
            else if (IsOfTypes(opctx, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            new InvalidTypeException($"Expression {lhs} {opctx} {rhs} is invalid (types) at {expressionNode.Line}:{expressionNode.Offset}");
            return null;
        }

        public override object Visit(ExpressionTerm expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            if (lhs.Type == VAR)
            {
                lhs = CurrentScope.FindSymbol(expressionNode.LeftHand as VarNode);
            }
            return expressionNode.SymbolType = lhs;
        }

        public override object Visit(ForNode forNode)
        {

            CurrentScope = GlobalScope.FindChild($"LOOPF_{forNode.Line}");
            TypeContext fromType = (TypeContext)forNode.From.Accept(this);
            TypeContext toType = (TypeContext)forNode.To.Accept(this);
            if (null == CurrentScope.FindSymbol(forNode.CountingVariable)) CurrentScope.Symbols.Add(new Symbol(forNode.CountingVariable.Id, NUMERIC, false, forNode.CountingVariable));
            else CurrentScope.UpdateTypedef(forNode.CountingVariable, new TypeContext(TokenType.NUMERIC) { IsFloat = false }, CurrentScope.Name, true);
            if (fromType.Type != toType.Type)
                new InvalidTypeException($"Mismatch in range types at {forNode.Line}:{forNode.Offset}");
            forNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(FuncNode funcNode)
        {
            CurrentScope = GlobalScope.FindChild($"func_{funcNode.Name.Id}_{funcNode.Line}");
            funcNode.Statements.ForEach(stmnt =>
            {
                if (stmnt is CallNode)
                {
                    if (((CallNode)stmnt).Id.Id == funcNode.Name.Id)
                    {
                        new InvalidOperationException($"Illegal recursion at {stmnt.Line}:{stmnt.Offset}");
                    }
                }
                stmnt.Accept(this);
            });
            if (funcNode.Statements.Any() && funcNode.Statements.Last().Type == TokenType.RETURN)
            {
                {
                    funcNode.SymbolType = (TypeContext)funcNode.Statements.Last().Accept(this);
                    CurrentScope = CurrentScope.Parent ?? GlobalScope;
                    return funcNode.SymbolType;
                }
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(GreaterNode greaterNode)
        {
            return new TypeContext(OP_GREATER);
        }

        public override object Visit(IfStatementNode ifStatementNode)
        {
            CurrentScope = GlobalScope.FindChild($"{ifStatementNode.Type}_{ifStatementNode.Line}");
            if (((TypeContext)ifStatementNode.Expression.Accept(this)).Type == BOOL)
            {
                ifStatementNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"If statement expected a boolean expression at {ifStatementNode.Line}:{ifStatementNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(LessNode lessNode)
        {
            return new TypeContext(OP_LESS);
        }

        public override object Visit(PlusNode plusNode)
        {
            return new TypeContext(OP_PLUS);
        }

        public override object Visit(MinusNode minusNode)
        {
            return new TypeContext(OP_MINUS);
        }

        public override object Visit(ModuloNode moduloNode)
        {
            return new TypeContext(OP_MODULO);
        }

        public override object Visit(OrNode orNode)
        {
            return new TypeContext(OP_OR);
        }

        public override object Visit(StringNode stringNode)
        {
            return new TypeContext(STRING);
        }

        public override object Visit(WhileNode whileNode)
        {
            CurrentScope = GlobalScope.FindChild($"LOOPW_{whileNode.Line}");
            if (((TypeContext)whileNode.Expression.Accept(this)).Type == BOOL)
            {
                whileNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"While statement expected a boolean expression at {whileNode.Line}:{whileNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            CurrentScope = GlobalScope.FindChild($"ELSESTMNT_{elseStatement.Line}");
            elseStatement.Statements.ForEach(stmnt => stmnt.Accept(this));
            CurrentScope = CurrentScope?.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            CurrentScope = GlobalScope.FindChild($"{elseifStatementNode.Type}_{elseifStatementNode.Line}");
            if (((TypeContext)elseifStatementNode.Expression.Accept(this)).Type == BOOL)
            {
                elseifStatementNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"Else if statement expected a boolean expression at {elseifStatementNode.Line}:{elseifStatementNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(ReturnNode returnNode)
        {
            return returnNode.ReturnValue.Accept(this);
        }

        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            return new TypeContext(OP_GEQ);
        }

        public override object Visit(LessOrEqualNode lessNode)
        {
            return new TypeContext(OP_LEQ);
        }

        public override object Visit(BoolNode boolNode)
        {
            return new TypeContext(BOOL);
        }

        public bool IsOfTypes(TypeContext ctx, params TokenType[] types)
        {
            return types.Any(t => t == ctx.Type);
        }

        public override object Visit(ArrayNode arrayNode)
        {
            if (arrayNode.HasBeenAccessed)
                return new TypeContext(VAR);
            else
                return null;
        }

        public override object Visit(ArrayAccessNode arrayAccess)
        {
            return (CurrentScope ?? GlobalScope).FindArray(arrayAccess.Actual.ActualId.Id).Accept(this);
        }
    }
}
