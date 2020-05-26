using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for greater or equal node class
    /// It inherits the bool operator node class
    /// </summary>
    public class GreaterOrEqualNode : BoolOperatorNode
    {
        /// <summary>
        /// This is the constructor for greater or equal node
        /// </summary>
        /// <param name="node">This is the node</param>
        public GreaterOrEqualNode(OperatorNode node) : base(TokenType.OP_GEQ, node.Line, node.Offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}