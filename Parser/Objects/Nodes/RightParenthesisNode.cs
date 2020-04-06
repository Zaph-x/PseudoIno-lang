using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class RightParenthesisNode : AstNode
    {
        public RightParenthesisNode()
        {
            Type = TokenType.OP_RPAREN;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}