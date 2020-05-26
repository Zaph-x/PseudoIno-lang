using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the or node class
    /// It inherits the mathoperator node class
    /// </summary>
    public class OrNode : MathOperatorNode
    {
        /// <summary>
        /// This is the constructor for or node 
        /// </summary>
        /// <param name="token">This is the token</param>
        public OrNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}