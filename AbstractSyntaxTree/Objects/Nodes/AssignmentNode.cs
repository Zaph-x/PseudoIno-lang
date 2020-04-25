using System.Linq.Expressions;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AssignmentNode : StatementNode
    {
        public IAssginable Var { get; set; }
        public IAssignment Assignment { get; set; }
        public AssignmentNode(int line, int offset) : base(TokenType.ASSIGNMENT, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}