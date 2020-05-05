using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class MinusNode : MathOperatorNode
    {
        public MinusNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}