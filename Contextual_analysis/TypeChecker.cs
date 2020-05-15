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
        public override object Visit(BeginNode beginNode)
        {
            return null;
        }

        public override object Visit(TimeNode timeNode)
        {
            return null;
        }

        public override object Visit(DeclParametersNode declParametersNode)
        {
            return null;
        }

        public override object Visit(TimesNode timesNode)
        {
            return new TypeContext(OP_TIMES);
        }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            return null;
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            TypeContext rhs = (TypeContext)assignmentNode.RightHand.Accept(this);
            TypeContext lhs = (TypeContext)assignmentNode.LeftHand.Accept(this);
            if (lhs.Type == VAR)
            {
                if (CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode).Type == VAR)
                {
                    CurrentScope.UpdateTypedef(assignmentNode.LeftHand as VarNode, rhs);
                }
                lhs = CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode);
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

        public override object Visit(StatementNode statementNode)
        {
            return null;
        }

        public override object Visit(WithNode withNode)
        {
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

        public override object Visit(RightParenthesisNode rightParenthesisNode)
        {
            return null;
        }

        public override object Visit(NumericNode numericNode)
        {
            return numericNode.SymbolType;
        }

        public override object Visit(NewlineNode newlineNode)
        {
            return null;
        }

        public override object Visit(LeftParenthesisNode leftParenthesisNode)
        {
            return null;
        }

        public override object Visit(InNode inNode)
        {
            return null;
        }

        public override object Visit(EqualNode equalNode)
        {
            return new TypeContext(OP_EQUAL);

        }

        public override object Visit(EqualsNode equalsNode)
        {
            return new TypeContext(OP_EQUAL);

        }

        public override object Visit(EOFNode eOFNode)
        {
            return null;
        }

        public override object Visit(EpsilonNode epsilonNode)
        {
            return null;
        }

        public override object Visit(DoNode doNode)
        {
            return null;
        }

        public override object Visit(ProgramNode programNode)
        {
            programNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            programNode.FunctionDefinitons.ForEach(func =>
            {
                GlobalScope.FunctionDefinitions.Add(func);
                func.Accept(this);
            });
            if (programNode.LoopFunction == null) {
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
                TypeContext ctx = GlobalScope.FunctionDefinitions.First(node => node.Name.Id == callNode.Id.Id).SymbolType;
                return ctx;
            }
            catch
            {
                new NotDefinedException($"Undefined function call at {callNode.Line}:{callNode.Offset}");
                return null;
            }
        }

        public override object Visit(EndNode endNode)
        {
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(AndNode andNode)
        {
            return new TypeContext(BOOL);

        }

        public override object Visit(PinNode pinNode)
        {
            return null;
        }

        public override object Visit(APinNode apinNode)
        {
            return new TypeContext(APIN);
        }

        public override object Visit(DPinNode dpinNode)
        {
            return new TypeContext(DPIN);
        }

        public override object Visit(OperatorNode operatorNode)
        {
            return new TypeContext(BOOL);
        }

        public override object Visit(BoolOperatorNode boolOperatorNode)
        {
            return new TypeContext(BOOL);
        }

        public override object Visit(CallParametersNode callParametersNode)
        {
            return null;
        }

        public override object Visit(DivideNode divideNode)
        {
            return new TypeContext(OP_DIVIDE);
        }

        public override object Visit(ExpressionNode expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator.Accept(this);
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
        public override object Visit(BinaryExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
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
        public override object Visit(ParenthesisExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.LeftHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
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
            CurrentScope.Symbols.Add(new Symbol(forNode.CountingVariable.Id,TokenType.NUMERIC, false, forNode.CountingVariable));
            if (fromType.Type != toType.Type)
                new InvalidTypeException($"Mismatch in range types at {forNode.Line}:{forNode.Offset}");
            forNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }

        public override object Visit(FuncNode funcNode)
        {
            CurrentScope = GlobalScope.FindChild($"func_{funcNode.Name.Id}");
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

        public override object Visit(LoopNode loopNode)
        {
            return null;
        }

        public override object Visit(MathOperatorNode mathOperatorNode)
        {
            return null;
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

        public override object Visit(RangeNode rangeNode)
        {
            TypeContext fromType = (TypeContext)rangeNode.From.Accept(this);
            TypeContext toType = (TypeContext)rangeNode.To.Accept(this);
            if (fromType != toType)
                new InvalidTypeException($"Mismatch in range types at {rangeNode.Line}:{rangeNode.Offset}");
            if (int.Parse(rangeNode.From.Value) > int.Parse(rangeNode.To.Value))
                new InvalidRangeException($"Invalid range in range at {rangeNode.Line}:{rangeNode.Offset}");

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

        public override object Visit(FollowTermNode followTermNode)
        {
            throw new System.NotImplementedException();
        }

        public override object Visit(BoolNode boolNode)
        {
            return new TypeContext(BOOL);
        }

        public bool IsOfTypes(TypeContext ctx, params TokenType[] types)
        {
            return types.Any(t => t == ctx.Type);
        }
    }
}
