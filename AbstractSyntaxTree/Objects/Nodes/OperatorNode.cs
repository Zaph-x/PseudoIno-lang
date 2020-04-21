using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class OperatorNode : AstNode
    {
        public OperatorNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}