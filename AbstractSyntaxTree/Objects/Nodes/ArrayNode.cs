using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// The Array node class
    /// It inherits Ast node
    /// It uses IAssignment, IExpr and ITerm interfaces
    /// </summary>
    public class ArrayNode : AstNode, IAssignment, IExpr, ITerm
    {
        /// <summary>
        /// is a list of the dimensions of the array, of the type numeric node
        /// </summary>
        public List<NumericNode> Dimensions { get; set; } = new List<NumericNode>();
        /// <summary>
        /// The Id of the array
        /// </summary>
        public VarNode ActualId { get; set; }
        /// <summary>
        /// This is the first access of the array
        /// </summary>
        public AssignmentNode _firstAccess;
        /// <summary>
        /// This sets and returns the first access of the assignment node
        /// </summary>
        public AssignmentNode FirstAccess { get => this._firstAccess; set => _firstAccess = _firstAccess == null ? value : _firstAccess; }
        /// <summary>
        /// This sets and returns the boolean value of has been accessed
        /// </summary>
        public bool HasBeenAccessed { get; set; } = false;

        #region Not implemented
        /// <summary>
        /// Inerited property not used
        /// </summary>
        /// <returns>Nothing</returns>
        public ITerm LeftHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        /// <summary>
        /// Inerited property not used
        /// </summary>
        /// <returns>Nothing</returns>
        public OperatorNode Operator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        /// <summary>
        /// Inerited property not used
        /// </summary>
        /// <returns>Nothing</returns>
        public IExpr RightHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        #endregion
        /// <summary>
        /// This is the constructor of array node
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public ArrayNode(int line, int offset) : base(TokenType.ARR, line, offset)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}