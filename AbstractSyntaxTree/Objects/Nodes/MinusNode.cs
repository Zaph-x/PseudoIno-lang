using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class MinusNode : MathOperatorNode
    {
        public MinusNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}