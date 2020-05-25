using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class WhileNode : StatementNode, IScope
    {
        public ExpressionNode Expression { get; set; }
        public List<StatementNode> Statements { get; set; }

        public WhileNode(int line, int offset) : base(TokenType.WHILE, line, offset)
        {
            Statements = new List<StatementNode>();
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}