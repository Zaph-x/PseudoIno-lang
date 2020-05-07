using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public interface ITerm : ITyped
    {
        void Accept(Visitor visitor);
    }
}