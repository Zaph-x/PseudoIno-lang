using System;
using System.Collections.Generic;
using Lexer.Objects;
using static Lexer.Objects.TokenType;

namespace Parser.Objects
{
    public class ParseTable
    {
        public Dictionary<TokenType, Dictionary<TokenType, ParseAction>> Table { get; private set; }

        public ParseAction this[ScannerToken key1, ScannerToken key2]
        {
            get => Table[key1.Type][key2.Type];
            set => Table[key1.Type][key2.Type] = value;
        }

        public ParseAction this[TokenType key1, TokenType key2]
        {
            get => Table[key1][key2];
            set => Table[key1][key2] = value;
        }

        public ParseTable()
        {
            Init();
            InitTable();
        }

        private void Init()
        {

            Table = new Dictionary<TokenType, Dictionary<TokenType, ParseAction>>();

            foreach (TokenType type in Enum.GetValues(typeof(TokenType)))
            {
                Table.Add(type, new Dictionary<TokenType, ParseAction>());
                foreach (TokenType innerType in Enum.GetValues(typeof(TokenType)))
                {
                    Table[type].Add(innerType, ParseAction.Error());
                }
            }
        }

        public void InitTable()
        {
            this[PROG, EOF] = new ParseAction(STMNTS);
            this[PROG, WAIT] = new ParseAction(STMNTS);
            this[PROG, VAR] = new ParseAction(STMNTS);
            this[PROG, BEGIN] = new ParseAction(STMNTS);
            this[PROG, FUNC] = new ParseAction(STMNTS);
            this[PROG, CALL] = new ParseAction(STMNTS);
            this[PROG, IF] = new ParseAction(STMNTS);
            this[PROG, APIN] = new ParseAction(STMNTS);
            this[PROG, DPIN] = new ParseAction(STMNTS);
            this[PROG, COMMENT] = new ParseAction(NT_COMMENT);
            this[PROG, MULT_COMNT] = new ParseAction(NT_COMMENT);

            this[NT_COMMENT, COMMENT] = new ParseAction(COMMENT);
            this[NT_COMMENT, MULT_COMNT] = new ParseAction(MULT_COMNT);

            this[STMNTS, EOF] = new ParseAction();
            this[STMNTS, WAIT] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, END] = new ParseAction();
            this[STMNTS, VAR] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, BEGIN] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, RETURN] = new ParseAction();
            this[STMNTS, FUNC] = new ParseAction(FUNCDECL, STMNTS);
            this[STMNTS, CALL] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, IF] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, ELSE] = new ParseAction();
            this[STMNTS, APIN] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, DPIN] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, COMMENT] = new ParseAction(NT_COMMENT, STMNTS);
            this[STMNTS, MULT_COMNT] = new ParseAction(NT_COMMENT, STMNTS);

            this[STMNT, WAIT] = new ParseAction(WAITSTMNT);
            this[STMNT, VAR] = new ParseAction(ASSIGNSTMNT);
            this[STMNT, BEGIN] = new ParseAction(BEGINSTMNT);
            this[STMNT, CALL] = new ParseAction(FUNCCALL);
            this[STMNT, IF] = new ParseAction(IFSTMNT);
            this[STMNT, APIN] = new ParseAction(ASSIGNSTMNT);
            this[STMNT, DPIN] = new ParseAction(ASSIGNSTMNT);

            this[ASSIGNSTMNT, VAR] = new ParseAction(VAR, ASSIGN, ASSIGNMENT);
            this[ASSIGNSTMNT, APIN] = new ParseAction(APIN, ASSIGN, ASSIGNMENT);
            this[ASSIGNSTMNT, DPIN] = new ParseAction(DPIN, ASSIGN, ASSIGNMENT);

            this[ASSIGNMENT, NUMERIC] = new ParseAction(EXPR);
            this[ASSIGNMENT, VAR] = new ParseAction(EXPR);
            this[ASSIGNMENT, CALL] = new ParseAction(FUNCCALL);
            this[ASSIGNMENT, APIN] = new ParseAction(EXPR);
            this[ASSIGNMENT, DPIN] = new ParseAction(EXPR);
            this[ASSIGNMENT, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);
            this[ASSIGNMENT, STRING] = new ParseAction(EXPR);
            this[ASSIGNMENT, OP_MINUS] = new ParseAction(EXPR);
            this[ASSIGNMENT, OP_LPAREN] = new ParseAction(EXPR);

            this[EXPR, NUMERIC] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, VAR] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, APIN] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, DPIN] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, STRING] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, OP_MINUS] = new ParseAction(TERM, FOLLOWTERM);
            this[EXPR, OP_LPAREN] = new ParseAction(TERM, FOLLOWTERM);

            this[FOLLOWTERM, EOF] = new ParseAction();
            this[FOLLOWTERM, WAIT] = new ParseAction();
            this[FOLLOWTERM, END] = new ParseAction();
            this[FOLLOWTERM, DO] = new ParseAction();
            this[FOLLOWTERM, VAR] = new ParseAction();
            this[FOLLOWTERM, BEGIN] = new ParseAction();
            this[FOLLOWTERM, RETURN] = new ParseAction();
            this[FOLLOWTERM, FUNC] = new ParseAction();
            this[FOLLOWTERM, CALL] = new ParseAction();
            this[FOLLOWTERM, IF] = new ParseAction();
            this[FOLLOWTERM, ELSE] = new ParseAction();
            this[FOLLOWTERM, APIN] = new ParseAction();
            this[FOLLOWTERM, DPIN] = new ParseAction();
            this[FOLLOWTERM, DPIN] = new ParseAction();
            this[FOLLOWTERM, OP_MINUS] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_EQUAL] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_OR] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_LESS] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_GREATER] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_AND] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_PLUS] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_NOT] = new ParseAction(TERMOP, EXPR);
            this[FOLLOWTERM, OP_RPAREN] = new ParseAction();
            this[FOLLOWTERM, MULT_COMNT] = new ParseAction();
            this[FOLLOWTERM, COMMENT] = new ParseAction();
           
            this[TERM, NUMERIC] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, VAR] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, APIN] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, DPIN] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, STRING] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, BOOL] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, OP_MINUS] = new ParseAction(FACTOR, FOLLOWFACTOR);
            this[TERM, OP_LPAREN] = new ParseAction(FACTOR, FOLLOWFACTOR);

            this[FOLLOWFACTOR, EOF] = new ParseAction();
            this[FOLLOWFACTOR, WAIT] = new ParseAction();
            this[FOLLOWFACTOR, END] = new ParseAction();
            this[FOLLOWFACTOR, DO] = new ParseAction();
            this[FOLLOWFACTOR, VAR] = new ParseAction();
            this[FOLLOWFACTOR, BEGIN] = new ParseAction();
            this[FOLLOWFACTOR, RETURN] = new ParseAction();
            this[FOLLOWFACTOR, FUNC] = new ParseAction();
            this[FOLLOWFACTOR, CALL] = new ParseAction();
            this[FOLLOWFACTOR, IF] = new ParseAction();
            this[FOLLOWFACTOR, ELSE] = new ParseAction();
            this[FOLLOWFACTOR, APIN] = new ParseAction();
            this[FOLLOWFACTOR, DPIN] = new ParseAction();
            this[FOLLOWFACTOR, OP_EQUAL] = new ParseAction();
            this[FOLLOWFACTOR, OP_OR] = new ParseAction();
            this[FOLLOWFACTOR, OP_NOT] = new ParseAction();
            this[FOLLOWFACTOR, OP_GREATER] = new ParseAction();
            this[FOLLOWFACTOR, OP_LESS] = new ParseAction();
            this[FOLLOWFACTOR, OP_AND] = new ParseAction();
            this[FOLLOWFACTOR, OP_DIVIDE] = new ParseAction(FACTOROP, TERM);
            this[FOLLOWFACTOR, OP_MODULO] = new ParseAction(FACTOROP, TERM);
            this[FOLLOWFACTOR, OP_TIMES] = new ParseAction(FACTOROP, TERM);
            this[FOLLOWFACTOR, OP_PLUS] = new ParseAction();
            this[FOLLOWFACTOR, OP_RPAREN] = new ParseAction();
            this[FOLLOWFACTOR, MULT_COMNT] = new ParseAction();
            this[FOLLOWFACTOR, COMMENT] = new ParseAction();

            this[FACTOR, NUMERIC] = new ParseAction(VAL);
            this[FACTOR, VAR] = new ParseAction(VAL);
            this[FACTOR, APIN] = new ParseAction(VAL);
            this[FACTOR, DPIN] = new ParseAction(VAL);
            this[FACTOR, STRING] = new ParseAction(VAL);
            this[FACTOR, OP_MINUS] = new ParseAction(VAL);
            this[FACTOR, OP_LPAREN] = new ParseAction(OP_LPAREN, EXPR, OP_RPAREN);

            this[ARRAYACCESSING, EOF] = new ParseAction();
            this[ARRAYACCESSING, SEPARATOR] = new ParseAction();
            this[ARRAYACCESSING, WAIT] = new ParseAction();
            this[ARRAYACCESSING, VAR] = new ParseAction();
            this[ARRAYACCESSING, END] = new ParseAction();
            this[ARRAYACCESSING, RETURN] = new ParseAction();
            this[ARRAYACCESSING, DO] = new ParseAction();
            this[ARRAYACCESSING, BEGIN] = new ParseAction();
            this[ARRAYACCESSING, FUNC] = new ParseAction();
            this[ARRAYACCESSING, CALL] = new ParseAction();
            this[ARRAYACCESSING, IF] = new ParseAction();
            this[ARRAYACCESSING, ELSE] = new ParseAction();
            this[ARRAYACCESSING, OP_MINUS] = new ParseAction();
            this[ARRAYACCESSING, OP_OR] = new ParseAction();
            this[ARRAYACCESSING, OP_LESS] = new ParseAction();
            this[ARRAYACCESSING, OP_GREATER] = new ParseAction();
            this[ARRAYACCESSING, OP_AND] = new ParseAction();
            this[ARRAYACCESSING, OP_EQUAL] = new ParseAction();
            this[ARRAYACCESSING, OP_MODULO] = new ParseAction();
            this[ARRAYACCESSING, OP_DIVIDE] = new ParseAction();
            this[ARRAYACCESSING, OP_TIMES] = new ParseAction();
            this[ARRAYACCESSING, OP_PLUS] = new ParseAction();
            this[ARRAYACCESSING, ARRAYINDEX] = new ParseAction(ARRAYINDEX, INDEXER, ARRAYACCESSING);
            this[ARRAYACCESSING, ASSIGN] = new ParseAction();
            this[ARRAYACCESSING, OP_RPAREN] = new ParseAction();
            this[ARRAYACCESSING, COMMENT] = new ParseAction();
            this[ARRAYACCESSING, MULT_COMNT] = new ParseAction();

            this[INDEXER, VAR] = new ParseAction(VAR);
            this[INDEXER, NUMERIC] = new ParseAction(NUMERIC);

            this[TERMOP, OP_MINUS] = new ParseAction(OP_MINUS);
            this[TERMOP, OP_AND] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_OR] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_GREATER] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_LESS] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_EQUAL] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_NOT] = new ParseAction(BOOL_OP);
            this[TERMOP, OP_PLUS] = new ParseAction(OP_PLUS);

            this[FACTOROP, OP_MODULO] = new ParseAction(OP_MODULO);
            this[FACTOROP, OP_DIVIDE] = new ParseAction(OP_DIVIDE);
            this[FACTOROP, OP_TIMES] = new ParseAction(OP_TIMES);

            this[MATH_OP, OP_MINUS] = new ParseAction(OP_MINUS);
            this[MATH_OP, OP_PLUS] = new ParseAction(OP_PLUS);
            this[MATH_OP, OP_DIVIDE] = new ParseAction(OP_DIVIDE);
            this[MATH_OP, OP_TIMES] = new ParseAction(OP_TIMES);
            this[MATH_OP, OP_MODULO] = new ParseAction(OP_MODULO);

            this[BOOL_OP, OP_AND] = new ParseAction(OP_AND);
            this[BOOL_OP, OP_OR] = new ParseAction(OP_OR);
            this[BOOL_OP, OP_LESS] = new ParseAction(OP_LESS, OP_OREQUAL);
            this[BOOL_OP, OP_GREATER] = new ParseAction(OP_GREATER, OP_OREQUAL);
            this[BOOL_OP, OP_EQUAL] = new ParseAction(OP_EQUAL);
            this[BOOL_OP, OP_NOT] = new ParseAction(OP_NOT, OP_EQUAL);

            this[OP_OREQUAL, NUMERIC] = new ParseAction();
            this[OP_OREQUAL, VAR] = new ParseAction();
            this[OP_OREQUAL, APIN] = new ParseAction();
            this[OP_OREQUAL, DPIN] = new ParseAction();
            this[OP_OREQUAL, STRING] = new ParseAction();
            this[OP_OREQUAL, OP_MINUS] = new ParseAction();
            this[OP_OREQUAL, OP_OR] = new ParseAction(OP_OR, OP_EQUAL);
            this[OP_OREQUAL, OP_LPAREN] = new ParseAction();

            this[VAL, NUMERIC] = new ParseAction(NUMERIC);
            this[VAL, VAR] = new ParseAction(VAR, ARRAYACCESSING);
            this[VAL, APIN] = new ParseAction(PIN);
            this[VAL, DPIN] = new ParseAction(PIN);
            this[VAL, BOOL] = new ParseAction(BOOL);
            this[VAL, STRING] = new ParseAction(STRING);
            this[VAL, OP_MINUS] = new ParseAction(OP_MINUS, NUMERIC);

            this[ARR, EOF] = new ParseAction();
            this[ARR, WAIT] = new ParseAction();
            this[ARR, END] = new ParseAction();
            this[ARR, VAR] = new ParseAction();
            this[ARR, BEGIN] = new ParseAction();
            this[ARR, RETURN] = new ParseAction();
            this[ARR, FUNC] = new ParseAction();
            this[ARR, CALL] = new ParseAction();
            this[ARR, IF] = new ParseAction();
            this[ARR, ELSE] = new ParseAction();
            this[ARR, APIN] = new ParseAction();
            this[ARR, DPIN] = new ParseAction();
            this[ARR, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);
            this[ARR, COMMENT] = new ParseAction();
            this[ARR, MULT_COMNT] = new ParseAction();

            this[PIN, APIN] = new ParseAction(APIN);
            this[PIN, DPIN] = new ParseAction(DPIN);

            this[IFSTMNT, IF] = new ParseAction(IF, EXPR, DO, STMNTS, ELSESTMNT, ENDIF);

            this[ENDIF, END] = new ParseAction(END, IF);

            this[ELSESTMNT, END] = new ParseAction();
            this[ELSESTMNT, ELSE] = new ParseAction(ELSE, ELSEIFSTMNT, STMNTS);

            this[ELSEIFSTMNT, IF] = new ParseAction(IF, EXPR, DO, STMNTS, ELSESTMNT);
            this[ELSEIFSTMNT, VAR] = new ParseAction();
            this[ELSEIFSTMNT, NUMERIC] = new ParseAction();
            this[ELSEIFSTMNT, APIN] = new ParseAction();
            this[ELSEIFSTMNT, DPIN] = new ParseAction();
            this[ELSEIFSTMNT, OP_MINUS] = new ParseAction();
            this[ELSEIFSTMNT, BEGIN] = new ParseAction();
            this[ELSEIFSTMNT, RETURN] = new ParseAction();
            this[ELSEIFSTMNT, CALL] = new ParseAction();
            this[ELSEIFSTMNT, WAIT] = new ParseAction();

            this[FUNCCALL, CALL] = new ParseAction(CALL, VAR, CALLPARAMS);

            this[FUNCDECL, FUNC] = new ParseAction(FUNC, VAR, DECLPARAMS, STMNTS, RETSTMNT, END, VAR);
            
            this[ENDFUNC, END] = new ParseAction(END, VAR);

            this[DECLPARAMS, WITH] = new ParseAction(WITH, VAR, DECLPARAM);
            this[DECLPARAMS, WAIT] = new ParseAction();
            this[DECLPARAMS, END] = new ParseAction();
            this[DECLPARAMS, VAR] = new ParseAction();
            this[DECLPARAMS, BEGIN] = new ParseAction();
            this[DECLPARAMS, RETURN] = new ParseAction();
            this[DECLPARAMS, FUNC] = new ParseAction();
            this[DECLPARAMS, CALL] = new ParseAction();
            this[DECLPARAMS, IF] = new ParseAction();
            this[DECLPARAMS, ELSE] = new ParseAction();
            this[DECLPARAMS, APIN] = new ParseAction();
            this[DECLPARAMS, DPIN] = new ParseAction();
            this[DECLPARAMS, COMMENT] = new ParseAction();
            this[DECLPARAMS, MULT_COMNT] = new ParseAction();

            this[DECLPARAM, SEPARATOR] = new ParseAction(SEPARATOR, VAR, DECLPARAM);
            this[DECLPARAM, WAIT] = new ParseAction();
            this[DECLPARAM, END] = new ParseAction();
            this[DECLPARAM, VAR] = new ParseAction();
            this[DECLPARAM, BEGIN] = new ParseAction();
            this[DECLPARAM, RETURN] = new ParseAction();
            this[DECLPARAM, FUNC] = new ParseAction();
            this[DECLPARAM, CALL] = new ParseAction();
            this[DECLPARAM, IF] = new ParseAction();
            this[DECLPARAM, APIN] = new ParseAction();
            this[DECLPARAM, DPIN] = new ParseAction();
            this[DECLPARAM, COMMENT] = new ParseAction();
            this[DECLPARAM, MULT_COMNT] = new ParseAction();


            this[RETSTMNT, RETURN] = new ParseAction(RETURN, VAL);
            this[RETSTMNT, END] = new ParseAction();

            this[BEGINSTMNT, BEGIN] = new ParseAction(BEGIN, BEGINABLE);

            this[BEGINABLE, FOR] = new ParseAction(FOR, VAR, IN, RANGE, DO, STMNTS, END, FOR);
            
            this[ENDFOR, END] = new ParseAction(END, FOR);
            
            this[BEGINABLE, WHILE] = new ParseAction(WHILE, EXPR, DO, STMNTS, END, WHILE);

            this[ENDWHILE, END] = new ParseAction(END, WHILE);

            this[RANGE, NUMERIC] = new ParseAction(NUMERIC, OP_RANGE, NUMERIC);

            this[WAITSTMNT, WAIT] = new ParseAction(WAIT, NUMERIC, TIME_MOD);

            this[TIME_MOD, TIME_HR] = new ParseAction(TIME_HR);
            this[TIME_MOD, TIME_MIN] = new ParseAction(TIME_MIN);
            this[TIME_MOD, TIME_SEC] = new ParseAction(TIME_SEC);
            this[TIME_MOD, TIME_MS] = new ParseAction(TIME_MS);

            this[CALLPARAMS, EOF] = new ParseAction();
            this[CALLPARAMS, WITH] = new ParseAction(WITH, VAL, CALLPARAM);
            this[CALLPARAMS, WAIT] = new ParseAction();
            this[CALLPARAMS, END] = new ParseAction();
            this[CALLPARAMS, VAR] = new ParseAction();
            this[CALLPARAMS, BEGIN] = new ParseAction();
            this[CALLPARAMS, RETURN] = new ParseAction();
            this[CALLPARAMS, FUNC] = new ParseAction();
            this[CALLPARAMS, CALL] = new ParseAction();
            this[CALLPARAMS, IF] = new ParseAction();
            this[CALLPARAMS, ELSE] = new ParseAction();
            this[CALLPARAMS, COMMENT] = new ParseAction();
            this[CALLPARAMS, MULT_COMNT] = new ParseAction();

            this[CALLPARAM, EOF] = new ParseAction();
            this[CALLPARAM, VAR] = new ParseAction();
            this[CALLPARAM, WAIT] = new ParseAction();
            this[CALLPARAM, END] = new ParseAction();
            this[CALLPARAM, BEGIN] = new ParseAction();
            this[CALLPARAM, RETURN] = new ParseAction();
            this[CALLPARAM, FUNC] = new ParseAction();
            this[CALLPARAM, CALL] = new ParseAction();
            this[CALLPARAM, IF] = new ParseAction();
            this[CALLPARAM, ELSE] = new ParseAction();
            this[CALLPARAM, COMMENT] = new ParseAction();
            this[CALLPARAM, MULT_COMNT] = new ParseAction();
            this[CALLPARAM, SEPARATOR] = new ParseAction(SEPARATOR, VAL, CALLPARAM);


            // this[PROG, VAR] = new ParseAction(STMNTS);
            // this[PROG, IF] = new ParseAction(STMNTS);
            // this[PROG, CALL] = new ParseAction(STMNTS);
            // this[PROG, FUNC] = new ParseAction(STMNTS);
            // this[PROG, BEGIN] = new ParseAction(STMNTS);
            // this[PROG, WAIT] = new ParseAction(STMNTS);
            // this[PROG, APIN] = new ParseAction(STMNTS);
            // this[PROG, DPIN] = new ParseAction(STMNTS);
            // this[PROG, EOF] = new ParseAction(EOF);

            // this[TYPE, STRING] = new ParseAction(STRING);
            // this[TYPE, NUMERIC] = new ParseAction(NUMERIC);
            // this[TYPE, BOOL] = new ParseAction(BOOL);
            // this[TYPE, DPIN] = new ParseAction(DPIN);
            // this[TYPE, APIN] = new ParseAction(APIN);

            // this[STMNTS, VAR] = new ParseAction(STMNT);
            // this[STMNTS, IF] = new ParseAction(STMNT);
            // this[STMNTS, CALL] = new ParseAction(STMNT);
            // this[STMNTS, FUNC] = new ParseAction(FUNCDECL);
            // this[STMNTS, BEGIN] = new ParseAction(STMNT);
            // this[STMNTS, WAIT] = new ParseAction(STMNT);
            // this[STMNTS, APIN] = new ParseAction(STMNT);
            // this[STMNTS, DPIN] = new ParseAction(STMNT);
            // this[STMNTS, ELSE] = new ParseAction();

            // this[STMNT, IF] = new ParseAction(IFSTMNT);
            // this[STMNT, CALL] = new ParseAction(FUNCCALL);
            // this[STMNT, BEGIN] = new ParseAction(BEGINSTMNT);
            // this[STMNT, WAIT] = new ParseAction(WAITSTMNT);
            // this[STMNT, VAR] = new ParseAction(VAR, ASSIGN, ASSIGNMENT);
            // this[STMNT, APIN] = new ParseAction(APIN, ASSIGN, ASSIGNMENT);
            // this[STMNT, DPIN] = new ParseAction(DPIN, ASSIGN, ASSIGNMENT);
            // // this[STMNT, END] = new ParseAction(END, ENDABLE);

            // // this[ENDABLE, VAR] = new ParseAction(VAR);
            // // this[ENDABLE, FOR] = new ParseAction(FOR);
            // // this[ENDABLE, WHILE] = new ParseAction(WHILE);


            // this[ASSIGNMENT, VAR] = new ParseAction(VAR, EXPR);
            // this[ASSIGNMENT, NUMERIC] = new ParseAction(NUMERIC, EXPR);
            // this[ASSIGNMENT, OP_MINUS] = new ParseAction(OP_MINUS, NUMERIC, EXPR);
            // this[ASSIGNMENT, STRING] = new ParseAction(STRING, EXPR);
            // this[ASSIGNMENT, BOOL] = new ParseAction(BOOL, EXPR);
            // this[ASSIGNMENT, DPIN] = new ParseAction(DPIN, EXPR);
            // this[ASSIGNMENT, APIN] = new ParseAction(APIN, EXPR);
            // this[ASSIGNMENT, OP_LPAREN] = new ParseAction(OP_LPAREN, ASSIGNMENT);

            // this[EXPR, OP_PLUS] = new ParseAction(OP_PLUS, VAL, EXPR);
            // this[EXPR, OP_MINUS] = new ParseAction(OP_MINUS, VAL, EXPR);
            // this[EXPR, OP_TIMES] = new ParseAction(OP_TIMES, VAL, EXPR);
            // this[EXPR, OP_DIVIDE] = new ParseAction(OP_DIVIDE, VAL, EXPR);
            // this[EXPR, OP_MODULO] = new ParseAction(OP_MODULO, VAL, EXPR);
            // this[EXPR, OP_RPAREN] = new ParseAction(OP_RPAREN, EXPR);

            // this[EXPR, OP_AND] = new ParseAction(OP_AND, VAL, EXPR);
            // this[EXPR, OP_OR] = new ParseAction(OP_OR, VAL, EXPR);
            // this[EXPR, OP_EQUAL] = new ParseAction(OP_EQUAL, VAL, EXPR);
            // this[EXPR, OP_NOT] = new ParseAction(OP_NOT, VAL, EXPR);
            // this[EXPR, OP_LESS] = new ParseAction(OP_LESS, OP_OREQUAL, VAL, EXPR);
            // this[EXPR, OP_GREATER] = new ParseAction(OP_GREATER, OP_OREQUAL, VAL, EXPR);

            // this[EXPR, VAR] = new ParseAction();
            // this[EXPR, IF] = new ParseAction();
            // this[EXPR, DO] = new ParseAction();
            // this[EXPR, END] = new ParseAction();
            // this[EXPR, ELSE] = new ParseAction();
            // this[EXPR, CALL] = new ParseAction();
            // this[EXPR, FUNC] = new ParseAction();
            // this[EXPR, BEGIN] = new ParseAction();
            // this[EXPR, WAIT] = new ParseAction();

            // this[OP_OREQUAL, OP_OR] = new ParseAction(OP_OR, OP_EQUAL);
            // this[OP_OREQUAL, VAR] = new ParseAction();
            // this[OP_OREQUAL, NUMERIC] = new ParseAction();
            // this[OP_OREQUAL, BOOL] = new ParseAction();
            // this[OP_OREQUAL, STRING] = new ParseAction();
            // this[OP_OREQUAL, DPIN] = new ParseAction();
            // this[OP_OREQUAL, APIN] = new ParseAction();

            // this[VAL, VAR] = new ParseAction(VAR);
            // this[VAL, NUMERIC] = new ParseAction(NUMERIC);
            // this[VAL, OP_MINUS] = new ParseAction(OP_MINUS, NUMERIC);
            // this[VAL, STRING] = new ParseAction(STRING);
            // this[VAL, BOOL] = new ParseAction(BOOL);
            // this[VAL, DPIN] = new ParseAction(DPIN);
            // this[VAL, APIN] = new ParseAction(APIN);

            // this[ARR, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);
            // this[ARR, VAR] = new ParseAction();
            // this[ARR, IF] = new ParseAction();
            // this[ARR, END] = new ParseAction();
            // this[ARR, ELSE] = new ParseAction();
            // this[ARR, CALL] = new ParseAction();
            // this[ARR, FUNC] = new ParseAction();
            // this[ARR, BEGIN] = new ParseAction();
            // this[ARR, WAIT] = new ParseAction();

            // this[PIN, DPIN] = new ParseAction(DPIN);
            // this[PIN, APIN] = new ParseAction(APIN);

            // this[IFSTMNT, IF] = new ParseAction(IF, VAL, EXPR, DO, STMNTS, ELSESTMNT, END, IF);

            // this[ELSESTMNT, ELSE] = new ParseAction(ELSE, ELSEIFSTMNT, STMNTS);
            // this[ELSESTMNT, END] = new ParseAction();

            // this[ELSEIFSTMNT, IF] = new ParseAction(IF, VAL, EXPR, DO, STMNTS, ELSESTMNT);
            // this[ELSEIFSTMNT, CALL] = new ParseAction();
            // this[ELSEIFSTMNT, BEGIN] = new ParseAction();
            // this[ELSEIFSTMNT, WAIT] = new ParseAction();
            // this[ELSEIFSTMNT, VAR] = new ParseAction();

            // this[FUNCCALL, CALL] = new ParseAction(CALL, VAR, CALLPARAMS);

            // this[FUNCDECL, FUNC] = new ParseAction(FUNC, VAR, OPTNL_ARGS, STMNTS, END, VAR);

            // this[BEGINSTMNT, BEGIN] = new ParseAction(BEGIN, BEGINABLE);

            // this[BEGINABLE, WHILE] = new ParseAction(WHILE, VAL, EXPR, DO, STMNTS, END, WHILE);
            // this[BEGINABLE, FOR] = new ParseAction(FOR, VAR, IN, RANGE, DO, STMNTS, END, FOR);

            // this[OPTNL_ARGS, WITH] = new ParseAction(WITH ,VAR, ARGLIST);
            // this[OPTNL_ARGS, VAR] = new ParseAction();
            // this[OPTNL_ARGS, IF] = new ParseAction();
            // this[OPTNL_ARGS, END] = new ParseAction();
            // this[OPTNL_ARGS, CALL] = new ParseAction();
            // this[OPTNL_ARGS, FUNC] = new ParseAction();
            // this[OPTNL_ARGS, BEGIN] = new ParseAction(BEGINSTMNT);
            // this[OPTNL_ARGS, WAIT] = new ParseAction();

            // this[ARGLIST, SEPARATOR] = new ParseAction(SEPARATOR, VAR, ARGLIST);
            // this[ARGLIST, VAR] = new ParseAction();
            // this[ARGLIST, IF] = new ParseAction();
            // this[ARGLIST, END] = new ParseAction();
            // this[ARGLIST, CALL] = new ParseAction();
            // this[ARGLIST, BEGIN] = new ParseAction();
            // this[ARGLIST, WAIT] = new ParseAction();


            // this[RANGE, NUMERIC] = new ParseAction(NUMERIC, OP_RANGE, NUMERIC);

            // this[WAITSTMNT, WAIT] = new ParseAction(WAIT, NUMERIC, TIME_MOD);

            // this[TIME_MOD, TIME_HR] = new ParseAction(TIME_HR);
            // this[TIME_MOD, TIME_MIN] = new ParseAction(TIME_MIN);
            // this[TIME_MOD, TIME_SEC] = new ParseAction(TIME_SEC);
            // this[TIME_MOD, TIME_MS] = new ParseAction(TIME_MS);

            // this[CALLPARAMS, WITH] = new ParseAction(WITH, VAL, CALLPARAM);
            // this[CALLPARAM, SEPARATOR] = new ParseAction(SEPARATOR, VAL);
            // this[CALLPARAM, OP_RPAREN] = new ParseAction();
        }
    }
}