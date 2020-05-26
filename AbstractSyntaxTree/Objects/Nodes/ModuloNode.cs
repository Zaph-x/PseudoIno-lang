using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for modulo node
    /// It inherits the math operator node class
    /// </summary>
    public class ModuloNode : MathOperatorNode
    {
        /// <summary>
        /// This is the constructor for modulo node
        /// </summary>
        /// <param name="token">This is the token</param>
        public ModuloNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}