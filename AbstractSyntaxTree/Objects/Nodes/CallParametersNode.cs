using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class CallParametersNode : AstNode
    {
        public ValNode ValNode { get; set; }
        public CallParametersNode RightHand { get; set; }
        public CallParametersNode(int line, int offset) : base(TokenType.CALLPARAMS, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}