using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualNode : BoolOperatorNode
    {
        public EqualNode(int line, int offset) : base(TokenType.EQUALS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}