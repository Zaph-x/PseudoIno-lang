using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ModuloNode : MathOperatorNode
    {
        public ModuloNode(int line, int offset) : base(TokenType.OP_MODULO, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}