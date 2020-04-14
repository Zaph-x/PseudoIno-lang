using System.Collections.Generic;

namespace Lexer.Objects
{
    /// <summary>
    /// A class containing a dictionary of keywords
    /// </summary>
    public class Keywords
    {
        /// <summary>
        /// A dictionary of the possible keywords.
        /// </summary>
        /// <returns>A token type</returns>
        public static Dictionary<string,TokenType> Keys = new Dictionary<string,TokenType>()
        {
            {"is",TokenType.ASSIGN},
            {"end", TokenType.END},
            {"func", TokenType.FUNC},
            {"if", TokenType.IF},
            {"with", TokenType.WITH},
            {"numeric", TokenType.TYPE},
            {"string", TokenType.TYPE},
            {"bool", TokenType.TYPE},
            {"pin", TokenType.TYPE},
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
            {"true", TokenType.BOOL},
            {"false", TokenType.BOOL},
            {"loop", TokenType.LOOP_FN},
            {"while", TokenType.WHILE},
            {"for", TokenType.FOR},
            {"wait", TokenType.WAIT},
            {"ms", TokenType.TIME_MS},
            {"s", TokenType.TIME_SEC},
            {"m", TokenType.TIME_MIN},
            {"h", TokenType.TIME_HR},
       };
    }
}