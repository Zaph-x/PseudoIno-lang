using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeMillisecondNode : TimeNode
    {
        public TimeMillisecondNode( ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
           return visitor.Visit(this);
        }
    }
}