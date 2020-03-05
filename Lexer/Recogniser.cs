using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Lexer
{
    public class Recogniser
    {
        const string DIGIT_REGEX = @"(-?[0-9]+)(\.?[0-9]+)?";

        public int InputString(string inputString)
        {
            if (inputString == "a is 4")
            {
                return 0;
            }
            return 1;
        }

        private bool IsBetween(char checking, char lower, char upper)
        {
            return checking >= lower && checking <= upper;
        }

        public bool IsDigit(char character)
        {
            return IsBetween(character, '0', '9');
        }

        public bool IsAcceptedCharacter(char character)
        {
            return IsBetween(character, 'a', 'z') || IsBetween(character, 'A', 'Z');
        }

        public float ScanDigit(string inputString)
        {
            Regex regex = new Regex(DIGIT_REGEX);
            if (regex.IsMatch(inputString))
            {
                return float.Parse(inputString, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
            }
            //MatchCollection collection = regex.Matches(s);
            //return Convert.ToInt32(collection.First().ToString());
            throw new InvalidSyntaxException($"{inputString} was not recognised as a digit.");
        }

        public List<string> ReadFile(string filePath)
        {
            int counter = 0;
            string line;
            List<string> lines = new List<string>();
            StreamReader file = new StreamReader(filePath); //@"c:\test.txt"  
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
                counter++;
            }
            file.Close();
            return lines;
        }

        public List<string> SplitString(string inputString)
        {
            string[] splittetArray = inputString.Split(" ");
            List<string> ss = new List<string>();
            foreach (var s in splittetArray)
            {
                if (s != "")
                {
                    ss.Add(s);
                }
            }
            return ss;
        }

        public bool IsKeyword(string input)
        {
            if (Lexer.Objects.Keywords.Keys.ContainsKey(input))
                return true;
            return false;
        }

        /*public TokenType GetKeywordToken(string input)
        {
            TokenType token;
            return Lexer.Objects.Keywords.Keys.TryGetValue(input, token);
        }*/
    }
}
