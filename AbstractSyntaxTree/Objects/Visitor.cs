using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public abstract class Visitor
    {
        public void Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);

        }

        internal void Visit(TimeNode timeNode)
        {
            timeNode.Accept(this);
        }

        public void Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any()) {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        internal void Visit(AssignmentNode assignmentNode)
        {
            assignmentNode.LeftHand.Accept(this);
            assignmentNode.RightHand.Accept(this);
            if (assignmentNode.ExpressionHand != null)
                assignmentNode.ExpressionHand.Accept(this);
        }

        internal void Visit(FunctionDefinitonNode functionDefinitonNode)
        {
            functionDefinitonNode.Accept(this);
        }

        public void Visit(StatementNode statementNode)
        {
           statementNode.Accept(this);
        }

        public void Visit(WithNode withNode)
        {
            withNode.Accept(this);
        }

        public void Visit(WaitNode waitNode)
        {
            
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this); 
        }

        public void Visit(VarNode varNode)
        {

        }

        public void Visit(ValNode valNode)
        {
            valNode.Accept(this);
        }

        public void Visit(TimeSecondNode timeSecondNode)
        {
            //timeSecondNode.Accept(this);
        }

        public void Visit(TimeMinuteNode timeMinuteNode)
        {
            //timeMinuteNode.Accept(this);
        }

        public void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            //timeMillisecondNode.Accept(this);
        }

        public void Visit(TimeHourNode timeHourNode)
        {
           // timeHourNode.Accept(this);

        }

        public void Visit(RightParenthesisNode rightParenthesisNode)
        {
            rightParenthesisNode.Accept(this);
        }

        public void Visit(NumericNode numericNode)
        {
 
            numericNode.Accept(this);

        }

        public void Visit(NewlineNode newlineNode)
        {
            newlineNode.Accept(this);
        }

        public void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            leftParenthesisNode.Accept(this);
        }

        public void Visit(InNode inNode)
        {
            inNode.Accept(this);
        }

        public void Visit(EqualsNode equalsNode)
        {
            equalsNode.Accept(this);
        }

        public void Visit(EOFNode eOFNode)
        {
            eOFNode.Accept(this);
        }
        
        public void Visit(EpsilonNode epsilonNode)
        {
            epsilonNode.Accept(this);
        }

        public void Visit(DoNode doNode)
        {
            doNode.Accept(this);
        }

        public void Visit(ProgramNode programNode)
        {
            if (programNode.FunctionDefinitons.Any()) {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any()) {
                programNode.Statements.ForEach(node => node.Accept(this));
            }
            programNode.LoopFunction.Accept(this);
        }

        public void Visit(CallNode callNode)
        {
            callNode.VarNode.Accept(this);
            callNode.RightHand.Accept(this);
            callNode.Accept(this);
        }

        public void Visit(EndNode endNode)
        {
            endNode.Accept(this);
        }
        public void Visit(AndNode andNode)
        {
            //andNode.Accept(this);
        }
        public void Visit(PinNode pinNode)
        {
            pinNode.Accept(this);
        }
        public void Visit(APinNode apinNode)
        {
            
        }
        public void Visit(DPinNode dpinNode)
        {

        }
        public void Visit(OperatorNode operatorNode)
        {
            operatorNode.Accept(this);
        }
        public void Visit(BoolOperatorNode boolOperatorNode)
        {
            boolOperatorNode.Accept(this);
        }
        public void Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.ValNode.Accept(this);
            callParametersNode.RightHand.Accept(this);
            callParametersNode.Accept(this);
        }
    }
}