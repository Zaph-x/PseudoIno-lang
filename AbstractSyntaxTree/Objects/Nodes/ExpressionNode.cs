using System;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the expression node class
    /// It inherits the statement node class and the assignment, term and expression interface
    /// </summary>
    public abstract class ExpressionNode : StatementNode, IAssignment, ITerm, IExpr
    {
        /// <summary>
        /// This returns and set the value of the expression node parent
        /// </summary>
        private ExpressionNode _Parent {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public new ExpressionNode Parent {get => _Parent;set {
            this._Parent = value;
            this._Parent.Child = this;
        }}
        /// <summary>
        /// This returns and sets the value of the child to the expression
        /// </summary>
        public ExpressionNode Child {get;set;}
        /// <summary>
        /// This is the constructor for expressions
        /// </summary>
        /// <param name="token">This is the token</param>
        protected ExpressionNode(ScannerToken token) : base(token)
        {
        }
        /// <summary>
        /// This is the constructor for expression node
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="token">This is the token</param>
        protected ExpressionNode(TokenType type, ScannerToken token) : base(type, token)
        {
        }
        /// <summary>
        /// This is the constructor for expressions node
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        protected ExpressionNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }
        /// <summary>
        /// This return and sets the value of the lefthand sideof the expression
        /// </summary>
        public ITerm LeftHand { get; set; }
        /// <summary>
        /// This return and sets the value of the operator
        /// </summary>
        public new OperatorNode Operator { get; set; }
        /// <summary>
        /// This return and sets the value of the righthand sideof the expression
        /// </summary>
        public IExpr RightHand { get; set; }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}