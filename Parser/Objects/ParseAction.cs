using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseAction
    {
        public int Type { get; private set; }
        public List<TokenType> Product { get; private set; } = new List<TokenType>();
        public ParseAction(int type, params TokenType[] types)
        {
            Product.AddRange(types);
            Type = type;
        }

        public static ParseAction Error() => new ParseAction(-1) {Product = new List<TokenType>() { TokenType.ERROR } };

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