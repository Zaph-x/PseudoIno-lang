using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AndNode : MathOperatorNode
    {
        public AndNode (ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}