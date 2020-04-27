using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DeclParametersNode : AstNode
    {
        public List<VarNode> Parameters { get; set; } = new List<VarNode>();
        public DeclParametersNode(int line, int offset) : base(TokenType.CALLPARAMS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}