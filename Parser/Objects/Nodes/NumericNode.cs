using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class NumericNode : AstNode
    {
        public int Value { get; set; }
        public NumericNode(int line, int offset) : base(TokenType.NUMERIC, line, offset)
        {
        }
        public override void Accept(Visitor visitor)
        {
           visitor.Visit(this);
        }
    }
}