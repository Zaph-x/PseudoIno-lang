using System;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public class PrettyPrinter : Visitor
    {
        private int Indent { get; set; } = 0;
        private void Print(string input)
        {
            string line = "";
            for (int i = 0; i < Indent; i++)
            {
                line += "|---";
            }
            Console.WriteLine(line + input);
        }
        public override void Visit(ProgramNode node)
        {
            Print("Program");
            Indent++;
            //base.Visit(node);
            Indent--;
        }

        public override void Visit(BeginNode beginNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimeNode timeNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimesNode timesNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(AssignmentNode assignmentNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(FunctionDefinitonNode functionDefinitonNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(StatementNode statementNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(WithNode withNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(WaitNode waitNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(VarNode varNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ValNode valNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimeSecondNode timeSecondNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimeMinuteNode timeMinuteNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TimeHourNode timeHourNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(RightParenthesisNode rightParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(NumericNode numericNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(NewlineNode newlineNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(InNode inNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(EqualNode equalNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(EqualsNode equalsNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(EOFNode eOFNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(EpsilonNode epsilonNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(DoNode doNode)
        {
            throw new NotImplementedException();
        }

         public override void Visit(CallNode callNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(AndNode andNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(PinNode pinNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(APinNode apinNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(DPinNode dpinNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(OperatorNode operatorNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(BoolOperatorNode boolOperatorNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(CallParametersNode callParametersNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(DivideNode divideNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ExpressionNode expressionNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ForNode forNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(FuncNode funcNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(GreaterNode greaterNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IfStatementNode ifStatementNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(LessNode lessNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(LoopNode loopNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(MathOperatorNode mathOperatorNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(PlusNode plusNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(MinusNode minusNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ModuloNode moduloNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(OrNode orNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(StringNode stringNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(WhileNode whileNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ElseStatementNode elseStatement)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            throw new NotImplementedException();
        }

        public override void Visit(RangeNode rangeNode)
        {
            throw new NotImplementedException();
        }
    }
}