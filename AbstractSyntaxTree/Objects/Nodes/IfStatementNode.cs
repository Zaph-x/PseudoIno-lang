using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class IfStatementNode : StatementNode , IScope
    {
        //private node condition { get; set; }
        public ExpressionNode Expression { get; set; }
        public List<StatementNode> Statements { get; set; }
        public IfStatementNode(int line, int offset) : base(TokenType.IFSTMNT, line, offset)
        {
            Statements = new List<StatementNode>();
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}