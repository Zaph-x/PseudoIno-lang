using System.Linq;
using System;
using System.Collections.Generic;
using static Lexer.Objects.TokenType;

namespace Lexer.Objects
{
    /// <summary>
    /// An enum to determine the token type of a given token
    /// </summary>
    public enum TokenType
    {
        /* error token*/
        ///<summary>
        /// Error token type
        /// </summary>
        ERROR = -1,
        /* Keywords */

        /// <summary>
        /// Begin token, terminal
        /// </summary>
        BEGIN,
        ///<summary>
        /// Assignment token type
        /// </summary>
        ASSIGN, // a is 4
        // function call
        ///<summary>
        /// Call token type
        /// </summary>
        CALL,
        // scoping
        ///<summary>
        /// Function token type
        /// </summary>
        FUNC,
        ///<summary>
        /// End token type
        /// </summary>
        END,
        // array
        ///<summary>
        /// Left array initialisation token type
        /// </summary>
        ARRAYLEFT,
        ///<summary>
        /// Right array initialisation token type
        /// </summary>
        ARRAYRIGHT,
        ///<summary>
        /// Array index accessor token type
        /// </summary>
        ARRAYINDEX,
        ARRAYACCESSING,
        INDEXER,
        // loops
        ///<summary>
        /// For loop token type
        /// </summary>
        FOR,
        ///<summary>
        /// While loop token type
        /// </summary>
        WHILE,
        // logic
        ///<summary>
        /// If statement token type
        /// </summary>
        IF,
        ///<summary>
        /// Else statement token type
        /// </summary>
        ELSE,
        // bool operators
        ///<summary>
        /// Bool greater than operator token type
        /// </summary>
        OP_GREATER,
        ///<summary>
        /// Bool equality operator token type
        /// </summary>
        OP_EQUAL,
        ///<summary>
        /// Bool less than operator token type
        /// </summary>
        OP_LESS,
        ///<summary>
        /// Bool and token type
        /// </summary>
        OP_AND,
        ///<summary>
        /// Bool or token type
        /// </summary>
        OP_OR,
        ///<summary>
        /// Bool not token type
        /// </summary>
        OP_NOT,
        ///<summary>
        /// Bool or equal token type
        /// </summary>
        OP_OREQUAL,
        ///<summary>
        /// Bool questionmark than token type
        /// </summary>
        OP_QUESTIONMARK,
        // predefined
        ///<summary>
        /// Predefined digital pin token type
        /// </summary>
        DPIN,
        ///<summary>
        /// Predefined analogue pin token type
        /// </summary>
        APIN,
        // mandatory function shit
        ///<summary>
        /// Mandatory loop function token type
        /// </summary>
        LOOP_FN,
        // comments
        ///<summary>
        /// Comment token type
        /// </summary>
        COMMENT,
        ///<summary>
        /// Multiline comment token type
        /// </summary>
        MULT_COMNT,

        /* TYPES */
        ///<summary>
        /// Numeric integer token type
        /// </summary>
        NUMERIC_INT,
        /// <summary>
        /// Numeric float token type
        /// </summary>
        NUMERIC_FLOAT,
        ///<summary>
        /// String token type
        /// </summary>
        STRING,
        ///<summary>
        /// Bool token type
        /// </summary>
        BOOL,
        ///<summary>
        /// Range token type
        /// </summary>
        RANGE,

        /* Arithmetic operators */
        ///<summary>
        /// Plus token type
        /// </summary>
        OP_PLUS,
        ///<summary>
        /// Minus token type
        /// </summary>
        OP_MINUS,
        ///<summary>
        /// Multiplication token type
        /// </summary>
        OP_TIMES,
        ///<summary>
        /// Divide token type
        /// </summary>
        OP_DIVIDE,
        ///<summary>
        /// Modulo token type
        /// </summary>
        OP_MODULO,
        ///<summary>
        /// Left parenthesis token type
        /// </summary>
        OP_LPAREN,
        ///<summary>
        /// Right parenthesis token type
        /// </summary>
        OP_RPAREN,

