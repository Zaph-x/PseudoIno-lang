using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        //public List<(string A, string a)> LLTable = new List<(string A, string a)>();
        public int[][] LLTable = new int[][10];

        public ParseTable()
        {
            // LLTable init. Set all slots to error stages 
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    LLTable[i][j] = 0;
                }
            }
        }

        public string Get(int x, int y)
        {
            if (LLTable[x][y] == 0)
            {
                throw new InvalidSyntaxException("Parse table encountered invalid move");
            }
            return "hej";
        }
    }
}