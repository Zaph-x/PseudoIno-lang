using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class WaitNode : AstNode
    {
        public WaitNode()
        {
            Type = TokenType.WAIT;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}