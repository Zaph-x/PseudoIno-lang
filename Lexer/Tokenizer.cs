using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Objects;

namespace Lexer
{
    public class Tokenizer
    {
        private List<string> Lines;
        private List<string> Elements = new List<string>();
        
        public List<Token> Tokens = new List<Token>();
        
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
                if (recogniser.IsKeyword(element))
                {
                    Tokens.Add(new Token(Keywords.Keys[element],element,0,0));
                }
                else if (recogniser.IsDigit(element) && element != "a")
                {
                    //Dummy token hardcoded
                    Tokens.Add(new Token(TokenType.NUMERIC,"5",0,0));
                }
                else
                {
                    Tokens.Add(new Token(TokenType.VAR,"a",0,0));
                }

                //check digit
                //check id
            }
        }
    }
}