        /* variable things */
        ///<summary>
        /// Variable token type
        /// </summary>
        VAR,
        ///<summary>
        /// Value token type
        /// </summary>
        VAL,
        /// <summary>
        /// Wait token type
        /// </summary>
        WAIT,
        /// <summary>
        /// Millisecond token type
        /// </summary>
        TIME_MS,
        /// <summary>
        /// Second token type
        /// </summary>
        TIME_SEC,
        /// <summary>
        /// Minute token type
        /// </summary>
        TIME_MIN,
        /// <summary>
        /// Hour token type
        /// </summary>
        TIME_HR,
        /// <summary>
        /// With token, terminal
        /// </summary>
        WITH,
        /// <summary>
        /// Equals token, terminal
        /// </summary>
        EQUALS,
        /// <summary>
        /// Do token, terminal
        /// </summary>
        DO,
        /// <summary>
        /// In token, terminal
        /// </summary>
        IN,
        /// <summary>
        /// Numeric token, terminal
        /// </summary>
        NUMERIC,
        /// <summary>
        /// Newline token, terminal
        /// </summary>
        NEWLINE,
        /// <summary>
        /// End of file token, terminal
        /// </summary>
        EOF,
        //Parser
        /// <summary>
        /// Program token, non terminal
        /// </summary>
        PROG,
        /// <summary>
        /// Start token, non terminal
        /// </summary>
        START,
        /// <summary>
        /// Statements token, non terminal
        /// </summary>
        STMNTS,
        /// <summary>
        /// Statement token, non terminal
        /// </summary>
        STMNT,
        /// <summary>
        /// Expression token, non terminal
        /// </summary>
        EXPR,
        /// <summary>
        /// Math expression token, non terminal
        /// </summary>
        MATHEXPR,
        /// <summary>
        /// Math operation token, non terminal
        /// </summary>
        MATH_OP,
        /// <summary>
        /// Array init token, non terminal
        /// </summary>
        ARRINIT,
        /// <summary>
        /// Boolean expression token, non terminal
        /// </summary>
        BOOLEXPR,
        /// <summary>
        /// Boolean operation token, non terminal
        /// </summary>
        BOOL_OP,
        /// <summary>
        /// If statement token, non terminal
        /// </summary>
        IFSTMNT,
        /// <summary>
        /// ELSE statement token, non terminal
        /// </summary>
        ELSESTMNT,
        /// <summary>
        /// Else If statement token, non terminal
        /// </summary>
        ELSEIFSTMNT,
        /// <summary>
        /// Pin token, non terminal
        /// </summary>
        PIN,
        /// <summary>
        /// Function call token, non terminal
        /// </summary>
        FUNCCALL,
        /// <summary>
        /// Function declaration token, non terminal
        /// </summary>
        FUNCDECL,
        /// <summary>
        /// Argument list token, non terminal
        /// </summary>
        ARGLIST,
        /// <summary>
        /// Function token, non terminal
        /// </summary>
        FUNCTION,
        /// <summary>
        /// Code block token, non terminal
        /// </summary>
        CODEBLOCK,
        /// <summary>
        /// End function token, non terminal
        /// </summary>
        ENDFUNC,
        /// <summary>
        /// Begin statement token, non terminal
        /// </summary>
        BEGINSTMNT,
        /// <summary>
        /// Loop while token, non terminal
        /// </summary>
        LOOPW,
        /// <summary>
        /// Loop for token, non terminal
        /// </summary>
        LOOPF,
        /// <summary>
        /// End while token, non terminal
        /// </summary>
        ENDWHILE,
        /// <summary>
        /// End for token, non terminal
        /// </summary>
        ENDFOR,
        ENDVAR,
        /// <summary>
        /// Assignment token, non terminal
        /// </summary>
        ASSIGNMENT,
        /// <summary>
        /// Type token, non terminal
        /// </summary>
        TYPE,
        /// <summary>
        /// Declaration parameter token, terminal
        /// </summary>
        DECLPARAM,
        /// <summary>
        /// Optional arguments token, terminal
        /// </summary>
        DECLPARAMS,
        /// <summary>
        /// Beginable token, non terminal
        /// </summary>
        BEGINABLE,
        /// <summary>
        /// Array token, non terminal
        /// </summary>
        ARR,
        /// <summary>
        /// First paramter in function call, non terminal
        /// </summary>
        CALLPARAM,
        /// <summary>
        /// Additional parameters in function call, non terminal
        /// </summary>
        CALLPARAMS,
        /// <summary>
        /// Wait statement, non terminal
        /// </summary> 
        WAITSTMNT,
        /// <summary>
        /// Epsilon transition, non terminal
        /// </summary>
        EPSILON,
        /// <summary>
        /// Range operator, terminal
        /// </summary>
        OP_RANGE,
        /// <summary>
        /// Time modeifer, non terminal
        /// </summary>
        TIME_MOD,
        /// <summary>
        /// Comman seperator, terminal
        /// </summary>
        SEPARATOR,
        RETURN,
        RETSTMNT,
        ENDIF,
        NT_COMMENT,
        ASSIGNSTMNT,
        TERM, FOLLOWTERM, FACTOR, FOLLOWFACTOR, TERMOP, FACTOROP, OP_GEQ, OP_LEQ
    }

