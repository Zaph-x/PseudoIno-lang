using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class PinNode : ValNode, IAssginable
    {
        public string Id {get;set;}
        public PinNode(TokenType type, int line, int offset) : base(type,line, offset)
        {
            
        }

        public abstract override void Accept(Visitor visitor);
    }
}