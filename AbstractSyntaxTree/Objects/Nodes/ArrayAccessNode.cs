using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// The array access node class
    /// It inherits Ast node
    /// It implements IAssignment, IExpr and ITerm interfaces
    /// </summary>
    public class ArrayAccessNode : AstNode, IAssignment, IExpr, ITerm
    {
        /// <summary>
        /// This is a list of accessers of the array with the type Valnode
        /// </summary>
        public List<ValNode> Accesses {get;set;} = new List<ValNode>();
        /// <summary>
        /// This is the actual array
        /// </summary>
        public ArrayNode Actual {get;set;}

        #region Not implemented
        public ITerm LeftHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public OperatorNode Operator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IExpr RightHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        #endregion
        /// <summary>
        /// The constructor for Array access nodes
        /// Actual is the actual array and has been accessed is set to true when visited.
        /// </summary>
        /// <param name="array">Is the name of the array</param>
        /// <param name="line">The line of the array</param>
        /// <param name="offset">The Array offset</param>
        public ArrayAccessNode(ArrayNode array, int line, int offset) : base(TokenType.ARRAYACCESSING, line, offset)
        {
            Actual = array;
            array.HasBeenAccessed = true;
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}