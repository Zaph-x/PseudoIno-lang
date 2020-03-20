namespace Lexer.Objects
{
    /// <summary>
    /// An enum to determine the token type of a given token
    /// </summary>
    public enum TokenType
    {
        /* Keywords */
        /// <summary>
        /// A token represeting a begin statement
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

        /* error token*/
        ///<summary>
        /// Error token type
        /// </summary>
        ERROR = -1,
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
        /// Line break token. This is equivalent to \n
        /// </summary>
        LINEBREAK,

        //Parser
        /// <summary>
        /// A token representing the start of a program
        /// </summary>
        START,
        /// <summary>
        /// A token representing a collection of statments
        /// </summary>
        STMNTS,
        /// <summary>
        /// A token representing a statement
        /// </summary>
        STMNT,
        /// <summary>
        /// A token representing a collection of function statements
        /// <example>These statements include
        /// if a equals 4?
        /// for a in 1..4
        /// while a equals b?
        /// </example>
        /// </summary>
        FUNC_STMTS,
        /// <summary>
        /// A token representing a function statement
        /// </summary>
        FUNC_STMT,
        /// <summary>
        /// A token representing a collection of assignment tokens
        /// </summary>
        ASSIGNMENT,

        /// <summary>
        /// A token representing an expression
        /// </summary>
        EXPR,
        /// <summary>
        /// A token representing a mathematical expression
        /// </summary>
        MATHEXPR,
        /// <summary>
        /// A token representing an array initialisation
        /// </summary>
        ARRINIT,
        /// <summary>
        /// A token representing a mathematical operator
        /// </summary>
        MATH_OP,
        /// <summary>
        /// A token representing an expression with a boolean value
        /// </summary>
        BOOL_OP,
        /// <summary>
        /// A token representing an if if statement
        /// </summary>
        IFSTMNT,
        /// <summary>
        /// A token representing a pin in the program
        /// </summary>
        PIN,
        /// <summary>
        /// A token representing a function call
        /// </summary>
        FUNC_CALL,
        /// <summary>
        /// A token representing an argument list for a function
        /// </summary>
        ARGLIST,
        /// <summary>
        /// A token representing a function definition
        /// </summary>
        FUNCTION,
        /// <summary>
        /// A token representing the optional function arguments
        /// </summary>
        OPT_ARGS,
        /// <summary>
        /// A token representing the list of arguments for a given function
        /// </summary>
        ARG_LIST,
        /// <summary>
        /// A token representing an argument in the argument list
        /// </summary>
        ARG,
        /// <summary>
        /// A token representing the end of a function
        /// </summary>
        ENDFUNC,
        /// <summary>
        /// A token representing a begin statement
        /// </summary>
        BEGINSTMNT,
        /// <summary>
        /// A token representing a while loop
        /// </summary>
        LOOPW,
        /// <summary>
        /// A token representing a for loop
        /// </summary>
        LOOPF,
        /// <summary>
        /// A token representing the end of a while loop
        /// </summary>
        ENDWHILE,
        /// <summary>
        /// A token representing the end of a for loop
        /// </summary>
        ENDFOR,
    }
}
