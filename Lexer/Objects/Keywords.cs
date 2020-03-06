using System.Collections.Generic;

namespace Lexer.Objects
{
    public class Keywords
    {
        public static Dictionary<string,TokenType> Keys = new Dictionary<string,TokenType>()
        {
            {"is",TokenType.ASSIGN},
            {"end", TokenType.END},
            {"func", TokenType.FUNC},
            {"if", TokenType.IF},
            {"else", TokenType.ELSE},
            {"call", TokenType.CALL},
            {"and", TokenType.OP_AND},
            {"or", TokenType.OP_OR},
            {"less", TokenType.OP_LESS},
            {"greater", TokenType.OP_GREATER},
            {"equals", TokenType.OP_EQUAL},
            {"not", TokenType.OP_NOT},
            {"?", TokenType.OP_QUESTIONMARK},
            {"DPIN",TokenType.DPIN},
            {"APIN", TokenType.APIN},
       };
    }
}