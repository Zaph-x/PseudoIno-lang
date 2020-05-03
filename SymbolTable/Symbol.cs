using AbstractSyntaxTree.Objects;
using Lexer.Objects;

namespace SymbolTable
{
    public class Symbol
    {
        public string Name { get; set; }
        public TokenType TokenType { get; set; }
        public AstNode AstNode { get; set; }
        
        public Symbol(string name, TokenType type, AstNode astNode)
        {
            Name = name;
            TokenType = type;
            AstNode = astNode;
        }
    }
}