using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class TimeNode : AstNode
    {
        public TimeNode( ScannerToken token) : base(token)
        {
        }
        public abstract override object Accept(Visitor visitor);
    }
}