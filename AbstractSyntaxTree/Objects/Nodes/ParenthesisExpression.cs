using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for parenthesis expression node
    /// It inherits the expression node class and implements the expression interface
    /// </summary>
    public class ParenthesisExpression : ExpressionNode, IExpr
    {
        /// <summary>
        /// This sets and returns the value of lefthand side of expressions
        /// </summary>
        public ITerm LeftHand { get; set; }
        /// <summary>
        /// This sets and returns the value of operators
        /// </summary>
        public OperatorNode Operator { get; set; }
        /// <summary>
        /// This sets and returns the value of right hands side of expressions
        /// </summary>
        public IExpr RightHand { get; set; }
        /// <summary>
        /// This is the constructor of parenthesis espressions
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public ParenthesisExpression(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }

        /// <inheritdoc cref="System.Object.ToString()"/>
        public override string ToString() => $"({LeftHand} {Operator} {RightHand})";
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}