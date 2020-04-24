using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class GreaterNode : MathOperatorNode
    {
        public OrEqualNode OrEqualNode { get; set; }
        public GreaterNode(int line, int offset) : base(TokenType.OP_GREATER, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}