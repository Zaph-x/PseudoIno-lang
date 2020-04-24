using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ForNode : LoopNode,IScope
    {
        public ValNode ValNode { get; set; }
        public RangeNode RangeNode { get; set; } 
        public List<StatementNode> Statements { get; set; }
        
        public ForNode(int line, int offset) : base(TokenType.FOR, line, offset)
        {
            Statements = new List<StatementNode>();
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}