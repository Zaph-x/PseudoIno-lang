using System.Collections.Generic;
using System.Linq;
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
                Tokens.Add(var as ScannerToken);
            }
        }

        public ScannerToken Peek()
        {
            return Tokens[Index + 1];
        }

        public void Advance()
        {
            Index += 1;
        }

        public ScannerToken EOF => Tokens.First(token => token.Type == TokenType.EOF) as ScannerToken;
        public ScannerToken PROG => Tokens[0] as ScannerToken;
        public ScannerToken Current()
        {
            return Tokens[Index] as ScannerToken;
        }
    }
}