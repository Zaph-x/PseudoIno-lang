using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class GreaterNode : MathOperatorNode
    {
        public OrEqualNode OrEqualNode { get; set; }
        public GreaterNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}