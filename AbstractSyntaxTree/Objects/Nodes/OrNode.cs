using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrNode : MathOperatorNode
    {
        public OrNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}