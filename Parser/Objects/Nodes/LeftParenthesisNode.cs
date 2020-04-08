using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class LeftParenthesisNode : AstNode
    {
        public LeftParenthesisNode(int line, int offset) : base(TokenType.OP_LPAREN, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}