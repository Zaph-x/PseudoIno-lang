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
        public int Line {get;private set;}
        public int Offset {get;private set;}
        public long BufferOffset {get;private set;}
        private StreamReader reader;

        /// <summary>
        /// The constructor for the Tokenizer class. This will set the iniitiate a reader and a recogniser.
        /// </summary>
        /// <param name="stream">The StreamReader responsible for reading a class</param>
        public Tokenizer(StreamReader stream)
        {
            recogniser = new Recogniser();
            reader = stream;
            Offset = 0;
            Line = 1;
            BufferOffset = 0;
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
        /// This is also responsible for counting up lines and the offset, to provide context for error handling.
        /// </summary>
        /// <returns>
        /// The next character in the stream.
        /// </returns>
        public char Pop()
        {
            CurrentChar = (char)reader.Read();
            
            BufferOffset++;
            if (IsEOL())
            {
                Line++;
                Offset = 0;
            }

            Offset++;
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
        /// Peeks the nth character in the stream and sets NextChar to the value of the character
        /// </summary>
        /// <param name="positions">The amount of possitions to look ahead in the stream</param>
        /// <returns>
        /// The character n positions ahead.
        /// </returns>
        public char Peek(int positions)
        {
            reader.BaseStream.Position = BufferOffset + positions -1;
            NextChar = (char)reader.BaseStream.ReadByte();
            reader.BaseStream.Position = BufferOffset - positions +1;
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
