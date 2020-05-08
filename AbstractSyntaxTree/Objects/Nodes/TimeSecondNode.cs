using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeSecondNode : TimeNode
    {
        public TimeSecondNode( ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}