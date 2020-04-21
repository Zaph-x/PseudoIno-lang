using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimesNode : MathOperatorNode
    {
        public TimesNode(int line, int offset) : base(TokenType.OP_TIMES, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}