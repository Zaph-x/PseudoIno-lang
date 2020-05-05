using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LessNode : MathOperatorNode
    {
        public OrEqualNode OrEqualNode { get; set; }
        public LessNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}