using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;

namespace Parser.Objects
{
    /// <summary>
    /// A stream of tokens that can not be indexed, but rather peeked or popped.
    /// </summary>
    public class TokenStream
    {
        /// <summary>
        /// The index of the underlying list
        /// </summary>
        /// <value>The index of the current token</value>
        private int Index { get; set; }
        /// <summary>
        /// A list of tokens provided by the scanner
        /// </summary>
        private List<ScannerToken> Tokens;

        /// <summary>
        /// The length of the token stream
        /// </summary>
        /// <value></value>
        public int Length { get => Tokens.Count; }

        /// <summary>
        /// Constructs a tokenstream. This is what is used in the parser, to parse from
        /// </summary>
        /// <param name="tokens">An IEnumerable of tokens gathered in the scanner</param>
        public TokenStream(IEnumerable<ScannerToken> tokens)
        {
            Tokens = new List<ScannerToken>();
            foreach (var var in tokens)
            {
                Tokens.Add(var as ScannerToken);
            }
        }

        /// <summary>
        /// Peeks the next token in the stream
        /// </summary>
        /// <returns>The next scanner token</returns>
        public ScannerToken Peek()
        {
            return Tokens[Index + 1];
        }

        /// <summary>
        /// Checks if the stream has reached the end.
        /// </summary>
        /// <returns>True if the stream is at the end. Else false</returns>
        public bool AtEnd()
        {
            if (Index + 1 > Length - 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Steps back in the stream
        /// </summary>
        public void Prev()
        {
            Index--;
        }
        /// <summary>
        /// This method peeks with a lookahead
        /// </summary>
        /// <param name="lookAhead">Amount of tokens to look ahead</param>
        /// <returns></returns>
        public ScannerToken Peek(int lookAhead)
        {
            return Tokens[Index + lookAhead];
        }
        /// <summary>
        /// Advances the stream one step
        /// </summary>
        public void Advance()
        {
            Index += 1;
        }
        /// <summary>
        /// A token signifying the end of file (EOF)
        /// </summary>
        /// <returns>EOF token</returns>
        public ScannerToken EOF => Tokens.First(token => token.Type == TokenType.EOF) as ScannerToken;
        /// <summary>
        /// A token signifying the start of a program (PROG)
        /// </summary>
        /// <returns>PROG token</returns>
        public ScannerToken PROG => Tokens[0] as ScannerToken;
        /// <summary>
        /// This method gets the current token of the current location in the stream.
        /// </summary>
        /// <returns>The current token</returns>
        public ScannerToken Current()
        {
            return Tokens[Index] as ScannerToken;
        }
    }
}