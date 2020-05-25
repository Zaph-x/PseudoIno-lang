using Lexer.Objects;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    public interface ITyped
    {
        TokenType Type {get;set;}
        TypeContext SymbolType {get;set;}
    }
}