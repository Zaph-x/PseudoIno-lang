using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class ValNode : AstNode
    {
        public ValNode()
        {
            Type = TokenType.VAL;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}