using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DivideNode : MathOperatorNode
    {
        public DivideNode(ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}