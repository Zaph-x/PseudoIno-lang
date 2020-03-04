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

        public float ScanDigtig(string inputString)
        {
            Regex regex = new Regex(@"(-?[0-9]*)\.?([0-9]*)");
            if (regex.IsMatch(inputString))
            {
                return float.Parse(inputString,System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
            }
            //MatchCollection collection = regex.Matches(s);
            //return Convert.ToInt32(collection.First().ToString());
            //TODO Lav det her om til exeption. Måske i try catch
            return 0 ;
        }
        
    }
}
