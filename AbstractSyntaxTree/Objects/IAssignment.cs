using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// Using this interface, implments an accept method for an assignment
    /// The assignment for the right side of the assignment
    /// </summary>
    public interface IAssignment : ITyped
    {
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        object Accept(Visitor visitor);
    }
}