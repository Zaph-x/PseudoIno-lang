using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class FunctionDefinitonNode : FuncNode
    {
        public FunctionDefinitonNode(string name, int line, int offset) : base(TokenType.FUNCTION, line, offset)
        {
            this.Value = name;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}