using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LessNode : MathOperatorNode
    {
        public LessNode(int line, int offset) : base(TokenType.OP_LESS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}