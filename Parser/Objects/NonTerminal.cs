using System;

namespace Parser.Objects
{ 
    public enum NonTerminal
    {
        // Non terminals from CFG - Donated by Torben
        START,
        STMNTS,
        STMNT,
        EXPR,
        MATHEXPR,
        MATHOP,
        LEFTARR,
        RIGHTARR,
        ARRINIT,
        BOOLEXPR,
        BOOLOP,
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