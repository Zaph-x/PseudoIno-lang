
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public abstract class AstNode
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public TokenType SymbolType { get; set; } = TokenType.ERROR;

        // TODO Pass these from the scanner Token
        public long Line { get; set; }
        public int Offset { get; set; }
        
        public bool Visited { get; set; }
        
        public AstNode Parent { get; set; }

        public AstNode(TokenType type, int line, int offset) {
            this.Type = type;
            this.Line = line;
            this.Offset = offset;
            this.Visited = false;
        }

        public override string ToString() => $"type={Type}; line={Line}; offset={Offset}";

        public abstract void Accept(Visitor visitor);
    }
}