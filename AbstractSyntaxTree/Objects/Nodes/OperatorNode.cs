using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class OperatorNode : AstNode
    {
        public OperatorNode(ScannerToken token) : base(token)
        {
        }

        public OperatorNode(TokenType type, int line, int offset) : base(type,line,offset)
        {
        }

        public abstract override object Accept(Visitor visitor);
    }
}