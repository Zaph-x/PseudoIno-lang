using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the time minute node class
    /// It inherits from time node
    /// </summary>
    public class TimeMinuteNode : TimeNode
    {
        /// <summary>
        /// This is the constructor for time minuse node
        /// </summary>
        /// <param name="token">This is the token</param>
        public TimeMinuteNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}