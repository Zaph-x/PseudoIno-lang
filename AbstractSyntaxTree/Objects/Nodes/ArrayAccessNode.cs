using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ArrayAccessNode : AstNode, IAssignment, IExpr, ITerm
    {
        public List<ValNode> Accesses {get;set;} = new List<ValNode>();
        public ArrayNode Actual {get;set;}

        #region Not implemented
        public ITerm LeftHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public OperatorNode Operator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IExpr RightHand { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        #endregion

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