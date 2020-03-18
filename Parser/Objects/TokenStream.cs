using System.Collections.Generic;
using Lexer.Objects;

namespace Parser.Objects
{
    public class StreamToken
    {
        private int Index { get; set; }
        private List<Token> Tokens;
        
        public StreamToken(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public Token Peek()
        {
            return Tokens[Index + 1];
        }

        public void Advance()
        {
            Index += 1;
        }

        public Token Current()
        {
            return Tokens[Index];
        }
    }
}