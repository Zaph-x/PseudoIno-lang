using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class DoNode : AstNode
    {
        public DoNode()
        {
            Type = TokenType.DO;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}