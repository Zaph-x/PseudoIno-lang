using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the val node class
    /// It inherits the Ast node class and the term interface
    /// </summary>
    public abstract class ValNode : AstNode, ITerm
    {
        /// <summary>
        /// This is the constructor for val node
        /// </summary>
        /// <param name="token">This is the token</param>
        public ValNode(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}