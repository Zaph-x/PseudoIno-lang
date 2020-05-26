using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class time hour node
    /// It inherits from time node class
    /// </summary>
    public class TimeHourNode : TimeNode
    {
        /// <summary>
        /// This is the time hour node constructor
        /// </summary>
        /// <param name="token">This is the token</param>
        public TimeHourNode( ScannerToken token) : base(token)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}