namespace Lexer.Objects
{
    /// <summary>
    /// Scanner token class. Inherits from Token class
    /// </summary>
    public class ScannerToken : Token
    {
        /// <summary>
        /// The constructor for a token.
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="val">The value of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public ScannerToken(TokenType type, string val, int line, int offset)
        {
            this.Type = type;
            this.Value = val;
            this.Line = line;
            this.Offset = offset;
        }
        /// <summary>
        /// The constructor of a token with no value such as Operator tokens
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public ScannerToken(TokenType type, int line, int offset)
        {
            this.Type = type;
            this.Value = "";
            this.Line = line;
            this.Offset = offset;
        }
    }
}