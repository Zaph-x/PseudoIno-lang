using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ReturnNode : StatementNode
    {
        public ExpressionNode ReturnValue {get;set;}

        public ReturnNode(int line, int offset) : base(TokenType.RETURN, line, offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}