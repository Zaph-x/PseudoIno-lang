using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeSecondNode : AstNode
    {
        public TimeSecondNode(int line, int offset) : base(TokenType.TIME_SEC, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}