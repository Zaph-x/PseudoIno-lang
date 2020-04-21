using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DPinNode : PinNode
    {
        public DPinNode(int line, int offset) : base(TokenType.DPIN, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}