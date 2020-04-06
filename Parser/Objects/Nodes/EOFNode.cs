using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EOFNode : AstNode
    {
        public EOFNode()
        {
            Type = TokenType.EOF;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}