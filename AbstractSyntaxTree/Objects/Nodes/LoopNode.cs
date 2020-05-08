using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class LoopNode : StatementNode
    {
        public LoopNode(ScannerToken token) : base(token)
        {
        }

        public LoopNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public abstract override object Accept(Visitor visitor);
    }
}