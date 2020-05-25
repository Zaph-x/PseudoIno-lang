using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class MathOperatorNode : OperatorNode
    {
        public MathOperatorNode(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}