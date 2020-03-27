using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        private List<TokenType>[,] dict = new List<TokenType>[65,65];
      
        
        private int xt = 0;

        public List<TokenType> this[TokenType key1, TokenType key2]
        {

            get
            {
                int keyx = (int) key1;
                int keyy = (int) key2;
                return dict[keyx, keyy];
            }

            set
            {
                int keyx = (int) key1;
                int keyy = (int) key2;
                dict[keyx, keyy] = value;
            }
        }
        
        public List<TokenType> this[int key1, int key2]
        {
            get
            {
                int keyx = (int) key1;
                int keyy = (int) key2;
                return dict[keyx, keyy];
            }

            set
            {
                int keyx = (int) key1;
                int keyy = (int) key2;
                dict[keyx, keyy] = value;
            }
        }

        public void InitTable()
        {
            for (int i = 0; i < 65; i++)
            {
                for (int j = 0; j < 65; j++)
                {
                    this[i,j] = new List<TokenType>();
                    this[i, j].Add(TokenType.ERROR);
                }
            }
            
            this[TokenType.STMNT, TokenType.VAR].Clear();
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.VAR);
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.ASSIGN);
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.ASSIGNMENT);

            this[TokenType.ASSIGNMENT, TokenType.VAL].Clear();
            this[TokenType.ASSIGNMENT, TokenType.VAL].Add(TokenType.VAL);
            this[TokenType.ASSIGNMENT, TokenType.VAL].Add(TokenType.EXPR);
            
            this[TokenType.EXPR, TokenType.LINEBREAK].Clear();
            this[TokenType.EXPR, TokenType.LINEBREAK].Add(TokenType.LINEBREAK);

        }
    }
}