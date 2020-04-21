using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class InNode : AstNode
    {
        public InNode(int line, int offset) : base(TokenType.IN, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}