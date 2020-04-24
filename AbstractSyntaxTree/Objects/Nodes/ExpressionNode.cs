using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ExpressionNode : AstNode, IAssginment, ITerm
    {
        //public TypeNode type { get; set; }
        public ITerm Term { get; set; }
        public OperatorNode Operator { get; set; }
        public ExpressionNode Expression { get; set; }
        public FollowTermNode FTerm
        {
            private get => null;
            set
            {
                this.Operator = FTerm.Operator;
                this.Expression = FTerm.Expression;
            }
        }

        public ExpressionNode(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {

        }
    }
}