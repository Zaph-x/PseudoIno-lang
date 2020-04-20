using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseToken : Token
    {
        /// <summary>
        /// The constructor for a ParseToken.
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="val">The value of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public ParseToken(TokenType type, string val, int line, int offset)
        {
            this.Type = type;
            this.Value = val;
            this.Line = line;
            this.Offset = offset;
        }
        /// <summary>
        /// The constructor of a ParseToken with no value such as Operator tokens
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public ParseToken(TokenType type, int line, int offset)
        {
            this.Type = type;
            this.Value = "";
            this.Line = line;
            this.Offset = offset;
        }
    }
}