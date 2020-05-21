using System;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Objects;
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
        public override object Visit(TimesNode timesNode)
        {
            Print("TimesNode");
            Indent++;
            return null;
        }
        public override object Visit(AssignmentNode assignmentNode)
        {
            Print("AssignmentNode");
            Indent++;
            assignmentNode.LeftHand.Accept(this);
            assignmentNode.RightHand.Accept(this);
            Indent--;
            return null;
        }
        public override object Visit(WaitNode waitNode)
        {
            Print("WaitNode");
            Indent++;
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            Indent--;
            return null;
        }
        public override object Visit(VarNode varNode)
        {
            Print("VarNode");

            return null;
        }
        public override object Visit(ValNode valNode)
        {
            Print("ValNode");

            return null;
        }
        public override object Visit(TimeSecondNode timeSecondNode)
        {
            Print("TimeSecondNode");

            return null;
        }
        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            Print("TimeMinuteNode");

            return null;
        }
        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            Print("TimeMillisecondNode");

            return null;
        }
        public override object Visit(TimeHourNode timeHourNode)
        {
            Print("TimeHourNode");

            return null;
        }
        public override object Visit(NumericNode numericNode)
        {
            Print("NumericNode");

            return null;
        }
        public override object Visit(EqualNode equalNode)
        {
            Print("EqualNode");

            return null;
        }
        public override object Visit(ProgramNode programNode)
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
            return null;
        }
        public override object Visit(CallNode callNode)
        {
            Print("CallNode");
            Indent++;
            callNode.Id.Accept(this);
            callNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
            return null;
        }
        public override object Visit(AndNode andNode)
        {
            Print("AndNode");

            return null;
        }
        public override object Visit(APinNode apinNode)
        {
            Print("APinNode");

            return null;
        }
        public override object Visit(DPinNode dpinNode)
        {
            Print("DPinNode");

            return null;
        }
        public override object Visit(DivideNode divideNode)
        {
            Print("DivideNode");

            return null;
        }
        public override object Visit(ForNode forNode)
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
            return null;
        }
        public override object Visit(FuncNode funcNode)
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
            return null;
        }
        public override object Visit(GreaterNode greaterNode)
        {
            Print("GreaterNode");
            return null;
        }
        public override object Visit(IfStatementNode ifStatementNode)
        {
            Print("IfstatementNode");
            Indent++;
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        public override object Visit(LessNode lessNode)
        {
            Print("LessNode");
            return null;
        }
        public override object Visit(PlusNode plusNode)
        {
            Print("PlusNode");

            return null;
        }
        public override object Visit(MinusNode minusNode)
        {
            Print("MinusNode");

            return null;
        }
        public override object Visit(ModuloNode moduloNode)
        {
            Print("ModuloNode");

            return null;
        }
        public override object Visit(OrNode orNode)
        {
            Print("OrNode");

            return null;
        }
        public override object Visit(StringNode stringNode)
        {
            Print("StringNode");

            return null;
        }
        public override object Visit(WhileNode whileNode)
        {
            Print("WhileNode");
            Indent++;
            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        public override object Visit(ElseStatementNode elseStatement)
        {
            Print("ElseStatementNode");
            Indent++;
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            Print("ElseifStatementNode");
            Indent++;
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        public override object Visit(ReturnNode returnNode)
        {
            Print("ReturnNode");
            Indent++;
            returnNode.ReturnValue.Accept(this);
            Indent--;
            return null;
        }

        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            Print("GreaterOrEqualNode");
            return null;
        }

        public override object Visit(LessOrEqualNode lessNode)
        {
            Print("LessOrEqualNode");
            return null;
        }

        public override object Visit(ExpressionTerm expressionTermNode)
        {
            Print("ExpressionTerm");
            Indent++;
            expressionTermNode.LeftHand.Accept(this);
            Indent--;
            return null;
        }

        public override object Visit(BinaryExpression binaryExpression)
        {
            Print("BinaryExpression");
            Indent++;
            binaryExpression.LeftHand.Accept(this);
            binaryExpression.Operator?.Accept(this);
            binaryExpression.RightHand?.Accept(this);
            Indent--;
            return null;
        }

        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            Print("ParenthesisExpression");
            Indent++;
            parenthesisExpression.LeftHand.Accept(this);
            parenthesisExpression.Operator?.Accept(this);
            parenthesisExpression.RightHand?.Accept(this);
            Indent--;
            return null;
        }

        public override object Visit(BoolNode boolNode)
        {
            Print("Bool");
            return null;
        }

        public override object Visit(ArrayNode arrayNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ArrayAccessNode arrayAccess)
        {
            throw new NotImplementedException();
        }
    }
}