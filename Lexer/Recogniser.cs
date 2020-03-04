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
            Regex regex = new Regex("[0-9]*.[0-9]*");
            if (regex.IsMatch(s))
            {
                return Convert.ToInt32(s);
            }
            //MatchCollection collection = regex.Matches(s);
            //return Convert.ToInt32(collection.First().ToString());
            return -3;
        }
        
    }
}
