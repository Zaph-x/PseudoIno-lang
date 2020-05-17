using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LessNode : MathOperatorNode
    {
        public LessNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}