using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Lexer.Exceptions;
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
        private long BufferOffset {get;set;}
        private StreamReader reader;

        /// <summary>
        /// The constructor for the Tokenizer class. This will set the iniitiate a reader and a recogniser.
        /// </summary>
        /// <param name="stream">The StreamReader responsible for reading a class</param>
        public Tokenizer(StreamReader stream)
        {
            recogniser = new Recogniser();
            reader = stream;
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
                Pop();
            }
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
        
        private bool IsEOF()
        {
            return CurrentChar == 0 || CurrentChar == -1;
        }
        
        private void ScanNumeric()
        {
            string subString = "";
            if (recogniser.IsDigit(Current()))
            {
                while (recogniser.IsDigit(Peek()))
                {
                    subString.Append(Pop());
                }
                if (Peek() == '.')
                {
                    subString.Append(Pop());
                    while (recogniser.IsDigit(Peek()))
                    {
                        subString.Append(Pop());
                    }    
                }

                if (Peek() != ' ' || Peek() != '\n')
                {
                    throw new InvalidSyntaxException($"Numeric literal can only contain numbers. Error at line {Line}:{Offset}");
                }
                Tokens.Add(Token(TokenType.NUMERIC,subString));
            }
        }

        private void ScanCharacter()
        {
            string subString = "";
            if (recogniser.IsAcceptedCharacter(Current()))
            {
                while (recogniser.IsAcceptedCharacter(Peek()))
                {
                    subString.Append(Pop());
                    if (recogniser.IsKeyword(subString))
                    {
                        
                    }
                }

                if (Peek() == '@')
                {
                    
                }
                else if (Peek() == '?')
                {
                    Tokens.Add(Token(TokenType.VAR,subString));
                    Pop();
                    Tokens.Add(Token(TokenType.OP_QUESTIONMARK,"?"));
                }
                else if (Peek() == '[')
                {
                    Tokens.Add(Token(TokenType.VAR,subString));
                    Pop();
                    Tokens.Add(Token(TokenType.ARRAYLEFT,"["));
                }
                else if (Peek() == ']')
                {
                    Tokens.Add(Token(TokenType.VAR,subString));
                    Pop();
                    Tokens.Add(Token(TokenType.ARRAYRIGHT,"]"));   
                }

                /*if (Peek() != ' ' || Peek() != '\n')
                {
                    throw new InvalidSyntaxException($"Illegal symbol. Error at line {Line}:{Offset}");
                }*/
                
            }
        }
        /// <summary>
        /// A function to to generate tokens. This is done by reading from the stream.
        /// </summary>
        public void GenerateTokens()
        {
            string subString = "";
            while (Peek() != 0 || Peek() != -1) // EOF
            {
                if (IsEOL())
                    break;
                ScanNumeric();
                ScanCharacter();
                
            }
            
        }
        
    }
}
