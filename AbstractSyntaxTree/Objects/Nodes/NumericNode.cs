using Lexer.Objects;
using System.Globalization;
using System.Threading;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the numeric node class
    /// It inherits the val node class
    /// </summary>
    public class NumericNode : ValNode
    {
        //public int Value { get; set; }
        /// <summary>
        /// This is sets and returns the floating point value 
        /// </summary>
        public float FValue {get;set;}
        /// <summary>
        /// This sets and returns the integer value
        /// </summary>
        public int IValue {get;set;}
        /// <summary>
        /// This is the constructor for numeric node
        /// It enables the program to use period seperator for floating points 
        /// FValue is set to be floating points and IValue is set to be integer
        /// </summary>
        /// <param name="value"></param>
        /// <param name="token"></param>
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