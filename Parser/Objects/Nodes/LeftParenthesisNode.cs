using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class LeftParenthesisNode : AstNode
    {
        public LeftParenthesisNode()
        {
            Type = TokenType.OP_LPAREN;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}