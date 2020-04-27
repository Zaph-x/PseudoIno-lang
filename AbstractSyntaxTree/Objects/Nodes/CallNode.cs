using System;
using System.Collections.Generic;
using Lexer.Objects;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class CallNode : StatementNode, IAssignment
    {
        public VarNode Id { get; set; }
        public List<ValNode> Parameters { get; set; } = new List<ValNode>();
        public CallNode(int line, int offset) : base(TokenType.CALL, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}