using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class WithNode : AstNode
    {
        public WithNode(int line, int offset) : base(TokenType.WITH, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}