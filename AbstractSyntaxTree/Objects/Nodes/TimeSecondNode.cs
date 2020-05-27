using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the time second node class
    /// It inherits from the time node class
    /// </summary>
    public class TimeSecondNode : TimeNode
    {
        /// <summary>
        /// This is the constructor for time second node
        /// </summary>
        /// <param name="token">This is the token</param>
        public TimeSecondNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}