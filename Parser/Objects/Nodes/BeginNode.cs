using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class BeginNode : AstNode
    {
        public BeginNode(int line, int offset) : base(TokenType.BEGIN, line, offset)
        {
        }

        public override void Accept(Visitor visitor) 
        {
            visitor.Visit(this);
        }

        public void AddChild(AstNode node)
        {
            if (node == null)
            {
                throw new NullReferenceException();
            }
            Children.Add(node);
        }
    }
}