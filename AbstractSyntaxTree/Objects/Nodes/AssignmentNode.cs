using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AssignmentNode : StatementNode
    {
        public AstNode LeftHand { get; set; }
        public AstNode RightHand { get; set; }
        public AstNode ExpressionHand { get; set; }
        public AssignmentNode(int line, int offset) : base(TokenType.ASSIGNMENT, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}