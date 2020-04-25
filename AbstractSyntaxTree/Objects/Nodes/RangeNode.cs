using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class RangeNode : MathOperatorNode
    {
        public NumericNode From { get; set; }
        public NumericNode To { get; set; }
        
        public RangeNode(int line, int offset) : base(TokenType.RANGE, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
           visitor.Visit(this);
        }
    }
}