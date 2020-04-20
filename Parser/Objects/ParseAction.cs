using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseAction
    {
        public int Type { get; private set; }
        public List<TokenType> Product { get; private set; } = new List<TokenType>();
        public ParseAction(params TokenType[] types)
        {
            Product.AddRange(types);
            Type = 0;
        }

        public static ParseAction Error() => new ParseAction() { Type = -1 };

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