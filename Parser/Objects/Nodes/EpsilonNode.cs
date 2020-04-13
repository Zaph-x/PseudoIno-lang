using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EpsilonNode : AstNode
    {
        public EpsilonNode(int line, int offset) : base(TokenType.EPSILON, line, offset)
        {
        }
        
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}