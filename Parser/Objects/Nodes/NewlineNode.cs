using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class NewlineNode : AstNode
    {
        public NewlineNode(int line, int offset) : base(TokenType.NEWLINE, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}