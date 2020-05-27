using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for less or equal node
    /// It inherits the bool operator node
    /// </summary>
    public class LessOrEqualNode : BoolOperatorNode
    {
        /// <summary>
        /// This is the contructor for the less or equal node
        /// </summary>
        /// <param name="node">This is the name of the node</param>
        public LessOrEqualNode(OperatorNode node) : base(TokenType.OP_LEQ, node.Line, node.Offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}