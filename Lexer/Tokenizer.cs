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
            {
                Line++;
                Pop();
            }
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
