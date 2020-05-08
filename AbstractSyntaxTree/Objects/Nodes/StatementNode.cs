using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class StatementNode : AstNode
    {
        public StatementNode(TokenType type, int line, int offset) : base(type, line, offset) { }

        public StatementNode(TokenType type, ScannerToken token) : base(type, token) { }
        public StatementNode(ScannerToken token) : base(token) { }

        public abstract override object Accept(Visitor visitor);
    }
}