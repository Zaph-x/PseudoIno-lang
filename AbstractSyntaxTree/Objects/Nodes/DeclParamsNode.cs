using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DeclParametersNode : AstNode
    {
        public List<VarNode> Parameters { get; set; } = new List<VarNode>();
        public DeclParametersNode(ScannerToken token) : base(TokenType.DECLPARAMS, token)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}