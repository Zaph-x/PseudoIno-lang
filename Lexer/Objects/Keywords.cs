using System.Collections.Generic;

namespace Lexer.Objects
{
    public class Keywords
    {
        public static Dictionary<string,TokenType> Keys = new Dictionary<string,TokenType>()
        {
            {"is",TokenType.ASSIGN},
            {"begin",TokenType.BEGIN },
            {"end", TokenType.END}
        };
    }
}