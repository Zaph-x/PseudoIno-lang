using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeMinuteNode : AstNode
    {
        public TimeMinuteNode()
        {
            Type = TokenType.TIME_MIN;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}