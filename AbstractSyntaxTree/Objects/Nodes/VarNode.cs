using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class VarNode : ValNode
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