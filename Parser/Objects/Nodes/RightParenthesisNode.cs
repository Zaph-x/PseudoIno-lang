using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class RightParenthesisNode : AstNode
    {
        public RightParenthesisNode(int line, int offset) : base(TokenType.OP_RPAREN, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}