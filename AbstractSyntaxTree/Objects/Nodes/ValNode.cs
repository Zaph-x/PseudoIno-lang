using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ValNode : AstNode
    {
        public TokenType Type { get; set; }
        public ValNode(ScannerToken token, int line, int offset) : base(TokenType.VAL, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}