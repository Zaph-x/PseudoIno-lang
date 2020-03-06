using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lexer.Objects;

namespace Lexer
{
    public class Tokenizer
    {
       /*private List<string> Lines;
         private List<string> Elements = new List<string>();*/
        public List<Token> Tokens = new List<Token>();
        
        Recogniser recogniser = new Recogniser();
        
        private char CurrentChar {get; set;}
        private char NextChar {get; set;}
        private int Line {get;set;}
        private int Offset {get;set;}
        private StreamReader reader;

        public Tokenizer(StreamReader stream)
        {
            recogniser = new Recogniser();
            reader = stream;
        }
        
        public char Current()
        {
            return CurrentChar;
        }

        public Lexer.Objects.Token Token(TokenType type, string val)
        {
            return new Token(type, val, Line, Offset);
        }

        /// <summary>
        /// Sets current character as the next character in the stream and advances in the stream.
        /// </summary>
        public char Pop()
        {
            CurrentChar = (char)reader.Read();
            if (CurrentChar == '\n')
                Line++;
            return CurrentChar;
        }
        
        /// <summary>
        /// Peeks the next character in the stream and sets NextChar to the value
        /// </summary>
        public char Peek()
        {
            NextChar = (char)reader.Peek();
            return NextChar;
        }

        private bool IsEOL()
        {
            return CurrentChar == '\n';
        }

        public void GenerateTokens()
        {
            string subString = "";
            char subChar = '0';
            while ((subChar = Peek()) != 0)
            {
                subString.Append(Pop());
                if (subChar == '\n')
                {
                    Line++;
                    Offset = 0;
                }
                if (recogniser.IsDigit(subChar))
                {
                    while (recogniser.IsDigit(Peek()))
                    {
                        Token(TokenType.NUMERIC,subString); 
                    }
                    
                }
                    
            }
        }
        
    }
}
