namespace Lexer.Objects
{
    public enum TokenType
    {
        /* Keywords */
        // decl
        ASSIGN, // a is 4
        // function
        SIZE_OF, // sizeof(a)
        // function call
        CALL,
        // scoping
        FUNC,
        END,
        // array
        ARRAYLEFT,
        ARRAYRIGHT,
        ARRAYINDEX,
        // loops
        FOR,
        WHILE,
        // logic
        IF,
        ELSE,
        // bool operators
        OP_GREATER,
        OP_EQUAL,
        OP_LESS,
        OP_AND,
        OP_OR,
        OP_NOT,
        OP_QUESTIONMARK,
        // predefined
        DPIN,
        APIN,
        // mandatory function shit
        LOOP_FN,
        // comments
        COMMENT,
        MULT_COMNT,
        
        /* TYPES */
        NUMERIC,
        STRING,
        BOOL,
        RANGE,
        
        /* Arithmetic operators */
        OP_PLUS,
        OP_MINUS,
        OP_TIMES,
        OP_DIVIDE,
        OP_MODULO,

        /* variable things */
        VAR,
        VAL,
        
        /* error token*/
        ERROR,
        
    }
}
