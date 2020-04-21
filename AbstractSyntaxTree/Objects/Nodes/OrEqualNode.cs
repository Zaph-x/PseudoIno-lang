using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class OrEqualNode : BoolOperatorNode
    {
        public OrEqualNode(int line, int offset) : base(TokenType.OP_OREQUAL, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}