using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EpsilonNode : AstNode
    {
        public EpsilonNode(int line, int offset) : base(TokenType.EPSILON, line, offset)
        {
        }
        
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}