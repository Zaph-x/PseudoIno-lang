using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrNode : MathOperatorNode
    {
        public OrNode(int line, int offset) : base(TokenType.OP_OR, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}