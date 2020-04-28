using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public abstract class Visitor
    {
        public abstract void Visit(BeginNode beginNode);


        internal abstract void Visit(TimeNode timeNode);


        internal abstract void Visit(DeclParametersNode declParametersNode);


        public abstract void Visit(TimesNode timesNode);


        public abstract void Visit(FunctionLoopNode loopFnNode);


        internal abstract void Visit(AssignmentNode assignmentNode);


        public abstract void Visit(StatementNode statementNode);


        public abstract void Visit(WithNode withNode);


        public abstract void Visit(WaitNode waitNode);


        public abstract void Visit(VarNode varNode);


        public abstract void Visit(ValNode valNode);


        public abstract void Visit(TimeSecondNode timeSecondNode);


        public abstract void Visit(TimeMinuteNode timeMinuteNode);


        public abstract void Visit(TimeMillisecondNode timeMillisecondNode);


        public abstract void Visit(TimeHourNode timeHourNode);


        public abstract void Visit(RightParenthesisNode rightParenthesisNode);


        public abstract void Visit(NumericNode numericNode);

        public abstract void Visit(NewlineNode newlineNode);


        public abstract void Visit(LeftParenthesisNode leftParenthesisNode);


        public abstract void Visit(InNode inNode);

        public abstract void Visit(EqualNode equalNode);


        public abstract void Visit(EqualsNode equalsNode);


        public abstract void Visit(EOFNode eOFNode);


        public abstract void Visit(EpsilonNode epsilonNode);


        public abstract void Visit(DoNode doNode);


        public abstract void Visit(ProgramNode programNode);


        public abstract void Visit(CallNode callNode);


        public abstract void Visit(EndNode endNode);

        public abstract void Visit(AndNode andNode)
       ;
        public abstract void Visit(PinNode pinNode)
        ;
        public abstract void Visit(APinNode apinNode)
        ;
        public abstract void Visit(DPinNode dpinNode)
       ;
        public abstract void Visit(OperatorNode operatorNode)
        ;
        public abstract void Visit(BoolOperatorNode boolOperatorNode)
        ;
        public abstract void Visit(CallParametersNode callParametersNode)
       ;
        public abstract void Visit(DivideNode divideNode)
        ;
        public abstract void Visit(ExpressionNode expressionNode)
       ;
        public abstract void Visit(ForNode forNode)
      ;
        public abstract void Visit(FuncNode funcNode)
       ;
        public abstract void Visit(GreaterNode greaterNode)
       ;
        public abstract void Visit(IfStatementNode ifStatementNode)
       ;
        public abstract void Visit(LessNode lessNode)
        ;
        public abstract void Visit(LoopNode loopNode)
        ;
        public abstract void Visit(MathOperatorNode mathOperatorNode)
        ;
        public abstract void Visit(PlusNode plusNode)
       ;
        public abstract void Visit(MinusNode minusNode)
        ;
        public abstract void Visit(ModuloNode moduloNode)
       ;
        public abstract void Visit(OrNode orNode);
        public abstract void Visit(StringNode stringNode);

        public abstract void Visit(WhileNode whileNode);
        public abstract void Visit(ElseStatementNode elseStatement);
        public abstract void Visit(ElseifStatementNode elseifStatementNode);

        public abstract void Visit(RangeNode rangeNode);

        public abstract void Visit(ReturnNode returnNode);
    }
}