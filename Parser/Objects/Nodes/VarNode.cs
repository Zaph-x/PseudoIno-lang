using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class VarNode : AstNode
    {
        public VarNode()
        {
            Type = TokenType.VAR;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}