using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DPinNode : PinNode
    {
        public DPinNode(string pinNum, ScannerToken token) : base(token)
        {
            this.Id = pinNum;
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}