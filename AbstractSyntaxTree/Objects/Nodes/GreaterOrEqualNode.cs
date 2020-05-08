using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class GreaterOrEqualNode : BoolOperatorNode
    {
        public GreaterOrEqualNode(OperatorNode node) : base(TokenType.OP_GEQ, node.Line, node.Offset)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}