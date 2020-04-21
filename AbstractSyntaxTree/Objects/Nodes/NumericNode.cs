using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NumericNode : ValNode
    {
        //public int Value { get; set; }
        public float FValue {get;set;}
        public int IValue {get;set;}
        public NumericNode(string value ,int line, int offset) : base(TokenType.NUMERIC, line, offset)
        {
            float _f;
            int _i;
            float.TryParse(value, out _f);
            int.TryParse(value, out _i);

            FValue = _f;
            IValue = _i;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}