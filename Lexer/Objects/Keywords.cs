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
            {"begin", TokenType.BEGIN},
            {"in", TokenType.IN},
            {"do", TokenType.DO},
            {"is",TokenType.ASSIGN},
            {"end", TokenType.END},
            {"func", TokenType.FUNC},
            {"if", TokenType.IF},
            {"with", TokenType.WITH},
            {"else", TokenType.ELSE},
            {"call", TokenType.CALL},
            {"and", TokenType.OP_AND},
            {"or", TokenType.OP_OR},
            {"less", TokenType.OP_LESS},
            {"greater", TokenType.OP_GREATER},
            {"equal", TokenType.OP_EQUAL},
            {"not", TokenType.OP_NOT},
            // {"?", TokenType.OP_QUESTIONMARK},
            {"dpin",TokenType.DPIN},
            {"apin", TokenType.APIN},
            {"true", TokenType.BOOL},
            {"false", TokenType.BOOL},
            {"on", TokenType.BOOL},
            {"off", TokenType.BOOL},
            {"while", TokenType.WHILE},
            {"for", TokenType.FOR},
            {"return", TokenType.RETURN},
            {"wait", TokenType.WAIT},
            {"ms", TokenType.TIME_MS},
            {"s", TokenType.TIME_SEC},
            {"m", TokenType.TIME_MIN},
            {"h", TokenType.TIME_HR},
       };
    }
}