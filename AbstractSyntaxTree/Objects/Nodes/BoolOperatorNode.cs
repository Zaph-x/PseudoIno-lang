using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the booloperator node class
    /// It inherits from the operator node
    /// </summary>
    public abstract class BoolOperatorNode : OperatorNode
    {
        /// <summary>
        /// This is the bool operator node constructor.
        /// </summary>
        /// <param name="token">This is the token for bool operator</param>
        public BoolOperatorNode(ScannerToken token) : base(token)
        {
        }
        /// <summary>
        /// This is the constructor for bool operator node
        /// </summary>
        /// <param name="type">This is the type of the bool operator</param>
        /// <param name="line">This is the line of the bool operator</param>
        /// <param name="offset">This is the offset of the bool operator</param>
        public BoolOperatorNode(TokenType type, int line, int offset) : base(type,line,offset)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}