using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class PlusNode : MathOperatorNode
    {
        public PlusNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}