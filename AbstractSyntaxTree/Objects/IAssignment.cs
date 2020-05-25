using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public interface IAssignment : ITyped
    {
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        object Accept(Visitor visitor);
    }
}