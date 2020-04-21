using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class IfStatementNode : StatementNode
    {
        //private node condition { get; set; }
        public ValNode Val { get; set; }
        public ExpressionNode Expression { get; set; }
        public StatementNode Statements { get; set; }
        public IfStatementNode(int line, int offset) : base(TokenType.IFSTMNT, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}