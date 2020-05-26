using System;
using System.Collections.Generic;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for call node
    /// It inherits statement node and implements assignment, expression and term interfaces
    /// </summary>
    public class CallNode : StatementNode, IAssignment, IExpr, ITerm
    {
        /// <summary>
        /// This is the ID of the var node
        /// </summary>
        public VarNode Id { get; set; }
        /// <summary>
        /// This is a list of the parameters of the val node
        /// </summary>
        public List<ValNode> Parameters { get; set; } = new List<ValNode>();
        /// <summary>
        /// This is the lefthand side of an expression
        /// </summary>
        public ITerm LeftHand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// This is the operator
        /// </summary>
        public OperatorNode Operator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// This is the righthand side of an expression
        /// </summary>
        public IExpr RightHand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// This is the constructor for a call node
        /// </summary>
        /// <param name="line">This is the line of the call node</param>
        /// <param name="offset">This is the offset of the call node</param>
        public CallNode(int line, int offset) : base(TokenType.CALL, line, offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}