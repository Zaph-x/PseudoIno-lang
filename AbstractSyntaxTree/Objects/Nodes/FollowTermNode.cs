using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class FollowTermNode : AstNode, IAssignment
    {
        //public TypeNode type { get; set; }
        public OperatorNode Operator {get;set;}
        public ExpressionNode Expression {get;set;}

        public FollowTermNode(int line, int offset) : base(TokenType.FOLLOWTERM, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            
        }
    }
}