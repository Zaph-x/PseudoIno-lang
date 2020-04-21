using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
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