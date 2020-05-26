using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the operator node class
    /// It inherits the Ast node class 
    /// </summary>
    public abstract class OperatorNode : AstNode
    {
        /// <summary>
        /// This is the constructor for operator node
        /// </summary>
        /// <param name="token">This is the token</param>
        public OperatorNode(ScannerToken token) : base(token)
        {
        }
        /// <summary>
        /// This is the constructor for operator node
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public OperatorNode(TokenType type, int line, int offset) : base(type,line,offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}