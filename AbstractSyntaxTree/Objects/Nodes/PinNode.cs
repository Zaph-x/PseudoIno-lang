using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class PinNode : ValNode, IAssginable
    {
        public string Id {get;set;}
        public PinNode(ScannerToken token) : base(token)
        {
            
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}