using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class RangeNode : MathOperatorNode
    {
        public NumericNode From { get; set; }
        public NumericNode To { get; set; }
        
        public RangeNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
           return visitor.Visit(this);
        }
    }
}