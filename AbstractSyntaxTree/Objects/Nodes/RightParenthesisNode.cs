using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class RightParenthesisNode : AstNode
    {
        public RightParenthesisNode(int line, int offset) : base(TokenType.OP_RPAREN, line, offset)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}