using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Objects;

namespace Lexer
{
    public class Tokenizer
    {
        private List<string> Lines;
        private List<string> Elements;
        
        
        public Tokenizer(string inputFile)
        {
            Recogniser recogniser = new Recogniser();
            if (recogniser.FileExist(inputFile))
            {
                Lines = recogniser.ReadFile(inputFile);
                foreach (var line in Lines)
                {
                    List <string> bits = recogniser.SplitString(line);
                    foreach (var element in bits)
                    {
                        Elements.Add(element);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            foreach (var element in Elements)
            {
                //check keyword
                //check digit
                //check id
            }
        }
    }
}