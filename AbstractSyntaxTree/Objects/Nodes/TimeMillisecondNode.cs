using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the time millisecond node class
    /// It inherits from the time node class
    /// </summary>
    public class TimeMillisecondNode : TimeNode
    {
        /// <summary>
        /// This is the constructor for time millisecond node
        /// </summary>
        /// <param name="token">This is the token</param>
        public TimeMillisecondNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
           return visitor.Visit(this);
        }
    }
}