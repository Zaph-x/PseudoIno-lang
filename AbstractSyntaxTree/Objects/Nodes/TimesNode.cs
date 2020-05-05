using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class TimesNode : MathOperatorNode
    {
        public TimesNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}