using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeMillisecondNode : TimeNode
    {
        public TimeMillisecondNode( ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
           visitor.Visit(this);
        }
    }
}