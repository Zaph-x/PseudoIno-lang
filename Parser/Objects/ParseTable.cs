using System;
using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    /// <summary>
    /// The class containing the Parse table for the compiler
    /// </summary>
    public class ParseTable
    {
        /// <summary>
        /// The dictionary to look up in, in order to get a token.
        /// This is a 2D dictionary, meaning two keys must be provided.
        /// <example>The following is an example of how the dictionary might look:
        /// | k1\k2   | ID  | ASSIGN | NUMERIC | STRING | MATH_OP |
        /// |---------|-----|--------|---------|--------|---------|
        /// | ID      | err | ASSIGN | err     | err    | MATH_OP |
        /// | ASSIGN  | ID  | err    | NUMERIC | STRING | err     |
        /// | NUMERIC | err | err    | err     | err    | MATH_OP |
        /// | STRING  | err | err    | err     | err    | err     |
        /// | MATH_OP | ID  | err    | NUMERIC | err    | err     |
        /// 
        /// Here it is specified that `ID -> ASSIGN -> STRING` is a valid sequence, while `ID -> ASSIGN -> STRING -> MATH_OP` is not.
        /// </example>
        /// </summary>
        /// <returns>A ParseTable Dictionary</returns>
        private Dictionary<Token, Dictionary<Token, Token>> dict = 
            new Dictionary<Token, Dictionary<Token, Token>>();

        /// <summary>
        /// The indexing to get a given token from the Dictionary. This property allows for array like indexing of the tokens, where one can call ParseTable[ASSIGN, NUMERIC];
        /// </summary>
        /// <value>A parse token representing the tokens provided.</value>
        public Token this[Token key1, Token key2]
        {
            get
            {
                return dict[key1][key2];
            }

            set
            {
                if (!dict.ContainsKey(key1))
                {
                    dict[key1] = new Dictionary<Token, Token>();
                }
                dict[key1][key2] = value;
            }
        }
    }
}