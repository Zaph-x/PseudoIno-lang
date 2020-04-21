using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ValNode : AstNode
    {
        //public TokenType Type { get; set; }
        public ValNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}