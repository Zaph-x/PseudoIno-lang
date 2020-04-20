using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class DoNode : AstNode
    {
        public DoNode(int line, int offset) : base(TokenType.DO, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}