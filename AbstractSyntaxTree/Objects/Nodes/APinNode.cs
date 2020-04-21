using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class APinNode : PinNode
    {
        public APinNode(int line, int offset) : base(TokenType.APIN, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}