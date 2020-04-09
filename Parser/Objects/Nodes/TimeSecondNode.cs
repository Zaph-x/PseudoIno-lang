using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeSecondNode : AstNode
    {
        public TimeSecondNode()
        {
            Type = TokenType.TIME_SEC;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}