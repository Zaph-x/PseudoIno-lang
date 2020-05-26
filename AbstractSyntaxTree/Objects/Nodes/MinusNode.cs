using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the minus node class
    /// It inherits the math operator node class
    /// </summary>
    public class MinusNode : MathOperatorNode
    {
        /// <summary>
        /// This is the constructor for minus node 
        /// </summary>
        /// <param name="token">This is the token</param>
        public MinusNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}