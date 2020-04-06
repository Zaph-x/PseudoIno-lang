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

        internal void Visit(AssugnNode assugnNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(CallNode callNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(FuncNode funcNode)
        {
            throw new NotImplementedException();
        }

        internal void Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }
    }
}