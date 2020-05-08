using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class VarNode : ValNode, IAssginable
    {
        public string Id {get;set;}
        public VarNode(string id,  ScannerToken token) : base(token)
        {
            this.Id = id;
        }
        public override string ToString() => $"{Id}";
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}