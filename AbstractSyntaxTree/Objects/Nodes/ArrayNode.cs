using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ArrayNode : AstNode, IAssignment
    {
        public ArrayNode(int line, int offset) : base(TokenType.ARR, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}