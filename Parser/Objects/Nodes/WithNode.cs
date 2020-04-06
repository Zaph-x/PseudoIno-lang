using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class WithNode : AstNode
    {
        public WithNode()
        {
            Type = TokenType.WITH;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}