using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualNode : BoolOperatorNode
    {
        public EqualNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}