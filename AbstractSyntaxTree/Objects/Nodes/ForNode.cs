using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ForNode : LoopNode,IScope
    {
        public ValNode ValNode { get; set; }
        public ExpressionNode ExpressionNode { get; set; } 
        public List<StatementNode> Statements { get; set; }
        
        public ForNode(int line, int offset) : base(TokenType.FOR, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}