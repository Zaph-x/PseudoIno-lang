using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ElseStatementNode : StatementNode, IScope
    {
        public List<StatementNode> Statements { get; set; }
        
        public ElseStatementNode(int line, int offset) : base(TokenType.ELSE, line, offset)
        {
            Statements = new List<StatementNode>();
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}