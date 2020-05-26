using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class APinNode : PinNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pinNum"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public APinNode(string pinNum, ScannerToken token) : base(token)
        {
            this.Id = pinNum;
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}