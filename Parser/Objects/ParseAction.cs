using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace Parser.Objects
{
    /// <summary>
    /// A transition rule in the parse table
    /// </summary>
    public class ParseAction
    {
        /// <summary>
        /// The current action type signified by an integer value
        /// </summary>
        /// <value>-1 if error. Else <see cref="ParseTable.Table"/></value>
        public int Type { get; private set; }
        /// <summary>
        /// The product of the transition. This is set in the parse table
        /// </summary>
        /// <returns>A list of tokens to match. <see cref="ParseTable.Table"/></returns>
        public List<TokenType> Product { get; private set; } = new List<TokenType>();
        /// <summary>
        /// The constructor of a parse action. This contains the type and the production list.
        /// </summary>
        /// <param name="type">The integer representation of the transition</param>
        /// <param name="types">The list of production rules.</param>
        public ParseAction(int type, params TokenType[] types)
        {
            Product.AddRange(types);
            Type = type;
        }
        /// <summary>
        /// Returns an ERROR transition. Used when initialising the parse table.
        /// </summary>
        /// <returns>An error transition.</returns>
        public static ParseAction Error() => new ParseAction(-1) {Product = new List<TokenType>() { TokenType.ERROR } };

        /// <inheritdocs cref="System.Object"/>
        public override string ToString()
        {
            string returnString = "Type=";
            switch (this.Type)
            {
                case -1:
                    returnString += "Error";
                    break;
                case 0:
                    returnString += "Production Products=";
                    Product.ForEach(str => returnString += str + ";");
                    break;
                default:
                    returnString += "No value assigned";
                    break;
            }
            return returnString;
        }

    }
}