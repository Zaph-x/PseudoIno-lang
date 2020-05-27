using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the time node class
    /// It inherits from the Ast node class
    /// </summary>
    public abstract class TimeNode : AstNode
    {
        /// <summary>
        /// This is the constructor for time node
        /// </summary>
        /// <param name="token">This is the token</param>
        public TimeNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}