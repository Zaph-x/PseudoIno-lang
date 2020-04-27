using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class StringNode : ValNode
    {
        string Value {get;set;}
        public StringNode(string value, int line, int offset) : base(TokenType.STRING, line, offset)
        {
            Value = value;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}