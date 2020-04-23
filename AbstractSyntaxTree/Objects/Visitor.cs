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
            throw new NotImplementedException();
        }

        internal void Visit(TimeNode timeNode)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Visit(StatementNode statementNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(WithNode withNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(WaitNode waitNode)
        {
            //test
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this); 
        }

        public void Visit(VarNode varNode)
        {
        }

        public void Visit(ValNode valNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(TimeSecondNode timeSecondNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(TimeMinuteNode timeMinuteNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(TimeHourNode timeHourNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(RightParenthesisNode rightParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(NumericNode numericNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(NewlineNode newlineNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(InNode inNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(EqualsNode equalsNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(EOFNode eOFNode)
        {
            throw new NotImplementedException();
        }
        
        public void Visit(EpsilonNode epsilonNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(DoNode doNode)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }
    }
}