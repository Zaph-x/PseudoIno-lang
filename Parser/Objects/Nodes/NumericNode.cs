using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class NumericNode : AstNode
    {
        public NumericNode()
        {
            Type = TokenType.NUMERIC;
        }
        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}