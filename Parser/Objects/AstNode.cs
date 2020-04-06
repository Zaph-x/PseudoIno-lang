
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Objects
{
    public abstract class AstNode
    {
        public AstNode Parent { get; set; }
        public List<AstNode> Children { get; private set; } = new List<AstNode>();
        public ParseToken Type { get; set; }
        public string Value { get; set; }

        // TODO Pass these from the scanner Token
        private long Line { get; set; }
        private int Offset { get; set; }

        void Accept(Visitor visitor);
    }
}