using System.Linq.Expressions;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the assignment node class
    /// It inherits statement node and the expression interface
    /// </summary>
    public class AssignmentNode : StatementNode, IExpr
    {
        /// <summary>
        /// This is the right hand side of an expresstion
        /// </summary>
        public IExpr RightHand { get; set; }
        /// <summary>
        /// This is the lefthand side of an expression, which is the term 
        /// </summary>
        public ITerm LeftHand { get; set; }
        /// <summary>
        /// This is returns and sets the operator
        /// </summary>
        public OperatorNode Operator { get; set; }
        /// <summary>
        /// This is the constructor for the assignment node
        /// </summary>
        /// <param name="line">This is the line of the Assignment</param>
        /// <param name="offset">This is the offset of the assignment</param>
        public AssignmentNode(int line, int offset) : base(TokenType.ASSIGNMENT, line, offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}