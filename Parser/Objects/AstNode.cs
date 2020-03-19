using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Objects
{
    public class AstNode
    {
        public AstNode Parent { get; set; }
        public List<AstNode> Children { get; private set; } = new List<AstNode>();
        public ParseToken Type { get; set; }
        public string Value { get; set; }

        // TODO Pass these from the scanner Token
        private long Line { get; set; }
        private int Offset { get; set; }

        // For Terminals
        public AstNode(ParseToken type, string value, long line, int offset)
        {
            this.Type = type;
            this.Value = value;
            this.Line = line;
            this.Offset = offset;
        }

        // For Non-Terminals
        public AstNode(ParseToken type, long line, int offset)
        {
            this.Type = type;
            this.Value = "";
            this.Line = line;
            this.Offset = offset;
        }
        public void AddChild([NotNull]AstNode child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild([NotNull]AstNode child)
        {
            Children.Remove(child);
            child.Parent = null;
        }
        
        public void RemoveChild(int index)
        {
            AstNode child = Children[index];
            Children.RemoveAt(index);
            child.Parent = null;
        }

    }
}