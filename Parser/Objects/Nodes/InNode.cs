using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class InNode : AstNode
    {
        public InNode()
        {
            Type = TokenType.IN;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}