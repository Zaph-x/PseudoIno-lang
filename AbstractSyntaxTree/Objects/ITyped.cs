using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public interface ITyped
    {
        TokenType Type {get;set;}
        TypeContext SymbolType {get;set;}
    }
}