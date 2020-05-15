using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTableCopy
    {
        public Dictionary<TokenType, Dictionary<TokenType, ParseAction>> ParseTable { get; set; }

        public ParseAction this[TokenType key1, TokenType key2]
        {
            get => ParseTable[key1][key2];
            set => ParseTable[key1][key2] = value;
        }

        public ParseTableCopy()
        {
            Init();
        }

        private void Init()
        {

            this.ParseTable = new Dictionary<TokenType, Dictionary<TokenType, ParseAction>>();

            foreach (TokenType type in Enum.GetValues(typeof(TokenType)))
            {
                ParseTable.Add(type, new Dictionary<TokenType, ParseAction>());
                foreach (TokenType innerType in Enum.GetValues(typeof(TokenType)))
                {
                    ParseTable[type].Add(innerType, ParseAction.Error());
                }
            }
        }
    }
}