    /// <summary>
    /// A class with static classes to operate on token types.
    /// </summary>
    public class TokenTypeExpressions
    {
        /// <summary>
        /// This function will determine if a given token is a non-terminal
        /// </summary>
        /// <param name="type">The token to check</param>
        /// <returns>True if the token is a non-terminal. Else false.</returns>
        public static bool IsNonTerminal(TokenType type)
        {
            return type == PROG
            || type == COMMENT
            || type == STMNTS
            || type == STMNT
            || type == ASSIGNSTMNT
            || type == ASSIGNMENT
            || type == EXPR
            || type == FOLLOWTERM
            || type == TERM
            || type == FOLLOWFACTOR
            || type == FACTOR
            || type == ARRAYACCESSING
            || type == ARRAYINDEX
            || type == TERMOP
            || type == FACTOROP
            || type == BOOL_OP
            || type == OP_OREQUAL
            || type == VAL
            || type == ARR
            || type == PIN
            || type == IFSTMNT
            || type == ENDIF
            || type == ELSESTMNT
            || type == ELSEIFSTMNT
            || type == FUNCCALL
            || type == FUNCDECL
            || type == ENDFUNC
            || type == DECLPARAMS
            || type == DECLPARAM
            || type == RETSTMNT
            || type == BEGINSTMNT
            || type == BEGINABLE
            || type == LOOPW
            || type == ENDWHILE
            || type == LOOPF
            || type == ENDFOR
            || type == RANGE
            || type == WAITSTMNT
            || type == TIME_MOD
            || type == CALLPARAMS
            || type == CALLPARAM;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBlock(TokenType type)
        {
            return type == PROG
                   || type == IFSTMNT
                   || type == ELSE
                   || type == ELSEIFSTMNT
                   || type == FUNC
                   || type == WHILE
                   || type == FOR
                   || type == IF;

        }

        public static bool IsRef(TokenType type)
        {
            return type == ASSIGNMENT
                   || type == APIN
                   || type == DPIN
                   || type == VAR
                   || type == FUNCCALL;
        }

        public static bool IsDcl(TokenType type)
        {
            return type == ASSIGNMENT
                   || type == FUNC
                   || type == APIN
                   || type == DPIN;
        }
        /// <summary>
        /// Determines whether a token is an operator
        /// </summary>
        /// <param name="type">The token to test</param>
        /// <returns>True if a token is an operator. Else false</returns>
        public static bool IsOperator(TokenType type)
        {
            return type == OP_TIMES || type == OP_DIVIDE
            || type == OP_PLUS || type == OP_MINUS
            || type == OP_AND || type == OP_OR
            || type == OP_EQUAL || type == OP_NOT
            || type == OP_GREATER || type == OP_LESS
            || type == OP_MODULO || type == OP_RPAREN
            || type == OP_RPAREN || type == OP_LEQ
            || type == OP_GEQ;
        }

        /// <summary>
        /// This function will determine if a given token is terminal
        /// </summary>
        /// <param name="type">The token to test</param>
        /// <returns>True if the token is a terminal. Else false.</returns>
        public static bool IsTerminal(TokenType type) => !IsNonTerminal(type);
    }
}