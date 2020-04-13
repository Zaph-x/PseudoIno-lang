using System;
namespace Parser.Objects
{
    public class ParseAction
    {
        public int Type {get;set;}
        private ParseAction()
        {}

        public static ParseAction Error() => new ParseAction() {Type = -1};

        public override string ToString()
        {
            string returnString = "Type=";
            switch (this.Type)
            {
                case -1:
                returnString+="Error";
                break;
                default:
                returnString+="No value assigned";
                break;
            }
            return returnString;
        }

    }
}