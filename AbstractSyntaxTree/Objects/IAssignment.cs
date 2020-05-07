using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public interface IAssignment : ITyped
    {
        void Accept(Visitor visitor);
    }
}