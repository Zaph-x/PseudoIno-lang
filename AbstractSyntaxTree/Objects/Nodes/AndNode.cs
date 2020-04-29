using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AndNode : MathOperatorNode
    {
        public AndNode(int line, int offset) : base(TokenType.OP_AND, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}