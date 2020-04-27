using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DPinNode : PinNode
    {
        public DPinNode(string pinNum, int line, int offset) : base(TokenType.DPIN, line, offset)
        {
            this.Id = pinNum;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}