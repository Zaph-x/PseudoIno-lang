using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the node class for bool node
    /// It inherits a value node
    /// </summary>
    public class BoolNode : ValNode
    {
        /// <summary>
        /// This returns and set s a value
        /// </summary>
        public bool Value {get;set;}
        /// <summary>
        /// This is the constructor of the bool node
        /// It set the value to a bool
        /// </summary>
        /// <param name="value">This is the value of the boolean</param>
        /// <param name="token">This is the bool token</param>
        public BoolNode(string value , ScannerToken token) : base(token)
        {
            Value = bool.Parse(value);
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor) {
            return visitor.Visit(this);
        }
    }
}