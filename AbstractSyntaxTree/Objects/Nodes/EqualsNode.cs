using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualsNode : AstNode
    {
        public EqualsNode(int line, int offset) : base(TokenType.EQUALS, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}