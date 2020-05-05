using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrEqualNode : BoolOperatorNode
    {
        public OrEqualNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}