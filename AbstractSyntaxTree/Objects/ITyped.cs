using Lexer.Objects;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// Using this interface properties for checking if the type is checked
    /// </summary>
    public interface ITyped
    {
        /// <summary>
        /// The type of the token
        /// </summary>
        /// <value> The value is set in the constructor where it is used</value>
        TokenType Type {get;set;}
        /// <summary>
        /// Symboltype helps the typechecker set the type of symbols
        /// </summary>
        /// <value>The symboltype is set in the scanner constructor and updated in the typechecker</value>
        TypeContext SymbolType {get;set;}
    }
}