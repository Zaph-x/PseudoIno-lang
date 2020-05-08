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
            return null;
        }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            return null;
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            TypeContext rhs = (TypeContext)assignmentNode.RightHand.Accept(this);

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
            return null;
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
            return new TypeContext(BOOL);

        }

        public override object Visit(EqualsNode equalsNode)
        {
            return new TypeContext(BOOL);

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
            programNode.FunctionDefinitons.ForEach(func => func.Accept(this));
            programNode.LoopFunction.Accept(this);
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            return null;
        }

        public override object Visit(EndNode endNode)
        {
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
            return null;
        }

        public override object Visit(DPinNode dpinNode)
        {
            return null;
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
            return null;
        }

        public override object Visit(ExpressionNode expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator.Accept(this);
            return null;
        }
        public override object Visit(NoParenExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
            if (rhs == null && opctx == null)
            {
                return lhs;
            }
            else
            {
                if (lhs == rhs && lhs == opctx)
                {
                    return lhs;
                } 
            }
            return null;
        }
        public override object Visit(ParenthesisExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator.SymbolType;
            
            return null;
        }
        public override object Visit(ExpressionTerm expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            return lhs;
        }

        public override object Visit(ForNode forNode)
        {
            return null;
        }

        public override object Visit(FuncNode funcNode)
        {
            return null;
        }

        public override object Visit(GreaterNode greaterNode)
        {
            return null;
        }

        public override object Visit(IfStatementNode ifStatementNode)
        {
            return null;
        }

        public override object Visit(LessNode lessNode)
        {
            return null;
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
            return null;
        }

        public override object Visit(MinusNode minusNode)
        {
            return null;
        }

        public override object Visit(ModuloNode moduloNode)
        {
            return null;
        }

        public override object Visit(OrNode orNode)
        {
            return null;
        }

        public override object Visit(StringNode stringNode)
        {
            return null;
        }

        public override object Visit(WhileNode whileNode)
        {
            return null;
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            return null;
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            return null;
        }

        public override object Visit(RangeNode rangeNode)
        {
            TypeContext from = (TypeContext)rangeNode.From.Accept(this);
            TypeContext to = (TypeContext)rangeNode.To.Accept(this);
            if (from != to)
                throw new InvalidTypeException($"Mismatch in range types at {rangeNode.Line}:{rangeNode.Offset}");
            if (int.Parse(rangeNode.From.Value) > int.Parse(rangeNode.To.Value))
                throw new InvalidRangeException($"Invalid range in range at {rangeNode.Line}:{rangeNode.Offset}");
            
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
    }
}
