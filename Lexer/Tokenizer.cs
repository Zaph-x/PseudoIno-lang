using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lexer.Objects;

namespace Lexer
{
    public class Tokenizer
    {
        public List<Token> Tokens = new List<Token>();
        
        Recogniser recogniser = new Recogniser();
        
        private char CurrentChar {get; set;}
        private char NextChar {get; set;}
        private int Line {get;set;}
        private int Offset {get;set;}
        private StreamReader reader;

        /// <summary>
        /// The constructor for the Tokenizer class. This will set the iniitiate a reader and a recogniser.
        /// </summary>
        /// <param name="stream">The StreamReader responsible for reading a class</param>
        public Tokenizer(StreamReader stream)
        {
            recogniser = new Recogniser();
            reader = stream;
        }
        
        /// <summary>
        /// A function to continously return the current character.
        /// </summary>
        /// <returns>
        /// The current character
        /// </returns>
        public char Current()
        {
            return CurrentChar;
        }

        /// <summary>
        /// A function to generate a token.
        /// </summary>
        /// <returns>
        /// A token from the value.
        /// </returns>
        public Token Token(TokenType type, string val)
        {
            return new Token(type, val, Line, Offset);
        }

        /// <summary>
        /// Sets current character as the next character in the stream and advances in the stream.
        /// </summary>
        /// <returns>
        /// The next character in the stream.
        /// </returns>
        public char Pop()
        {
            CurrentChar = (char)reader.Read();
            if (IsEOL())
                Line++;
            return CurrentChar;
        }
        
        /// <summary>
        /// Peeks the next character in the stream and sets NextChar to the value
        /// </summary>
        /// <returns>
        /// The next character.
        /// </returns>
        public char Peek()
        {
            NextChar = (char)reader.Peek();
            return NextChar;
        }

        /// <summary>
        /// Checks if the current character is end of line.
        /// <summary>
        /// <returns>
        /// True if the current character is end of line, otherwise false.
        /// </returns>
        private bool IsEOL()
        {
            return CurrentChar == '\n';
        }

        /// <summary>
        /// A function to to generate tokens. This is done by reading from the stream.
        /// </summary>
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
