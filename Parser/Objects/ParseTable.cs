using System;
using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        private Dictionary<Token, Dictionary<Token, ParseToken>> dict = 
            new Dictionary<Token, Dictionary<Token, ParseToken>>();

        public ParseToken this[Token key1, Token key2]
        {
            get
            {
                return dict[key1][key2];
            }

            set
            {
                if (!dict.ContainsKey(key1))
                {
                    dict[key1] = new Dictionary<Token, ParseToken>();
                }
                dict[key1][key2] = value;
            }
        }
    }
}