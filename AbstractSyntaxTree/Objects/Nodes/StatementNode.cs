using System;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for statement node
    /// It inherits the Ast node class
    /// </summary>
    public abstract class StatementNode : AstNode
    {
        /// <summary>
        /// This is the constructor for statement node
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public StatementNode(TokenType type, int line, int offset) : base(type, line, offset) { }
        /// <summary>
        /// This is the constructor for statement node
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <param name="token">This is the token</param>
        public StatementNode(TokenType type, ScannerToken token) : base(type, token) { }
        /// <summary>
        /// This is the constructor for statement node
        /// </summary>
        /// <param name="token">this is the token</param>
        public StatementNode(ScannerToken token) : base(token) { }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public abstract override object Accept(Visitor visitor);
    }
}