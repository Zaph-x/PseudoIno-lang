using Lexer.Objects;
using System.Runtime.CompilerServices;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for binary expression
    /// It inherits an expression node and the expression interface
    /// </summary>
    public class BinaryExpression : ExpressionNode, IExpr
    {
        /// <summary>
        /// Returns and sets the left hands side of the expression
        /// </summary>
        public ITerm LeftHand { get; set; }
        /// <summary>
        /// Returns and sets the operater of the expression
        /// </summary>
        public new OperatorNode Operator { get; set; }
        /// <summary>
        /// Returns and sets the right hand side of the expression
        /// </summary>
        public IExpr RightHand { get; set; }
        /// <summary>
        /// This is the constructor of binaryesxpressions
        /// </summary>
        /// <param name="token">This is the token</param>
        public BinaryExpression(ScannerToken token) : base(token)
        {
        }
        /// <summary>
        /// This is the constructor for binaryexpressions
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="token">This is the token</param>
        public BinaryExpression(TokenType type, ScannerToken token) : base(type, token)
        {
        }
        /// <summary>
        /// This is the constructor for binaryexpressions
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public BinaryExpression(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }
        /// <summary>
        /// This method converts the left, right and operator of the expression to a string
        /// </summary>
        /// <returns>This returns a string of the lefthand, operator and righthand</returns>
        public override string ToString() => $"{LeftHand} {Operator} {RightHand}";

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}