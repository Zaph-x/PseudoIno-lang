using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class PlusNode : MathOperatorNode
    {
        public PlusNode(int line, int offset) : base(TokenType.OP_PLUS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}