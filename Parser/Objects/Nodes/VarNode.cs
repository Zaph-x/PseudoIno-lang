using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class VarNode : AstNode
    {
        public VarNode(int line, int offset) : base(TokenType.VAR, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}