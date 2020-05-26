using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the math operator node class
    /// It inherits the operator node class
    /// </summary>
    public abstract class MathOperatorNode : OperatorNode
    {
        /// <summary>
        /// This is the constructor for math operator
        /// </summary>
        /// <param name="token">This is the token</param>
        public MathOperatorNode(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}