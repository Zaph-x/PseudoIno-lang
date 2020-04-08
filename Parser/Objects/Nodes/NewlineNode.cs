using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class NewlineNode : AstNode
    {
        public NewlineNode()
        {
            Type = TokenType.NEWLINE;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}