using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Exceptions;
using Lexer.Objects;
using static Lexer.Objects.TokenType;

namespace Parser.Objects
{
    public class ParseTable
    {
        private ScannerToken Current { get; set; }
        private ScannerToken Next { get; set; }
        public Dictionary<TokenType, Dictionary<TokenType, ParseAction>> Table { get; private set; }

        public ParseAction this[ScannerToken key1, ScannerToken key2]
        {
            get
            {
                Current = key1;
                Next = key2;
                return Table[key1.Type][key2.Type];
            }
        }

        public ParseAction this[TokenType key1, TokenType key2]
        {
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

            this[PROG, STMNTS] = new ParseAction(STMNTS);

            this[TYPE, STRING] = new ParseAction(STRING);
            this[TYPE, NUMERIC] = new ParseAction(NUMERIC);
            this[TYPE, BOOL] = new ParseAction(BOOL);
            this[TYPE, ARR] = new ParseAction(ARR);
            this[TYPE, PIN] = new ParseAction(PIN);


            this[STMNTS, STMNT] = new ParseAction(STMNT, STMNTS);
            this[STMNTS, FUNCDECL] = new ParseAction(FUNCDECL, STMNTS);
            this[STMNTS, EPSILON] = new ParseAction();

            this[STMNT, VAR] = new ParseAction(VAR, ASSIGN, ASSIGNMENT);
            this[STMNT, IF] = new ParseAction(IFSTMNT);
            this[STMNT, FUNC] = new ParseAction(FUNCCALL);
            this[STMNT, WAIT] = new ParseAction(WAITSTMNT);
            this[STMNT, BEGIN] = new ParseAction(BEGINSTMNT);

            this[ASSIGNMENT, VAL] = new ParseAction(VAL, EXPR);
            this[ASSIGNMENT, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);

            this[EXPR, MATH_OP] = new ParseAction(MATH_OP, VAL, EXPR);
            this[EXPR, BOOL_OP] = new ParseAction(BOOL_OP, VAL, EXPR);
            this[EXPR, EPSILON] = new ParseAction();

            this[MATH_OP, OP_PLUS] = new ParseAction(OP_PLUS);
            this[MATH_OP, OP_MINUS] = new ParseAction(OP_MINUS);
            this[MATH_OP, OP_DIVIDE] = new ParseAction(OP_DIVIDE);
            this[MATH_OP, OP_TIMES] = new ParseAction(OP_TIMES);
            this[MATH_OP, OP_MODULO] = new ParseAction(OP_MODULO);

            this[BOOL_OP, EQUALS] = new ParseAction(EQUALS);
            this[BOOL_OP, OP_AND] = new ParseAction(OP_AND);
            this[BOOL_OP, OP_OR] = new ParseAction(OP_OR);
            this[BOOL_OP, OP_GREATER] = new ParseAction(OP_GREATER, OP_OREQUAL);
            this[BOOL_OP, OP_LESS] = new ParseAction(OP_LESS, OP_OREQUAL);

            this[OP_OREQUAL, OP_OR] = new ParseAction(OP_OR, OP_EQUAL);
            this[OP_OREQUAL, EPSILON] = new ParseAction();

            this[VAL, VAR] = new ParseAction(VAR);
            this[VAL, NUMERIC] = new ParseAction(NUMERIC);
            this[VAL, STRING] = new ParseAction(STRING);
            this[VAL, PIN] = new ParseAction(PIN);

            this[ARR, ARRAYLEFT] = new ParseAction(ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARR);
            this[ARR, EPSILON] = new ParseAction();

            this[PIN, DPIN] = new ParseAction(DPIN);
            this[PIN, APIN] = new ParseAction(APIN);

            this[IFSTMNT, IF] = new ParseAction(IF, VAL, EXPR, DO, STMNTS, ELSESTMNT, END, IF);

            this[ELSESTMNT, ELSE] = new ParseAction(ELSE, ELSEIFSTMNT, STMNTS, ELSESTMNT);
            this[ELSESTMNT, EPSILON] = new ParseAction();

            this[ELSEIFSTMNT, IF] = new ParseAction(IF, VAL, EXPR);
            this[ELSEIFSTMNT, EPSILON] = new ParseAction();

            this[FUNCCALL, CALL] = new ParseAction(CALL, VAR, CALLPARAM);

            this[FUNCDECL, FUNC] = new ParseAction(FUNC, VAR, OPTNL_ARGS, STMNTS, END, VAR);

            this[BEGINSTMNT, BEGIN] = new ParseAction(BEGIN, BEGINABLE);

            this[BEGINABLE, WHILE] = new ParseAction(WHILE, VAL, EXPR, DO, STMNTS, END, WHILE);
            this[BEGINABLE, FOR] = new ParseAction(FOR, VAR, IN, RANGE, DO, STMNTS, END, FOR);

            this[OPTNL_ARGS, WITH] = new ParseAction(WITH, ARG, ARGLIST);
            this[OPTNL_ARGS, EPSILON] = new ParseAction();

            this[ARGLIST, SEPARATOR] = new ParseAction(SEPARATOR, ARG, ARGLIST);
            this[ARGLIST, EPSILON] = new ParseAction();

            this[ARG, TYPE] = new ParseAction(TYPE, VAR);

            this[WAITSTMNT, WAIT] = new ParseAction(WAIT, NUMERIC, TIME_MOD);

            this[TIME_MOD, TIME_HR] = new ParseAction(TIME_HR);
            this[TIME_MOD, TIME_MIN] = new ParseAction(TIME_MIN);
            this[TIME_MOD, TIME_SEC] = new ParseAction(TIME_SEC);
            this[TIME_MOD, TIME_MS] = new ParseAction(TIME_MS);

            this[CALLPARAM, WITH] = new ParseAction(WITH, OP_LPAREN, VAR, CALLPARAMS, OP_RPAREN);

            this[CALLPARAMS, SEPARATOR] = new ParseAction(SEPARATOR, VAR, CALLPARAMS);
            this[CALLPARAMS, EPSILON] = new ParseAction();
        }
    }
}