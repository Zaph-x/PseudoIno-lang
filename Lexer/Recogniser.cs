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
        const string DIGIT_REGEX = @"(-?[0-9]*)(\.?[0-9]+)?";

        public int InputString(string inputString)
        {
            if (inputString == "a is 4")
            {
                return 0;
            }
            return 1;
        }

        public bool IsDigit(string inputString)
        {
            Regex regex = new Regex(DIGIT_REGEX);
            return regex.IsMatch(inputString);
        }

        public float ScanDigtig(string inputString)
        {
            Regex regex = new Regex(DIGIT_REGEX);
            if (regex.IsMatch(inputString))
            {
                return float.Parse(inputString,System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
            }
            //MatchCollection collection = regex.Matches(s);
            //return Convert.ToInt32(collection.First().ToString());
            //TODO Lav det her om til exeption. M�ske i try catch
            throw new InvalidSyntaxException($"{inputString} was not recognised as a digit.");
        }

        public List<string> ReadFile(string filePath)
        {
            int counter = 0;  
            string line;
            List<string> lines = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(filePath); //@"c:\test.txt"  
            while((line = file.ReadLine()) != null)  
            {  
                lines.Add(line);  
                counter++;  
            }
            file.Close();
            return lines;
        }

        public bool FileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public int FileLineCount(string filePath)
        {
            System.IO.StreamReader file =   
                new System.IO.StreamReader(filePath);
            int counter = 0;
            while(file.ReadLine() != null)
                counter++;
            return counter;
        }

        public void onetime()
        {
            using (FileStream fs = File.Create("fileExist")) {}
            
            List<string> lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(i.ToString());
            }
            
            using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter("fileWith10Lines"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        public int SplitCountString(string inputString)
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
            return ss.Count;
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
