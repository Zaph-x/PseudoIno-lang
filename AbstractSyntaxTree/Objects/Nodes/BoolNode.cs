using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class BoolNode : ValNode
    {
        //public int Value { get; set; }
        public bool Value {get;set;}
        public BoolNode(string value , ScannerToken token) : base(token)
        {
            Value = bool.Parse(value);
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}