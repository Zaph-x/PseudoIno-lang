using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ValNode : AstNode, ITerm
    {
        public ValNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}