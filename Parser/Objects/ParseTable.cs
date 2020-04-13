using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        private List<TokenType>[,] dict = new List<TokenType>[100,100];

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
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    this[i,j] = new List<TokenType>();
                    this[i, j].Add(TokenType.ERROR);
                }
            }
            //Below is old code that works
            /*this[TokenType.STMNT, TokenType.VAR].Clear();
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.VAR);
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.ASSIGN);
            this[TokenType.STMNT, TokenType.VAR].Add(TokenType.ASSIGNMENT);

            this[TokenType.ASSIGNMENT, TokenType.VAL].Clear();
            this[TokenType.ASSIGNMENT, TokenType.VAL].Add(TokenType.VAL);
            this[TokenType.ASSIGNMENT, TokenType.VAL].Add(TokenType.EXPR);
            
            this[TokenType.EXPR, TokenType.LINEBREAK].Clear();

            this[TokenType.EXPR, TokenType.LINEBREAK].Add(TokenType.LINEBREAK);*/
            // Above is old code that works
            //PROG
            /*this[TokenType.PROG,TokenType.STMNTS].Clear();
            this[TokenType.PROG,TokenType.STMNTS].Add(TokenType.STMNTS);
            this[TokenType.PROG,TokenType.STMNTS].Add(TokenType.NEWLINE);*/
            //STMNTS
            /*this[TokenType.STMNTS,TokenType.STMNT].Clear();
            this[TokenType.STMNTS,TokenType.STMNT].Add(TokenType.STMNT);
            this[TokenType.STMNTS,TokenType.STMNT].Add(TokenType.STMNTS);
            this[TokenType.STMNTS,TokenType.STMNT].Add(TokenType.NEWLINE);*/
            //I have lost control over the NEWLINES... Valve please fix
            //STMNT - done 
            this[TokenType.STMNT,TokenType.VAR].Clear();
            this[TokenType.STMNT,TokenType.VAR].Add(TokenType.VAR);
            this[TokenType.STMNT,TokenType.VAR].Add(TokenType.ASSIGN);
            this[TokenType.STMNT,TokenType.VAR].Add(TokenType.ASSIGNMENT);
            //STMNT - done
            this[TokenType.STMNT,TokenType.IF].Clear();
            this[TokenType.STMNT,TokenType.IF].Add(TokenType.IFSTMNT);
            //STMNT - done
            this[TokenType.STMNT,TokenType.FUNC].Clear();
            this[TokenType.STMNT,TokenType.FUNC].Add(TokenType.FUNCCALL);
            //STMNT - done
            this[TokenType.STMNT,TokenType.BEGIN].Clear();
            this[TokenType.STMNT,TokenType.BEGIN].Add(TokenType.BEGIN);
            this[TokenType.STMNT,TokenType.BEGIN].Add(TokenType.BEGINABLE);
            //TYPE - done
            this[TokenType.TYPE,TokenType.STRING].Clear();
            this[TokenType.TYPE,TokenType.STRING].Add(TokenType.STRING);
            //TYPE - done
            this[TokenType.TYPE,TokenType.NUMERIC_INT].Clear();
            this[TokenType.TYPE,TokenType.NUMERIC_INT].Add(TokenType.NUMERIC);
            //TYPE - done
            this[TokenType.TYPE,TokenType.NUMERIC_FLOAT].Clear();
            this[TokenType.TYPE,TokenType.NUMERIC_FLOAT].Add(TokenType.NUMERIC);
            //TYPE - done
            this[TokenType.TYPE,TokenType.NUMERIC].Clear();
            this[TokenType.TYPE,TokenType.NUMERIC].Add(TokenType.NUMERIC);
            //TYPE - done
            this[TokenType.TYPE,TokenType.BOOL].Clear();
            this[TokenType.TYPE,TokenType.BOOL].Add(TokenType.BOOL);
            //TYPE - done
            this[TokenType.TYPE,TokenType.ARRAYLEFT].Clear();
            this[TokenType.TYPE,TokenType.ARRAYLEFT].Add(TokenType.ARR);
            //TYPE - done
            this[TokenType.TYPE,TokenType.DPIN].Clear();
            this[TokenType.TYPE,TokenType.DPIN].Add(TokenType.PIN);
            //TYPE - done
            this[TokenType.TYPE,TokenType.APIN].Clear();
            this[TokenType.TYPE,TokenType.APIN].Add(TokenType.PIN);
            //FUNCSTMNTS
            /*this[TokenType.FUNCSTMNTS,TokenType.FUNCSTMNT].Clear();
            this[TokenType.FUNCSTMNTS,TokenType.FUNCSTMNT].Add(TokenType.FUNCSTMNT);
            this[TokenType.FUNCSTMNTS,TokenType.FUNCSTMNT].Add(TokenType.FUNCSTMNTS);
            this[TokenType.FUNCSTMNTS,TokenType.FUNCSTMNT].Add(TokenType.NEWLINE);*/
            //FUNCSTMNT - done
            // this[TokenType.FUNCSTMNT,TokenType.VAR].Clear();
            // this[TokenType.FUNCSTMNT,TokenType.VAR].Add(TokenType.VAR);
            // this[TokenType.FUNCSTMNT,TokenType.VAR].Add(TokenType.ASSIGN);
            // this[TokenType.FUNCSTMNT,TokenType.VAR].Add(TokenType.VAL);
            // this[TokenType.FUNCSTMNT,TokenType.VAR].Add(TokenType.EXPR);
            // //FUNCSTMNT - done
            // this[TokenType.FUNCSTMNT,TokenType.IF].Clear();
            // this[TokenType.FUNCSTMNT,TokenType.IF].Add(TokenType.IFSTMNT);
            // //FUNCSTMNT - done
            // this[TokenType.FUNCSTMNT,TokenType.BEGIN].Clear();
            // this[TokenType.FUNCSTMNT,TokenType.BEGIN].Add(TokenType.BEGINSTMNT);

            //Please find a new home for this NEWLINE
            /*this[TokenType.NEWLINE,TokenType.NEWLINE].Clear();
            this[TokenType.NEWLINE,TokenType.NEWLINE].Add(TokenType.NEWLINE);*/
            
            //ASSIGNMENT - done
            this[TokenType.ASSIGNMENT,TokenType.VAL].Clear();
            this[TokenType.ASSIGNMENT,TokenType.VAL].Add(TokenType.VAL);
            this[TokenType.ASSIGNMENT,TokenType.VAL].Add(TokenType.EXPR);
            //ASSIGNMENT - done
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Clear();
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.ARRAYLEFT);
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.VAL);
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.ARRAYRIGHT);
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.ARR);
            this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.NEWLINE);
            //this[TokenType.ASSIGNMENT,TokenType.ARRAYLEFT].Add(TokenType.ARR);

            //EXPR
            this[TokenType.EXPR,TokenType.OP_PLUS].Clear();
            this[TokenType.EXPR,TokenType.OP_PLUS].Add(TokenType.MATH_OP);
            this[TokenType.EXPR,TokenType.OP_PLUS].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.OP_PLUS].Add(TokenType.EXPR);
            //EXPR
            this[TokenType.EXPR,TokenType.OP_MINUS].Clear();
            this[TokenType.EXPR,TokenType.OP_MINUS].Add(TokenType.MATH_OP);
            this[TokenType.EXPR,TokenType.OP_MINUS].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.OP_MINUS].Add(TokenType.EXPR);
            //EXPR
            this[TokenType.EXPR,TokenType.OP_TIMES].Clear();
            this[TokenType.EXPR,TokenType.OP_TIMES].Add(TokenType.MATH_OP);
            this[TokenType.EXPR,TokenType.OP_TIMES].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.OP_TIMES].Add(TokenType.EXPR);
            //EXPR
            this[TokenType.EXPR,TokenType.OP_DIVIDE].Clear();
            this[TokenType.EXPR,TokenType.OP_DIVIDE].Add(TokenType.MATH_OP);
            this[TokenType.EXPR,TokenType.OP_DIVIDE].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.OP_DIVIDE].Add(TokenType.EXPR);
            //EXPR
            this[TokenType.EXPR,TokenType.OP_GREATER].Clear();
            this[TokenType.EXPR,TokenType.OP_GREATER].Add(TokenType.BOOL_OP);
            this[TokenType.EXPR,TokenType.OP_GREATER].Add(TokenType.VAL);
            //this[TokenType.EXPR,TokenType.OP_GREATER].Add(TokenType.EXPR);
            //EXPR
            this[TokenType.EXPR,TokenType.OP_LESS].Clear();
            this[TokenType.EXPR,TokenType.OP_LESS].Add(TokenType.BOOL_OP);
            this[TokenType.EXPR,TokenType.OP_LESS].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.OP_LESS].Add(TokenType.EXPR);
            
            //EXPR
            /*this[TokenType.EXPR,TokenType.BOOL_OP].Add(TokenType.BOOL_OP);
            this[TokenType.EXPR,TokenType.BOOL_OP].Add(TokenType.VAL);
            this[TokenType.EXPR,TokenType.BOOL_OP].Add(TokenType.EXPR);*/
            //EXPR
            this[TokenType.EXPR,TokenType.NEWLINE].Clear();
            this[TokenType.EXPR,TokenType.NEWLINE].Add(TokenType.NEWLINE);
            
            //EXPR - accept all
            /*this[TokenType.EXPR,TokenType.DO].Clear();
            this[TokenType.EXPR,TokenType.DO].Add(TokenType.DO);*/
            
            this[TokenType.EXPR,TokenType.END].Clear();
            this[TokenType.EXPR,TokenType.VAR].Clear();
            
            
            //MATH_OP - done
            this[TokenType.MATH_OP,TokenType.OP_PLUS].Clear();
            this[TokenType.MATH_OP,TokenType.OP_PLUS].Add(TokenType.OP_PLUS);
            //MATH_OP - done
            this[TokenType.MATH_OP,TokenType.OP_MINUS].Clear();
            this[TokenType.MATH_OP,TokenType.OP_MINUS].Add(TokenType.OP_MINUS);
            //MATH_OP - done
            this[TokenType.MATH_OP,TokenType.OP_TIMES].Clear();
            this[TokenType.MATH_OP,TokenType.OP_TIMES].Add(TokenType.OP_TIMES);
            //MATH_OP - done
            this[TokenType.MATH_OP,TokenType.OP_DIVIDE].Clear();
            this[TokenType.MATH_OP,TokenType.OP_DIVIDE].Add(TokenType.OP_DIVIDE);
            //MATH_OP - done
            this[TokenType.MATH_OP,TokenType.OP_MODULO].Clear();
            this[TokenType.MATH_OP,TokenType.OP_MODULO].Add(TokenType.OP_MODULO);

            //BOOL_OP - done
            this[TokenType.BOOL_OP,TokenType.EQUALS].Clear();
            this[TokenType.BOOL_OP,TokenType.EQUALS].Add(TokenType.EQUALS);
            //BOOL_OP - done
            this[TokenType.BOOL_OP,TokenType.OP_AND].Clear();
            this[TokenType.BOOL_OP,TokenType.OP_AND].Add(TokenType.OP_AND);
            //BOOL_OP - done
            this[TokenType.BOOL_OP,TokenType.OP_OR].Clear();
            this[TokenType.BOOL_OP,TokenType.OP_OR].Add(TokenType.OP_OR);
            //BOOL_OP - done
            this[TokenType.BOOL_OP,TokenType.OP_GREATER].Clear();
            this[TokenType.BOOL_OP,TokenType.OP_GREATER].Add(TokenType.OP_GREATER);
            //this[TokenType.BOOL_OP,TokenType.OP_GREATER].Add(TokenType.OREQUAL);
            //BOOL_OP - done
            this[TokenType.BOOL_OP,TokenType.OP_LESS].Clear();
            this[TokenType.BOOL_OP,TokenType.OP_LESS].Add(TokenType.OP_LESS);
            
            //OREQUAL - done
            //this[TokenType.OREQUAL,TokenType.VAL].Clear();
            //this[TokenType.OREQUAL,TokenType.VAL].Add(TokenType.VAL);
            //OREQUAL - done
            
            //VAL - done
            this[TokenType.VAL,TokenType.VAR].Clear();
            this[TokenType.VAL,TokenType.VAR].Add(TokenType.VAR);
            //VAL - done
            this[TokenType.VAL,TokenType.NUMERIC_INT].Clear();
            this[TokenType.VAL,TokenType.NUMERIC_INT].Add(TokenType.NUMERIC);
            //VAL - done
            this[TokenType.VAL,TokenType.NUMERIC_FLOAT].Clear();
            this[TokenType.VAL,TokenType.NUMERIC_FLOAT].Add(TokenType.NUMERIC);
            //VAL - done
            this[TokenType.VAL,TokenType.STRING].Clear();
            this[TokenType.VAL,TokenType.STRING].Add(TokenType.STRING);
            //VAL - done
            this[TokenType.VAL,TokenType.DPIN].Clear();
            this[TokenType.VAL,TokenType.DPIN].Add(TokenType.PIN);
            //VAL - done
            this[TokenType.VAL,TokenType.APIN].Clear();
            this[TokenType.VAL,TokenType.APIN].Add(TokenType.PIN);
            
            // Possible error in scannertoken
            this[TokenType.ARR,TokenType.ARRAYLEFT].Clear();
            this[TokenType.ARR,TokenType.ARRAYLEFT].Add(TokenType.ARRAYLEFT);
            this[TokenType.ARR,TokenType.ARRAYLEFT].Add(TokenType.VAL);
            this[TokenType.ARR,TokenType.ARRAYLEFT].Add(TokenType.ARRAYRIGHT);
            this[TokenType.ARR,TokenType.ARRAYLEFT].Add(TokenType.ARR);
            this[TokenType.ARR,TokenType.ARRAYLEFT].Add(TokenType.NEWLINE);
            //ARR
            this[TokenType.ARR,TokenType.NEWLINE].Clear();

            //PIN - done
            this[TokenType.PIN,TokenType.DPIN].Clear();
            this[TokenType.PIN,TokenType.DPIN].Add(TokenType.DPIN);
            //PIN - done
            this[TokenType.PIN,TokenType.APIN].Clear();
            this[TokenType.PIN,TokenType.APIN].Add(TokenType.APIN);

            //IFSTMNT - done
            this[TokenType.IFSTMNT,TokenType.IF].Clear();
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.IF);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.VAL);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.BOOL_OP);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.VAL);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.EXPR);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.END);
            this[TokenType.IFSTMNT,TokenType.IF].Add(TokenType.IF);

            //FUNCCALL - done
            this[TokenType.FUNCCALL,TokenType.FUNC].Clear();
            this[TokenType.FUNCCALL,TokenType.FUNC].Add(TokenType.FUNC);
            this[TokenType.FUNCCALL,TokenType.FUNC].Add(TokenType.VAR);
            this[TokenType.FUNCCALL,TokenType.FUNC].Add(TokenType.OPTNL_ARGS);
            this[TokenType.FUNCCALL,TokenType.FUNC].Add(TokenType.END);
            this[TokenType.FUNCCALL,TokenType.FUNC].Add(TokenType.VAR);

            //BEGINSTMNT - done
            this[TokenType.BEGINSTMNT,TokenType.BEGIN].Clear();
            this[TokenType.BEGINSTMNT,TokenType.BEGIN].Add(TokenType.BEGIN);
            this[TokenType.BEGINSTMNT,TokenType.BEGIN].Add(TokenType.BEGINABLE);

            //BEGINABLE - done
            this[TokenType.BEGINABLE,TokenType.WHILE].Clear();
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.WHILE);
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.VAL);
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.EXPR);
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.DO);
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.END);
            this[TokenType.BEGINABLE,TokenType.WHILE].Add(TokenType.FOR);
            //BEGINABLE - done
            this[TokenType.BEGINABLE,TokenType.FOR].Clear();
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.FOR);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.VAR);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.IN);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.RANGE);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.DO);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.END);
            this[TokenType.BEGINABLE,TokenType.FOR].Add(TokenType.FOR);
            
            //LOOPW - done
            this[TokenType.LOOPW,TokenType.WHILE].Clear();
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.WHILE);
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.VAL);
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.EXPR);
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.DO);
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.END);
            this[TokenType.LOOPW,TokenType.WHILE].Add(TokenType.WHILE);

            //LOOPF - done
            this[TokenType.LOOPF,TokenType.FOR].Clear();
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.FOR);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.VAR);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.IN);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.RANGE);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.DO);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.END);
            this[TokenType.LOOPF,TokenType.FOR].Add(TokenType.FOR);

            //OPTNL_ARGS - done
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Clear();
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.WITH);
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.ARG);
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.ARGLIST);
            //OPTNL_ARGS - done
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Clear();
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.WITH);
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.ARG);
            this[TokenType.OPTNL_ARGS,TokenType.WITH].Add(TokenType.ARGLIST);
            //OPTNL_ARGS - done
            this[TokenType.OPTNL_ARGS,TokenType.NEWLINE].Clear();

            //ATGLIST
            this[TokenType.ARGLIST,TokenType.ARG].Clear();
            this[TokenType.ARGLIST,TokenType.ARG].Add(TokenType.ARG);
            this[TokenType.ARGLIST,TokenType.ARG].Add(TokenType.ARGLIST);
            this[TokenType.ARGLIST,TokenType.ARG].Add(TokenType.NEWLINE);
            
            //ATGLIST
            this[TokenType.ARGLIST,TokenType.NEWLINE].Clear();
            //ATGLIST
            this[TokenType.ARGLIST,TokenType.VAL].Clear();
            //ATGLIST
            this[TokenType.ARGLIST,TokenType.NUMERIC].Clear();
            
            //ARG
            this[TokenType.ARG,TokenType.NUMERIC].Clear();
            this[TokenType.ARG,TokenType.NUMERIC].Add(TokenType.TYPE);
            this[TokenType.ARG,TokenType.NUMERIC].Add(TokenType.VAL);
            //ARG
            this[TokenType.ARG,TokenType.STRING].Clear();
            this[TokenType.ARG,TokenType.STRING].Add(TokenType.TYPE);
            //this[TokenType.ARG,TokenType.STRING].Add(TokenType.STRING);
            //ARG
            this[TokenType.ARG,TokenType.BOOL].Clear();
            this[TokenType.ARG,TokenType.BOOL].Add(TokenType.TYPE);
        }
    }
}