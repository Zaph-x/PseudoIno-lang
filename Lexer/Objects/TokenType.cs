namespace Lexer.Objects
{
    /// <summary>
    /// An enum to determine the token type of a given token
    /// </summary>
    public enum TokenType
    {
        /* Keywords */
        
        ///<summary>
        /// Assignment token type
        /// </summary>
        ASSIGN, // a is 4
        // function
        ///<summary>
        /// Size of function token type
        /// </summary>
        SIZE_OF, // sizeof(a)
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
        ERROR,
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
        ENDOFLINE
    }
}
