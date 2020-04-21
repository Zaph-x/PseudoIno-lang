using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class IfStatementNode : StatementNode
    {
        //private node condition { get; set; }
        private AstNode Statement { get; set; }
        public IfStatementNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}