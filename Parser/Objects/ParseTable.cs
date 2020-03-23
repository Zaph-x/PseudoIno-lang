using System;
using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        private Dictionary<TokenType, Dictionary<TokenType, List<ParseToken>>> dict = 
            new Dictionary<TokenType, Dictionary<TokenType, List<ParseToken>>>();

        private int xt = 0;
        public List<ParseToken> this[TokenType key1, TokenType key2]
        {
            get
            {
                return dict[key1][key2];
            }

            set
            {
                if (!dict.ContainsKey(key1))
                {
                    dict[key1] = new Dictionary<TokenType, List<ParseToken>>();
                }
                dict[key1][key2] = value;
            }
        }

        public void InitTable()
        {
            ScannerToken scannerToken = new ScannerToken(TokenType.VAR,"a",1,1);
            ParseToken parseToken = new ParseToken(TokenType.STMNT,"",1,1);

            Console.WriteLine($"Table: {parseToken.GetHashCode()}");
            xt = parseToken.GetHashCode();
            
            List<ParseToken> parseTokens = new List<ParseToken>();
            ParseToken parseToken2 = new ParseToken(TokenType.VAR,"",1,1);
            ParseToken parseToken3 = new ParseToken(TokenType.ASSIGN,"",1,1);
            ParseToken parseToken4 = new ParseToken(TokenType.ASSIGNMENT,"",1,1);
            parseTokens.Add(parseToken2);
            parseTokens.Add(parseToken3);
            parseTokens.Add(parseToken4);

            this[parseToken.Type, scannerToken.Type].Add(parseTokens[0]);
            this[parseToken.Type, scannerToken.Type].Add(parseTokens[1]);
            this[parseToken.Type, scannerToken.Type].Add(parseTokens[2]);
            List<ParseToken> d = this[parseToken.Type, scannerToken.Type];

            scannerToken = new ScannerToken(TokenType.VAL,"5",1,1);
            parseToken = new ParseToken(TokenType.ASSIGNMENT,"",1,1);
            
            parseTokens.Clear();
            parseToken2 = new ParseToken(TokenType.VAL,"",1,1);
            parseToken3 = new ParseToken(TokenType.EXPR,"",1,1);
            parseTokens.Add(parseToken2);
            parseTokens.Add(parseToken3);

            this[parseToken.Type, scannerToken.Type].Add(parseTokens[0]);
            this[parseToken.Type, scannerToken.Type].Add(parseTokens[1]);

            scannerToken = new ScannerToken(TokenType.LINEBREAK,"",1,1);
            parseToken = new ParseToken(TokenType.EXPR,"",1,1);
            
            parseTokens.Clear();
            this[parseToken.Type, scannerToken.Type].Add(parseTokens[0]);

        }
    }
}