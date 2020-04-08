using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeMillisecondNode : AstNode
    {
        public TimeMillisecondNode()
        {
            Type = TokenType.TIME_MS;
        }
        public override void Accept(Visitor visitor)
        {
           visitor.Visit(this);
        }
    }
}