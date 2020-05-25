using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ValNode : AstNode, ITerm
    {
        public ValNode(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}