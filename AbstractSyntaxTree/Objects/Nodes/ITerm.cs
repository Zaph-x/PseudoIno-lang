using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public interface ITerm
    {
        public void Accept(Visitor visitor);
    }
}