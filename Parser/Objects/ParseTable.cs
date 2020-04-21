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
            this[PROG, VAR] = new ParseAction(STMNTS);
            this[PROG, IF] = new ParseAction(STMNTS);
            this[PROG, CALL] = new ParseAction(STMNTS);
            this[PROG, FUNC] = new ParseAction(STMNTS);
            this[PROG, BEGIN] = new ParseAction(STMNTS);
            this[PROG, WAIT] = new ParseAction(STMNTS);
            this[PROG, APIN] = new ParseAction(STMNTS);
            this[PROG, DPIN] = new ParseAction(STMNTS);
            this[PROG, EOF] = new ParseAction(EOF);

            this[TYPE, STRING] = new ParseAction(STRING);
            this[TYPE, NUMERIC] = new ParseAction(NUMERIC);
            this[TYPE, BOOL] = new ParseAction(BOOL);
            this[TYPE, DPIN] = new ParseAction(DPIN);
            this[TYPE, APIN] = new ParseAction(APIN);

            this[STMNTS, VAR] = new ParseAction(STMNT);
            this[STMNTS, IF] = new ParseAction(STMNT);
            this[STMNTS, CALL] = new ParseAction(STMNT);
            this[STMNTS, FUNC] = new ParseAction(FUNCDECL);
            this[STMNTS, BEGIN] = new ParseAction(STMNT);
            this[STMNTS, WAIT] = new ParseAction(STMNT);
            this[STMNTS, APIN] = new ParseAction(STMNT);
            this[STMNTS, DPIN] = new ParseAction(STMNT);
            this[STMNTS, ELSE] = new ParseAction();

            this[STMNT, IF] = new ParseAction(IFSTMNT);
            this[STMNT, CALL] = new ParseAction(FUNCCALL);
            this[STMNT, BEGIN] = new ParseAction(BEGINSTMNT);
            this[STMNT, WAIT] = new ParseAction(WAITSTMNT);
            this[STMNT, VAR] = new ParseAction(VAR, ASSIGN, ASSIGNMENT);
            this[STMNT, APIN] = new ParseAction(APIN, ASSIGN, ASSIGNMENT);
            this[STMNT, DPIN] = new ParseAction(DPIN, ASSIGN, ASSIGNMENT);
            // this[STMNT, END] = new ParseAction(END, ENDABLE);

            // this[ENDABLE, VAR] = new ParseAction(VAR);
            // this[ENDABLE, FOR] = new ParseAction(FOR);
            // this[ENDABLE, WHILE] = new ParseAction(WHILE);


            this[ASSIGNMENT, VAR] = new ParseAction(VAR, EXPR);
            this[ASSIGNMENT, NUMERIC] = new ParseAction(NUMERIC, EXPR);
            this[ASSIGNMENT, OP_MINUS] = new ParseAction(OP_MINUS, NUMERIC, EXPR);
            this[ASSIGNMENT, STRING] = new ParseAction(STRING, EXPR);
            this[ASSIGNMENT, BOOL] = new ParseAction(BOOL, EXPR);
            this[ASSIGNMENT, DPIN] = new ParseAction(DPIN, EXPR);
            this[ASSIGNMENT, APIN] = new ParseAction(APIN, EXPR);
            this[ASSIGNMENT, OP_LPAREN] = new ParseAction(OP_LPAREN, ASSIGNMENT);

            this[EXPR, OP_PLUS] = new ParseAction(OP_PLUS, VAL, EXPR);
            this[EXPR, OP_MINUS] = new ParseAction(OP_MINUS, VAL, EXPR);
            this[EXPR, OP_TIMES] = new ParseAction(OP_TIMES, VAL, EXPR);
            this[EXPR, OP_DIVIDE] = new ParseAction(OP_DIVIDE, VAL, EXPR);
            this[EXPR, OP_MODULO] = new ParseAction(OP_MODULO, VAL, EXPR);
            this[EXPR, OP_RPAREN] = new ParseAction(OP_RPAREN, EXPR);

            this[EXPR, OP_AND] = new ParseAction(OP_AND, VAL, EXPR);
            this[EXPR, OP_OR] = new ParseAction(OP_OR, VAL, EXPR);
            this[EXPR, OP_EQUAL] = new ParseAction(OP_EQUAL, VAL, EXPR);
            this[EXPR, OP_NOT] = new ParseAction(OP_NOT, VAL, EXPR);
            this[EXPR, OP_LESS] = new ParseAction(OP_LESS, OP_OREQUAL, VAL, EXPR);
            this[EXPR, OP_GREATER] = new ParseAction(OP_GREATER, OP_OREQUAL, VAL, EXPR);

            this[EXPR, VAR] = new ParseAction();
            this[EXPR, IF] = new ParseAction();
            this[EXPR, DO] = new ParseAction();
            this[EXPR, END] = new ParseAction();
            this[EXPR, ELSE] = new ParseAction();
            this[EXPR, CALL] = new ParseAction();
            this[EXPR, FUNC] = new ParseAction();
            this[EXPR, BEGIN] = new ParseAction();
            this[EXPR, WAIT] = new ParseAction();

            this[OP_OREQUAL, OP_OR] = new ParseAction(OP_OR, OP_EQUAL);
            this[OP_OREQUAL, VAR] = new ParseAction();
            this[OP_OREQUAL, NUMERIC] = new ParseAction();
            this[OP_OREQUAL, BOOL] = new ParseAction();
            this[OP_OREQUAL, STRING] = new ParseAction();
            this[OP_OREQUAL, DPIN] = new ParseAction();
            this[OP_OREQUAL, APIN] = new ParseAction();

            this[VAL, VAR] = new ParseAction(VAR);
            this[VAL, NUMERIC] = new ParseAction(NUMERIC);
            this[VAL, OP_MINUS] = new ParseAction(OP_MINUS, NUMERIC);
            this[VAL, STRING] = new ParseAction(STRING);
            this[VAL, BOOL] = new ParseAction(BOOL);
            this[VAL, DPIN] = new ParseAction(DPIN);
            this[VAL, APIN] = new ParseAction(APIN);

            this[ARR, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);
            this[ARR, VAR] = new ParseAction();
            this[ARR, IF] = new ParseAction();
            this[ARR, END] = new ParseAction();
            this[ARR, ELSE] = new ParseAction();
            this[ARR, CALL] = new ParseAction();
            this[ARR, FUNC] = new ParseAction();
            this[ARR, BEGIN] = new ParseAction();
            this[ARR, WAIT] = new ParseAction();

            this[PIN, DPIN] = new ParseAction(DPIN);
            this[PIN, APIN] = new ParseAction(APIN);

            this[IFSTMNT, IF] = new ParseAction(IF, VAL, EXPR, DO, STMNTS, ELSESTMNT, END, IF);

            this[ELSESTMNT, ELSE] = new ParseAction(ELSE, ELSEIFSTMNT, STMNTS);
            this[ELSESTMNT, END] = new ParseAction();

            this[ELSEIFSTMNT, IF] = new ParseAction(IF, VAL, EXPR, DO, STMNTS, ELSESTMNT);
            this[ELSEIFSTMNT, CALL] = new ParseAction();
            this[ELSEIFSTMNT, BEGIN] = new ParseAction();
            this[ELSEIFSTMNT, WAIT] = new ParseAction();
            this[ELSEIFSTMNT, VAR] = new ParseAction();

            this[FUNCCALL, CALL] = new ParseAction(CALL, VAR, CALLPARAMS);

            this[FUNCDECL, FUNC] = new ParseAction(FUNC, VAR, OPTNL_ARGS, STMNTS, END, VAR);

            this[BEGINSTMNT, BEGIN] = new ParseAction(BEGIN, BEGINABLE);

            this[BEGINABLE, WHILE] = new ParseAction(WHILE, VAL, EXPR, DO, STMNTS, END, WHILE);
            this[BEGINABLE, FOR] = new ParseAction(FOR, VAR, IN, RANGE, DO, STMNTS, END, FOR);

            this[OPTNL_ARGS, WITH] = new ParseAction(WITH ,VAR, ARGLIST);
            this[OPTNL_ARGS, VAR] = new ParseAction();
            this[OPTNL_ARGS, IF] = new ParseAction();
            this[OPTNL_ARGS, END] = new ParseAction();
            this[OPTNL_ARGS, CALL] = new ParseAction();
            this[OPTNL_ARGS, FUNC] = new ParseAction();
            this[OPTNL_ARGS, BEGIN] = new ParseAction(BEGINSTMNT);
            this[OPTNL_ARGS, WAIT] = new ParseAction();

            this[ARGLIST, SEPARATOR] = new ParseAction(SEPARATOR, VAR, ARGLIST);
            this[ARGLIST, VAR] = new ParseAction();
            this[ARGLIST, IF] = new ParseAction();
            this[ARGLIST, END] = new ParseAction();
            this[ARGLIST, CALL] = new ParseAction();
            this[ARGLIST, BEGIN] = new ParseAction();
            this[ARGLIST, WAIT] = new ParseAction();


            this[RANGE, NUMERIC] = new ParseAction(NUMERIC, OP_RANGE, NUMERIC);

            this[WAITSTMNT, WAIT] = new ParseAction(WAIT, NUMERIC, TIME_MOD);

            this[TIME_MOD, TIME_HR] = new ParseAction(TIME_HR);
            this[TIME_MOD, TIME_MIN] = new ParseAction(TIME_MIN);
            this[TIME_MOD, TIME_SEC] = new ParseAction(TIME_SEC);
            this[TIME_MOD, TIME_MS] = new ParseAction(TIME_MS);

            this[CALLPARAMS, WITH] = new ParseAction(WITH, VAL, CALLPARAM);
            this[CALLPARAM, SEPARATOR] = new ParseAction(SEPARATOR, VAL);
            this[CALLPARAM, OP_RPAREN] = new ParseAction();
        }
    }
}