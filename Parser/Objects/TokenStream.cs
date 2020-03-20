using System.Collections.Generic;
using Lexer.Objects;

namespace Parser.Objects
{
    /// <summary>
    /// A class representing a stream of tokens to parse.
    /// </summary>
    public class TokenStream
    {
        /// <summary>
        /// The current index in the stream
        /// </summary>
        /// <value>0 by default</value>
        private int Index { get; set; }
        /// <summary>
        /// The list of tokens to parse
        /// </summary>
        private List<Token> Tokens;
        
        /// <summary>
        /// The constructor of the TokenStream. This takes a list of tokens as parameter and uses it as the stream.
        /// </summary>
        /// <param name="tokens">A list of tokens provided by the scanner</param>
        public TokenStream(List<Token> tokens)
        {
            Tokens = tokens;
        }

        /// <summary>
        /// A peek function to get the next token in the stream, without advancing.
        /// </summary>
        /// <returns>The next token in the stream</returns>
        public Token Peek()
        {
            return Tokens[Index + 1];
        }

        /// <summary>
        /// Increments the index by one
        /// </summary>
        public void Advance()
        {
            Index += 1;
        }

        /// <summary>
        /// A function to get the token at the current index in the stream. This is done by accessing the list of tokens at the current index.
        /// </summary>
        /// <returns>The current token in the stream.</returns>
        public Token Current()
        {
            return Tokens[Index];
        }
    }
}