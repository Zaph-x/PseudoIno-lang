using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the string node class
    /// It inherits from val node class
    /// </summary>
    public class StringNode : ValNode
    {
        /// <summary>
        /// This sets and returns the value
        /// </summary>
        string Value {get;set;}
        /// <summary>
        /// This is the constructor for string node
        /// Value is assigned to value
        /// </summary>
        /// <param name="value">This is the value</param>
        /// <param name="token">This is the token</param>
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