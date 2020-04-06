using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class TimeHourNode : AstNode
    {
        public TimeHourNode()
        {
            Type = TokenType.TIME_HR;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}