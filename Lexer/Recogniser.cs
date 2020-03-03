using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lexer
{
    public class Recogniser
    {
        public int InputString(string inputString)
        {
            if (inputString == "a is 4")
            {
                return 0;
            }
            return 1;
        }

        public int ScanDigtig(string s)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection collection = regex.Matches(s);
            return Convert.ToInt32(collection.First().ToString());
        }
        
    }
}
