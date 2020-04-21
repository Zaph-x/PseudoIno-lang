using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class StringNode : ValNode
    {
        public StringNode(int line, int offset) : base(TokenType.STRING, line, offset)
        {
            
        }

        public override void Accept(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}