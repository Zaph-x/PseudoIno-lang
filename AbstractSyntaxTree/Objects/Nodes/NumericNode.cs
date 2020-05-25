using Lexer.Objects;
using System.Globalization;
using System.Threading;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NumericNode : ValNode
    {
        //public int Value { get; set; }
        public float FValue {get;set;}
        public int IValue {get;set;}
        public NumericNode(string value , ScannerToken token) : base(token)
        {
                CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            float _f;
            int _i;
           // float.TryParse(value, CultureInfo.InvariantCulture.,, out _f);
            _f = float.Parse(value, customCulture);
            int.TryParse(value, out _i);

            FValue = _f;
            IValue = _i;
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor) {
            return visitor.Visit(this);
        }
    }
}