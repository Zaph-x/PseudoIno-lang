using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeSecondNode : TimeNode
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