using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ModuloNode : MathOperatorNode
    {
        public ModuloNode( ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}