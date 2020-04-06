using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EqualsNode : AstNode
    {
        public EqualsNode()
        {
            Type = TokenType.EQUALS;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}