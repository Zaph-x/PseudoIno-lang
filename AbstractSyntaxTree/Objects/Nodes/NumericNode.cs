using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class NumericNode : ValNode
    {
        //public int Value { get; set; }
        public NumericNode(int line, int offset) : base(TokenType.NUMERIC, line, offset)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}