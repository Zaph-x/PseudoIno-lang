using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ForNode : LoopNode, IScope
    {
        public VarNode CountingVariable { get; set; }
        public NumericNode From { get; set; }
        public NumericNode To { get; set; }
        public List<StatementNode> Statements { get; set; }

        public ForNode(int line, int offset) : base(TokenType.FOR, line, offset)
        {
            Statements = new List<StatementNode>();
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}