using System;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Objects;

namespace AbstractSyntaxTreeSyntaxTree.Objects
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

        //public new void Visit(ProgramNode node)
        //{
        //    Print("Program");
        //    Indent++;
        //    base.Visit(node);
        //    Indent--;
        //}
        //public new void Visit(IfStatementNode node)
        //{
        //    Print("If");
        //    Indent++;
        //    base.Visit(node);
        //    Indent--;

        //}
        //public new void Visit(FuncNode node)
        //{
        //    Print("Funcnode");
        //    Indent++;
        //    base.Visit(node);
        //    Indent--;


        //}

        public override void Visit(BeginNode beginNode)
        {
            Print("BeginNode");
            Indent++;
            Indent--;
            beginNode.LoopNode.Accept(this);

        }



        internal override void Visit(TimeNode timeNode)
        {
            Print("TimeNode");
            Indent++;
            Indent--;
        }

        internal override void Visit(DeclParametersNode declParametersNode)
        {
            Print("DeclParametersNode");
            Indent++;
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
            Indent--;
        }

        public override void Visit(TimesNode timesNode)
        {
            Print("TimesNode");
            Indent++;

        }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            Print("FunctionLoopNode");
            Indent++;
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            Indent--;
        }

        internal override void Visit(AssignmentNode assignmentNode)
        {
            Print("AssignmentNode");
            Indent++;
            Indent--;
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public override void Visit(StatementNode statementNode)
        {
            Print("StatementNode");
            Indent++;
            Indent--;

        }

        public override void Visit(WithNode withNode)
        {
            Print("WithNode");
            Indent++;
            Indent--;

        }

        public override void Visit(WaitNode waitNode)
        {
            Print("WaitNode");
            Indent++;
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            Indent--;
        }

        public override void Visit(VarNode varNode)
        {
            Print("VarNode");
            Indent++;
            Indent--;

        }

        public override void Visit(ValNode valNode)
        {
            Print("ValNode");
            Indent++;
            Indent--;
        }

        public override void Visit(TimeSecondNode timeSecondNode)
        {
            Print("TimeSecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeMinuteNode timeMinuteNode)
        {
            Print("TimeMinuteNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            Print("TimeMillisecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeHourNode timeHourNode)
        {
            Print("TimeHourNode");
            Indent++;
            Indent--;


        }

        public override void Visit(RightParenthesisNode rightParenthesisNode)
        {
            Print("TimeMillisecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(NumericNode numericNode)
        {
            Print("NumericNode");
            Indent++;
            Indent--;


        }

        public override void Visit(NewlineNode newlineNode)
        {
            Print("NewlineNode");
            Indent++;
            Indent--;
        }

        public override void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            Print("LeftParenthesisNode");
            Indent++;
            Indent--;
        }

        public override void Visit(InNode inNode)
        {
            Print("InNode");
            Indent++;
            Indent--;

        }
        public override void Visit(EqualNode equalNode)
        {
            Print("EqualNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EqualsNode equalsNode)
        {
            Print("EqualsNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EOFNode eOFNode)
        {
            Print("EOFNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EpsilonNode epsilonNode)
        {
            Print("EpsilonNode");
            Indent++;
            Indent--;

        }

        public override void Visit(DoNode doNode)
        {
            Print("DoNode");
            Indent++;
            Indent--;

        }

        public override void Visit(ProgramNode programNode)
        {
            Print("Program");
            Indent++;
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
            }
            programNode.LoopFunction.Accept(this);
            Indent--;
        }

        public override void Visit(CallNode callNode)
        {
            Print("CallNode");
            Indent++;
            callNode.Id.Accept(this);
            callNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
        }

        public override void Visit(EndNode endNode)
        {
            Print("EndNode");
            Indent++;
            Indent--;
        }
        public override void Visit(AndNode andNode)
        {
            Print("AndNode");
            Indent++;
            Indent--;
        }
        public override void Visit(PinNode pinNode)
        {
            Print("PinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(APinNode apinNode)
        {
            Print("APinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(DPinNode dpinNode)
        {
            Print("DPinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(OperatorNode operatorNode)
        {
            Print("OperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(BoolOperatorNode boolOperatorNode)
        {
            Print("BoolOperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(CallParametersNode callParametersNode)
        {
            Print("CallParametersNode");
            Indent++;
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
        }
        public override void Visit(DivideNode divideNode)
        {
            Print("DivideNode");
            Indent++;
            Indent--;
        }
        public override void Visit(ExpressionNode expressionNode)
        {
            Print("ExpressionNode");
            Indent++;
            expressionNode.Term.Accept(this);
            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
            Indent--;
        }
        public override void Visit(ForNode forNode)
        {
            Print("ForNode");
            Indent++;
            forNode.CountingVariable.Accept(this);
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
            }
            //forNode.Accept(this);
            Indent--;
        }
        public override void Visit(FuncNode funcNode)
        {
            Print("FuncNode");
            Indent++;

            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            Indent--;
        }
        public override void Visit(GreaterNode greaterNode)
        {
            Print("GreaterNode");
            Indent++;
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
            Indent--;
        }
        public override void Visit(IfStatementNode ifStatementNode)
        {
            Print("IfstatementNode");
            Indent++;
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
        }
        public override void Visit(LessNode lessNode)
        {
            Print("LessNode");
            Indent++;
            lessNode.OrEqualNode.Accept(this);
            Indent--;
        }
        public override void Visit(LoopNode loopNode)
        {
            Print("LoopNode");
            Indent++;
            Indent--;
        }
        public override void Visit(MathOperatorNode mathOperatorNode)
        {
            Print("MathOperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(PlusNode plusNode)
        {
            Print("PlusNode");
            Indent++;
            Indent--;
        }
        public override void Visit(MinusNode minusNode)
        {
            Print("MinusNode");
            Indent++;
            Indent--;
        }
        public override void Visit(ModuloNode moduloNode)
        {
            Print("ModuloNode");
            Indent++;
            Indent--;
        }
        public override void Visit(OrNode orNode)
        {
            Print("OrNode");
            Indent++;
            Indent--;
        }
        public override void Visit(StringNode stringNode)
        {
            Print("StringNode");
            Indent++;
            Indent--;
        }
        public override void Visit(WhileNode whileNode)
        {
            Print("WhileNode");
            Indent++;

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
        }
        public override void Visit(ElseStatementNode elseStatement)
        {
            Print("ElseStatementNode");
            Indent++;
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }
            //elseStatement.Accept(this);
            Indent--;
        }
        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            Print("ElseifStatementNode");
            Indent++;
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            //elseifStatementNode.Accept(this);
            Indent--;
        }
        public override void Visit(RangeNode rangeNode)
        {
            Print("RangeNode");
            Indent++;
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
            Indent--;
        }

        public override void Visit(ReturnNode returnNode)
        {
            Print("ReturnNode");
            Indent++;
            returnNode.ReturnValue.Accept(this);
            Indent--;
        }
    }
}