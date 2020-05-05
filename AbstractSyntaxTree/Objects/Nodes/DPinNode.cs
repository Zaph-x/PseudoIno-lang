using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DPinNode : PinNode
    {
        public DPinNode(string pinNum, ScannerToken token) : base(token)
        {
            this.Id = pinNum;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}