using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class FollowTermNode : AstNode, IAssignment
    {
        //public TypeNode type { get; set; }
        public OperatorNode Operator {get;set;}
        public ExpressionNode Expression {get;set;}

        public FollowTermNode(ScannerToken token) : base(TokenType.FOLLOWTERM, token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            
        }
    }
}