using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeHourNode : TimeNode
    {
        public TimeHourNode( ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}