using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimesNode : MathOperatorNode
    {
        public TimesNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}