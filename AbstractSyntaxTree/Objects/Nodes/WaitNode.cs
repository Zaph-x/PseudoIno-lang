using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is teh wait node class
    /// It inherits the statement node class
    /// </summary>
    public class WaitNode : StatementNode
    {
        /// <summary>
        /// This sets and returns the time modifier with the type time node
        /// </summary>
        public TimeNode TimeModifier {get;set;}
        /// <summary>
        /// This sets and returns the time amount with the type numeric node
        /// </summary>
        public NumericNode TimeAmount { get; set; }
        /// <summary>
        /// This is the constructor for wait node
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public WaitNode(int line, int offset) : base(TokenType.WAIT, line, offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}