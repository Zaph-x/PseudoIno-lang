using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class GreaterNode : MathOperatorNode
    {
        public GreaterNode(int line, int offset) : base(TokenType.OP_GREATER, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}