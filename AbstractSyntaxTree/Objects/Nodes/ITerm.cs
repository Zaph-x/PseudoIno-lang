using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public interface ITerm
    {
        void Accept(Visitor visitor);
    }
}