using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DivideNode : MathOperatorNode
    {
        public DivideNode(int line, int offset) : base(TokenType.OP_DIVIDE, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}