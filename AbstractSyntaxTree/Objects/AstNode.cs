
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Lexer.Objects;
using System;
/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// The AST node class containin the information about the construction of an AST node
    /// </summary>
    public abstract class AstNode
    {
        /// <summary>
        /// The type of token.
        /// </summary>
        /// <value>Set in the constructor</value>
        public TokenType Type { get; set; }
        /// <summary>
        /// Value of the token.
        /// </summary>
        /// <value>Empty string if the value is not significant for the token</value>
        public string Value { get; set; }
        /// <summary>
        /// Symboltype helps the typechecker set the type of symbols
        /// </summary>
        /// <value> The symboltype is set in the scanner constructor and updated in the typechecker</value>
        public TypeContext SymbolType { get; set; }
        /// <summary>
        /// The line of the token
        /// </summary>
        /// <value>Set in the constructor</value>
        public int Line { get; set; }
        /// <summary>
        /// The offset of the value for the token
        /// </summary>
        /// <value>Set in the constructor</value>
        public int Offset { get; set; }
        /// <summary>
        /// Determans if the node has been visited
        /// </summary>
        /// <value>Value is set to false in constructor by default</value>
        public bool Visited { get; set; }
        /// <summary>
        /// Set the node as a parent node. A parent node have children
        /// </summary>
        /// <value>Set in each node in the parser</value>
        public AstNode Parent { get; set; }
        /// <summary>
        /// The base constructor for an AST nodes array, operator, program and statement nodes
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public AstNode(TokenType type, int line, int offset)
        {
            this.Type = type;
            this.Line = line;
            this.Offset = offset;
            this.Visited = false;
        }
        /// <summary>
        /// The base constructor for the AST nodes, operator, statement, time, val nodes
        /// </summary>
        /// <param name="token">The kind of token</param>
        public AstNode(ScannerToken token)
        {
            this.Value = token.Value;
            this.Type = token.Type;
            this.Line = token.Line;
            this.Offset = token.Offset;
            this.SymbolType = token.SymbolicType;
            this.Visited = false;
        }
        /// <summary>
        /// The constructor for an AST node. This constructor is used for the terminal statement node
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="token">The kind of token</param>
        public AstNode(TokenType type, ScannerToken token)
        {
            this.Type = type;
            this.Value = token.Value;
            this.Line = token.Line;
            this.Offset = token.Offset;
            this.SymbolType = token.SymbolicType;
            this.Visited = false;
        }
        /// <summary>
        /// A method that converts the type of the token to a string
        /// </summary>
        /// <returns>A string of type</returns>
        public override string ToString() => $"Type={Type}";
        
        /// <summary>
        /// A Boolean method that checks if the instance of type can be assigned to the current type of the token
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <returns>Return true or false if the token can't be assigned to the current tyep</returns>
        public bool IsType(Type type)
        {
            return this.GetType().IsAssignableFrom(type);
        }
        /// <summary>
        /// An accept method that accepts the AST node in the tree when the node is visited.
        /// </summary>
        /// <param name="visitor">visitor object used to visit the node.</param>
        /// <returns>Null, String, or TypeContext based on the visitor used.</returns>
        public abstract object Accept(Visitor visitor);
    }
}