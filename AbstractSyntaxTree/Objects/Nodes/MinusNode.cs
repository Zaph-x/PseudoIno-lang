using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class MinusNode : MathOperatorNode
    {
        public MinusNode(int line, int offset) : base(TokenType.OP_MINUS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}