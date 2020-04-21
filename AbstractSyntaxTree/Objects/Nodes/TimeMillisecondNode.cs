using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeMillisecondNode : TimeNode
    {
        public TimeMillisecondNode(int line, int offset) : base(TokenType.TIME_MS, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
           visitor.Visit(this);
        }
    }
}