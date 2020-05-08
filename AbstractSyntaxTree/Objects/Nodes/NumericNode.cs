using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NumericNode : ValNode
    {
        //public int Value { get; set; }
        public float FValue {get;set;}
        public int IValue {get;set;}
        public NumericNode(string value , ScannerToken token) : base(token)
        {
            float _f;
            int _i;
            float.TryParse(value, out _f);
            int.TryParse(value, out _i);

            FValue = _f;
            IValue = _i;
        }

        public override object Accept(Visitor visitor) {
            return visitor.Visit(this);
        }
    }
}