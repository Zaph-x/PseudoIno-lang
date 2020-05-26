using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// The class for Analog pin node
    /// Inherits from the pin node class
    /// </summary>
    public class APinNode : PinNode
    {
        /// <summary>
        /// The constructor for APin node
        /// Id is set to the pin number
        /// </summary>
        /// <param name="pinNum">The number of the pin</param>
        /// <param name="token">The name of the token</param>
        public APinNode(string pinNum, ScannerToken token) : base(token)
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