using AbstractSyntaxTree.Objects;
using Lexer.Objects;

namespace SymbolTable
{
    /// <summary>
    /// The symbol object being passed to the symbol table
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// The name of the symbol
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the symbol
        /// </summary>
        public TokenType TokenType { get; set; }
        /// <summary>
        /// The AST node the symbol is representing
        /// </summary>
        public AstNode AstNode { get; set; }
        /// <summary>
        /// Is the symbol a reference
        /// </summary>
        public bool IsRef { get; set; }
        /// <summary>
        /// The constructor of a symbol
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <param name="type">The type of the symbol</param>
        /// <param name="isRef">Bool specifying if the symbol is a reference</param>
        /// <param name="astNode">The AST node of the symbol</param>
        public Symbol(string name, TokenType type, bool isRef, AstNode astNode)
        {
            Name = name;
            TokenType = type;
            IsRef = isRef;
            AstNode = astNode;
        }
    }
}