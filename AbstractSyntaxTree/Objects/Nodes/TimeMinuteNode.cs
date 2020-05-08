using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeMinuteNode : TimeNode
    {
        public TimeMinuteNode( ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}