using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ModuloNode : MathOperatorNode
    {
        public ModuloNode( ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}