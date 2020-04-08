
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Lexer.Objects;

namespace Parser.Objects
{
    public abstract class AstNode
    {
        public AstNode Parent { get; set; }
        public List<AstNode> Children { get; private set; } = new List<AstNode>();
        public TokenType Type { get; set; }
        public string Value { get; set; }

        // TODO Pass these from the scanner Token
        long Line { get; set; }
        private int Offset { get; set; }

        public override string ToString() => $"line={Line}; offset={Offset}";

        public abstract void Accept(Visitor visitor);
    }
}