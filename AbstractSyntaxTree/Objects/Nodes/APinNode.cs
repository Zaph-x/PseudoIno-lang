using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class APinNode : PinNode
    {
        public APinNode(string pinNum, int line, int offset) : base(TokenType.APIN, line, offset)
        {
            this.PinNumber = pinNum;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}