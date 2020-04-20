using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeMillisecondNode : AstNode
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