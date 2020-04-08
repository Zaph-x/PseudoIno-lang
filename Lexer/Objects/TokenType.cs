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
        //TODO Add LEQ and GRQ
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
        WITH,
        EQUALS,
        DO,
        IN,
        NUMERIC,
        NEWLINE,
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
        /// Pin token, non terminal
        /// </summary>
        PIN,
        /// <summary>
        /// Function call token, non terminal
        /// </summary>
        FUNCCALL,
        /// <summary>
        /// Argument list token, non terminal
        /// </summary>
        ARGLIST,
        /// <summary>
        /// Function token, non terminal
        /// </summary>
        FUNCTION,
        /// <summary>
        /// Function statement token, non terminal
        /// </summary>
        FUNCSTMNT,
        /// <summary>
        /// Function statements token, non terminal
        /// </summary>
        FUNCSTMNTS,
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
        /// <summary>
        /// Or equal token, non terminal
        /// </summary>
        OREQUAL,
        /// <summary>
        /// Assignment token, non terminal
        /// </summary>
        ASSIGNMENT,
        /// <summary>
        /// Type token, non terminal
        /// </summary>
        TYPE,
        /// <summary>
        /// Argument token, non terminal
        /// </summary>
        ARG,
        /// <summary>
        /// Optional arguments token, non terminal
        /// </summary>
        OPTNL_ARGS,
        /// <summary>
        /// Beginable token, non terminal
        /// </summary>
        BEGINABLE,
        /// <summary>
        /// Array token, non terminal
        /// </summary>
        ARR
    }
}
