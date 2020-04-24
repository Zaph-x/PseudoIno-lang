using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class BoolNode : ValNode
    {
        //public int Value { get; set; }
        public bool Value {get;set;}
        public BoolNode(string value ,int line, int offset) : base(TokenType.BOOL, line, offset)
        {
            Value = bool.Parse(value);
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}