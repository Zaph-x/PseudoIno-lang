using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class APinNode : PinNode
    {
        public APinNode(string pinNum, ScannerToken token) : base(token)
        {
            this.Id = pinNum;
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}