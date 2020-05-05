using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class BoolOperatorNode : OperatorNode
    {
        public BoolOperatorNode(ScannerToken token) : base(token)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}