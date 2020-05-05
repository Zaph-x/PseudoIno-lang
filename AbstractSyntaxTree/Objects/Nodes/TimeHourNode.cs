using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeHourNode : TimeNode
    {
        public TimeHourNode( ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}