using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AndNode : MathOperatorNode
    {
        public AndNode (ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}