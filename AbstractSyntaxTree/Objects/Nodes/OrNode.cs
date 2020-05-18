using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrNode : MathOperatorNode
    {
        public OrNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}