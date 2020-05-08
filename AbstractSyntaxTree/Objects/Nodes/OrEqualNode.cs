using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrEqualNode : BoolOperatorNode
    {
        public TokenType UnderlyingComparer {get;set;}
        public OrEqualNode(TokenType underlying, ScannerToken token) : base(token)
        {
            this.UnderlyingComparer = underlying;
        }

        public OrEqualNode(OperatorNode node) : base(TokenType.OP_OREQUAL, node.Line, node.Offset)
        {
            this.UnderlyingComparer = node.Type;
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}