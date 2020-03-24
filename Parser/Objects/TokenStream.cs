using System.Collections.Generic;
using Lexer.Objects;

namespace Parser.Objects
{
    public class TokenStream
    {
        private int Index { get; set; }
        private List<ScannerToken> Tokens;
        
        public TokenStream(List<ScannerToken> tokens)
        {
            Tokens = new List<ScannerToken>();
            Tokens.Add(new ScannerToken(TokenType.START,"",1,1));
            foreach (var var in tokens)
            {
                Tokens.Add(var);
            }
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