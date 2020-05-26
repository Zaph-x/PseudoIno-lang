using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the Pin node class
    /// It inherits the val node class and the assignable interface
    /// </summary>
    public abstract class PinNode : ValNode, IAssginable
    {
        /// <summary>
        /// This sets and returns the ID
        /// </summary>
        public string Id {get;set;}
        /// <summary>
        /// this is the constructor of Pin node
        /// </summary>
        /// <param name="token">This is the token</param>
        public PinNode(ScannerToken token) : base(token)
        {
            
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}