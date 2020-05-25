using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class StringNode : ValNode
    {
        string Value {get;set;}
        public StringNode(string value,  ScannerToken token) : base(token)
        {
            Value = value;
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}