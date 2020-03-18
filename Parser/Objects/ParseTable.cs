using System;
using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        //public List<(string A, string a)> LLTable = new List<(string A, string a)>();
        public TokenType[,] LLTableOld;
        public Dictionary<TokenType, ParseToken> LLTable;
        private int tokenTypeMax = 0, nonTerminalMax = 0;
        public ParseTable()
        {
            // LLTable init. Set all slots to error stages
            
            while (Enum.IsDefined(typeof(TokenType),tokenTypeMax))
            {
                tokenTypeMax++;
            }
            
            while (Enum.IsDefined(typeof(TokenType),nonTerminalMax))
            {
                nonTerminalMax++;
            }
                
            LLTableOld = new TokenType[tokenTypeMax,nonTerminalMax];
            
            for (int i = 0; i < nonTerminalMax; i++)
            {
                for (int j = 0; j < tokenTypeMax; j++)
                {
                    LLTableOld[i,j] = TokenType.ERROR;
                }
            }
        }

        public TokenType Get(Token tokenA, Token tokenB)
        {
            int x = (int) tokenA.Type;
            int y = (int) tokenB.Type;
            
            if (LLTableOld[x,y] == TokenType.ERROR)
            {
                throw new InvalidSyntaxException("Parse table encountered invalid move");
            }
            return LLTableOld[x,y];
        }

        public void PrintParseTable()
        {
            for (int i = 0; i < nonTerminalMax; i++)
            {
                for (int j = 0; j < tokenTypeMax; j++)
                {
                    Console.Write($"{LLTableOld[i,j]} ");
                }
                Console.WriteLine();
            }
        }

        public void AddKeyToLLTable(TokenType tokenType, ParseToken parseToken)
        {
            if (LLTable.ContainsKey(tokenType))
                LLTable[tokenType] = parseToken;
            else
                LLTable.Add(tokenType,parseToken);
        }

        public ParseToken GetValueFromLLTable(TokenType tokenType)
        {
            if (LLTable.ContainsKey(tokenType))
                return LLTable[tokenType];
            throw new InvalidSyntaxException("Key not in LLTable");
        }

        public void FillWithCFG()
        {
            AddKeyToLLTable(TokenType.IF,ParseToken.IFSTMNT);
        }
    }
}