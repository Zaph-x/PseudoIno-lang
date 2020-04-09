using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class AssugnNode : AstNode
    {
        public AssugnNode()
        {
            this.Type = TokenType.ASSIGN;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}