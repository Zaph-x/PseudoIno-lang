

namespace Lexer.Objects
{
    /// <summary>
    /// A class representing a token in the source language.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The type of the token
        /// </summary>
        /// <value>Set in the constructor</value>
        public TokenType Type {get;set;}
        /// <summary>
        /// The value of the token.
        /// </summary>
        /// <value>Empty string if the value is not significant for the token</value>
        public string Value {get;set;}
        /// <summary>
        /// The line the token is placed on
        /// </summary>
        /// <value>Set in the constructor</value>
        public int Line {get;private set;}
        /// <summary>
        /// The offset of the value for the token
        /// </summary>
        /// <value>Set in the constructor</value>  
        public int Offset {get;private set;}

        /// <summary>
        /// The constructor for a token.
        /// </summary>
        /// <param name="type">The type of the token</param>
        /// <param name="val">The value of the token</param>
        /// <param name="line">The line of the token</param>
        /// <param name="offset">The offset of the token</param>
        public Token(TokenType type, string val, int line, int offset)
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
        public Token(TokenType type, int line, int offset)
        {
            this.Type = type;
            this.Value = "";
            this.Line = line;
            this.Offset = offset;
        }

        /// <summary>
        /// A function to format tokens, when printed to the screen or in other ways used as a string
        /// </summary>
        /// <returns>A string representation of the token</returns>
        public override string ToString() => $"({Line}:{Offset}) {Type} => {Value}";
    }
}
