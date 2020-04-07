using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class BeginNode : AstNode
    {
        public BeginNode()
        {
            this.Type = TokenType.BEGIN;
        }

        public override void Accept(Visitor visitor) 
        {
            visitor.Visit(this);
        }
    }
}