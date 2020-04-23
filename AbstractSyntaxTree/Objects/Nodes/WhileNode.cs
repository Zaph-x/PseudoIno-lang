using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class WhileNode : LoopNode, IScope
    {
        public ValNode ValNode { get; set; }
        public ExpressionNode ExpressionNode { get; set; }
        public List<StatementNode> Statements { get; set; }

        public WhileNode(int line, int offset) : base(TokenType.WHILE, line, offset)
        {
            Statements = new List<StatementNode>();
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}