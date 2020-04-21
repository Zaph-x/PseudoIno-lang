using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ExpressionNode : AstNode
    {
        //public TypeNode type { get; set; }
        public OperatorNode LeftHandSide { get; set; }
        public ValNode Middel { get; set; }
        public ExpressionNode RightHandSide { get; set; }

        public ExpressionNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}