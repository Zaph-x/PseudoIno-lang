using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class BoolOperatorNode : OperatorNode
    {
        public BoolOperatorNode(ScannerToken token) : base(token)
        {
        }

        public BoolOperatorNode(TokenType type, int line, int offset) : base(type,line,offset)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}