using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class MathOperatorNode : OperatorNode
    {
        public MathOperatorNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}