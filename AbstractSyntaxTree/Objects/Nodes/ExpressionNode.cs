using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ExpressionNode : AstNode
    {
        //public TypeNode type { get; set; }
        public OperatorNode Operator { get; set; }
        public ValNode Value { get; set; }
        public ExpressionNode Expression { get; set; }

        public ExpressionNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}