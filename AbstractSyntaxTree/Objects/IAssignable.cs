using Lexer.Objects;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// Using this interface implements an ID on pins and and an accept method for visiting the nodes
    /// </summary>
    public interface IAssginable : ITyped
    {
        /// <summary>
        /// The Identification of the Pins.
        /// </summary>
        /// <value>The value is set in the constructor of the nodes which implements this interface</value>
        string Id { get; set; }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        object Accept(Visitor visitor);
    }

}