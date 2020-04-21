using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class VarNode : ValNode
    {
        string Id {get;set;}
        public VarNode(string id, int line, int offset) : base(TokenType.VAR, line, offset)
        {
            this.Id = id;
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}