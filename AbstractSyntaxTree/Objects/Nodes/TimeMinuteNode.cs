using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimeMinuteNode : AstNode
    {
        public TimeMinuteNode(int line, int offset) : base(TokenType.TIME_MIN, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}