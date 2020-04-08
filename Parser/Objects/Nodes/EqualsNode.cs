using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EqualsNode : AstNode
    {
        public EqualsNode()
        {
            Type = TokenType.EQUALS;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}