using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the DPin node class
    /// It inherits the Pin node class
    /// </summary>
    public class DPinNode : PinNode
    {
        /// <summary>
        /// This is the DPin constructor
        /// The ID is set to the pin number
        /// </summary>
        /// <param name="pinNum">This is the pin number</param>
        /// <param name="token">This is the token</param>
        public DPinNode(string pinNum, ScannerToken token) : base(token)
        {
            this.Id = pinNum;
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}