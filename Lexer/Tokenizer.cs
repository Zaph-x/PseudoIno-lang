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
        
        Recogniser recogniser = new Recogniser();
        
        public Tokenizer(string inputFile)
        {
            recogniser = new Recogniser();
            
            FileToElements(inputFile);

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
        
        public Tokenizer(List<string> inputFile)
        {
            recogniser = new Recogniser();

            Lines = inputFile;
            LinesToElements();

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

        private void FileToElements(string inputFile)
        {
            recogniser = new Recogniser();
            if (File.Exists(inputFile))
            {
                Lines = recogniser.ReadFile(inputFile);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private void LinesToElements()
        {
            foreach (var line in Lines)
            {
                List <string> bits = recogniser.SplitString(line);
                foreach (var element in bits)
                {
                    Elements.Add(element);
                }
            }
        }
    }
}