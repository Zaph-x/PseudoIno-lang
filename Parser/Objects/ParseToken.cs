namespace Parser.Objects
{
    /// <summary>
    /// An enum to determine the token type of a given token
    /// </summary>
    public enum ParseToken
    {
        START,
        STMNTS,
        STMNT,
        EXPR,
        MATHEXPR,
        OP_PLUS,
        OP_MINUS,
        OP_TIMES,
        OP_DIVIDE,
        OP_MODULO,
        LEFTARR,
        RIGHTARR,
        ARRINIT,
        BOOLEXPR,
        OP_EQUAL,
        OP_AND,
        OP_OR,
        OP_LESS,
        OP_GREATER,
        IFSTMNT,
        PIN,
        FUNCCALL,
        ARGLIST,
        FUNCTION,
        CODEBLOCK,
        ENDFUNC,
        BEGINSTMNT,
        BEGIN,
        LOOPW,
        ENDWHILE,
        LOOPF,
        ENDFOR,
        RANGE,
        VALUE,
        TRMNL
    }
}