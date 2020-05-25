using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public interface IAssginable : ITyped
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        string Id { get; set; }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        object Accept(Visitor visitor);
    }

}