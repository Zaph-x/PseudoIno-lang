using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class RangeNode : MathOperatorNode
    {
        public ValNode LeftHand { get; set; }
        public ValNode RightHand { get; set; }
        
        public RangeNode(int line, int offset) : base(TokenType.RANGE, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}