using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ElseifStatementNode : StatementNode, IScope
    {
        public ValNode Val { get; set; }
        public ExpressionNode Expression { get; set; }
        public List<StatementNode> Statements { get; set; }
        
        public ElseifStatementNode(int line, int offset) : base(TokenType.ELSEIFSTMNT, line, offset)
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