using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeHourNode : TimeNode
    {
        public TimeHourNode(int line, int offset) : base(TokenType.TIME_HR, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}