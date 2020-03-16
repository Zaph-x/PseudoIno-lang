using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        //public List<(string A, string a)> LLTable = new List<(string A, string a)>();
        public string[,] LLTable = new string[10,10];

        public ParseTable()
        {
            // LLTable init. Set all slots to error stages 
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    LLTable[i,j] = "error";
                }
            }
        }

        public string Get(int x, int y)
        {
            if (LLTable[x,y] == "error")
            {
                throw new InvalidSyntaxException("Parse table encountered invalid move");
            }
            return "test";
        }
    }
}