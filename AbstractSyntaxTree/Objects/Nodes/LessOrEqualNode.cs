using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LessOrEqualNode : BoolOperatorNode
    {
        public LessOrEqualNode(OperatorNode node) : base(TokenType.OP_LEQ, node.Line, node.Offset)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}