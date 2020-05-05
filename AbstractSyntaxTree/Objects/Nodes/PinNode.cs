using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class PinNode : ValNode, IAssginable
    {
        public string Id {get;set;}
        public PinNode(ScannerToken token) : base(token)
        {
            
        }

        public abstract override void Accept(Visitor visitor);
    }
}