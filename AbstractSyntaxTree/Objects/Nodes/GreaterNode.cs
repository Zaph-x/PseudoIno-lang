using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class GreaterNode : MathOperatorNode
    {
        public GreaterNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}