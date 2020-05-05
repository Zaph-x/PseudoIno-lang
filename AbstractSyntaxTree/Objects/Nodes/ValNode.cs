using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ValNode : AstNode, ITerm
    {
        public ValNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}