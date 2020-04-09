using System;
using Parser.Objects.Nodes;

namespace Parser.Objects
{
    public abstract class Visitor
    {
        internal void Visit(BeginNode beginNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(LoopFnNode loopFnNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(StatementNode statementNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(WithNode withNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(WaitNode waitNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(VarNode varNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(ValNode valNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(TimeSecondNode timeSecondNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(TimeMinuteNode timeMinuteNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(TimeHourNode timeHourNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(RightParenthesisNode rightParenthesisNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(NumericNode numericNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(NewlineNode newlineNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(InNode inNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(EqualsNode equalsNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(EOFNode eOFNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(DoNode doNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(AssugnNode assugnNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(ProgramNode programNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(CallNode callNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }
    }
}