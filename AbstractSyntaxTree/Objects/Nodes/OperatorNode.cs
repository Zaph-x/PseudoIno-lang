using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class OperatorNode : AstNode
    {
        public OperatorNode(ScannerToken token) : base(token)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}