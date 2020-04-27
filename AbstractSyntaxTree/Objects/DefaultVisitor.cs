using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public class DefaultVisitor : Visitor
    {
        public override void Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);

        }

        public override void Visit(TimeNode timeNode)
        {
            //timeNode.Accept(this);
        }
        public override void Visit(TimesNode timesNode)
        {

        }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        public override void Visit(AssignmentNode assignmentNode)
        {
            assignmentNode.LeftHand.Accept(this);
            assignmentNode.RightHand.Accept(this);
            if (assignmentNode.ExpressionHand != null)
                assignmentNode.ExpressionHand.Accept(this);
        }

        public override void Visit(FunctionDefinitonNode functionDefinitonNode)
        {
            functionDefinitonNode.LeftHand?.Accept(this);
            functionDefinitonNode.RightHand?.Accept(this);
            if (functionDefinitonNode.Statements.Any())
            {
                functionDefinitonNode.Statements.ForEach(node => node.Accept(this));
            }


            //functionDefinitonNode.Accept(this);
        }

        public override void Visit(StatementNode statementNode)
        {
            //statementNode.Accept(this);
        }

        public override void Visit(WithNode withNode)
        {
            //withNode.Accept(this);
        }

        public override void Visit(WaitNode waitNode)
        {

            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
        }

        public override void Visit(VarNode varNode)
        {

        }

        public override void Visit(ValNode valNode)
        {
            //valNode.Accept(this);
        }

        public override void Visit(TimeSecondNode timeSecondNode)
        {
            //timeSecondNode.Accept(this);
        }

        public override void Visit(TimeMinuteNode timeMinuteNode)
        {
            //timeMinuteNode.Accept(this);
        }

        public override void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            //timeMillisecondNode.Accept(this);
        }

        public override void Visit(TimeHourNode timeHourNode)
        {
            // timeHourNode.Accept(this);

        }

        public override void Visit(RightParenthesisNode rightParenthesisNode)
        {
            //rightParenthesisNode.Accept(this);
        }

        public override void Visit(NumericNode numericNode)
        {

            //numericNode.Accept(this);

        }

        public override void Visit(NewlineNode newlineNode)
        {
            //newlineNode.Accept(this);
        }

        public override void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            //leftParenthesisNode.Accept(this);
        }

        public override void Visit(InNode inNode)
        {
            //inNode.Accept(this);
        }
        public override void Visit(EqualNode equalNode)
        {
            //equalNode.Accept(this);
        }

        public override void Visit(EqualsNode equalsNode)
        {
            //equalsNode.Accept(this);
        }

        public override void Visit(EOFNode eOFNode)
        {
            //eOFNode.Accept(this);
        }

        public override void Visit(EpsilonNode epsilonNode)
        {
            // epsilonNode.Accept(this);
        }

        public override void Visit(DoNode doNode)
        {
            //doNode.Accept(this);
        }

        public override void Visit(ProgramNode programNode)
        {
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
            }

            programNode.LoopFunction.Accept(this);
        }

        public override void Visit(CallNode callNode)
        {
            callNode.VarNode.Accept(this);
            if (callNode.RightHand != null)
            {
                callNode.RightHand.Accept(this);
            }
            //callNode.Accept(this);
        }

        public override void Visit(EndNode endNode)
        {
            //endNode.Accept(this);
        }
        public override void Visit(AndNode andNode)
        {
            //andNode.Accept(this);
        }
        public override void Visit(PinNode pinNode)
        {
            //pinNode.Accept(this);
        }
        public override void Visit(APinNode apinNode)
        {

        }
        public override void Visit(DPinNode dpinNode)
        {

        }
        public override void Visit(OperatorNode operatorNode)
        {
            //operatorNode.Accept(this);
        }
        public override void Visit(BoolOperatorNode boolOperatorNode)
        {
            //boolOperatorNode.Accept(this);
        }
        public override void Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.ValNode.Accept(this);
            if (callParametersNode.RightHand != null)
            {
                callParametersNode.RightHand.Accept(this);
            }
            //callParametersNode.Accept(this);
        }
        public override void Visit(DivideNode divideNode)
        {

        }
        public override void Visit(ExpressionNode expressionNode)
        {
            expressionNode.Value.Accept(this);
            expressionNode.Operator.Accept(this);
            if (expressionNode.Expression != null)
            {
                expressionNode.Expression.Accept(this);
            }

        }
        public override void Visit(ForNode forNode)
        {
            forNode.ValNode.Accept(this);
            forNode.RangeNode.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
            }
            //forNode.Accept(this);
        }
        public override void Visit(FuncNode funcNode)
        {
            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
            funcNode.LeftHand.Accept(this);
            funcNode.RightHand.Accept(this);

        }
        public override void Visit(GreaterNode greaterNode)
        {
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
        }
        public override void Visit(IfStatementNode ifStatementNode)
        {
            ifStatementNode.Val.Accept(this);
            ifStatementNode.Expression.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            if (ifStatementNode.ElseifStatementNode.Any())
            {
                ifStatementNode.ElseifStatementNode.ForEach(node => node.Accept(this));
            }
            ifStatementNode.ElseStatementNode.Accept(this);
            //ifStatementNode.Accept(this);
        }
        public override void Visit(LessNode lessNode)
        {
            lessNode.OrEqualNode?.Accept(this);

        }
        public override void Visit(LoopNode loopNode)
        {
            //loopNode.Accept(this);
        }
        public override void Visit(MathOperatorNode mathOperatorNode)
        {
            //mathOperatorNode.Accept(this);
        }
        public override void Visit(PlusNode plusNode)
        {

        }
        public override void Visit(MinusNode minusNode)
        {

        }
        public override void Visit(ModuloNode moduloNode)
        {

        }
        public override void Visit(OrNode orNode)
        {
            //orNode.Accept(this);
        }
        public override void Visit(StringNode stringNode)
        {
            //stringNode.Accept(this);
        }
        public override void Visit(WhileNode whileNode)
        {
            whileNode.ValNode.Accept(this);
            whileNode.ExpressionNode.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
            }

        }
        public override void Visit(ElseStatementNode elseStatement)
        {
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }
            //elseStatement.Accept(this);
        }
        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            elseifStatementNode.Val.Accept(this);
            elseifStatementNode.Expression.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            // elseifStatementNode.Accept(this);
        }
        public override void Visit(RangeNode rangeNode)
        {
            rangeNode.LeftHand.Accept(this);
            rangeNode.RightHand.Accept(this);
            // rangeNode.Accept(this);
        }
    }
}