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

        private char CurrentChar { get; set; }
        private char NextChar { get; set; }
        public int Line { get; private set; }
        public int Offset { get; private set; }
        public long BufferOffset { get; private set; }
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
            reader.BaseStream.Position = BufferOffset + positions - 1;
            NextChar = (char)reader.BaseStream.ReadByte();
            reader.BaseStream.Position = BufferOffset - positions + 1;
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

        private bool IsEOL(char character)
        {
            return character == '\n';
        }

        private bool IsEOF()
        {
            return (int)CurrentChar == '\uffff';
        }

        private bool IsEOF(char character)
        {
            return (int)character == '\uffff';
        }
        
        private bool IsSpace()
        {
            return CurrentChar == ' ';
        }
        private bool IsSpace(char character)
        {
            return character == ' ';
        }

        private void ScanNumeric()
        {
            string subString = CurrentChar.ToString();
            while (recogniser.IsDigit(Peek()))
            {
                Pop();
                subString += CurrentChar;
            }
            if (Peek() == '.')
            {
                Pop();
                subString += CurrentChar;
                while (recogniser.IsDigit(Peek()))
                {
                    Pop();
                    subString += CurrentChar;
                }
            }

            if (!IsEOL(NextChar) && !IsEOF(NextChar) && !IsSpace(NextChar))
            {
                throw new InvalidSyntaxException($"Numeric literal can only contain numbers. Error at line {Line}:{Offset}. Found '{NextChar}({(int)NextChar})'.");
            }
            Tokens.Add(Token(TokenType.NUMERIC, subString));
        }

        private void ScanCharacter()
        {
            string subString = CurrentChar.ToString();
            while (recogniser.IsAcceptedCharacter(Peek()))
            {
                subString += Pop();
            }
            if (Keywords.Keys.TryGetValue(subString, out TokenType tokenType))
            {
                Tokens.Add(Token(tokenType, subString));
                return;
            }
            Tokens.Add(Token(TokenType.VAR, subString));
            subString = "";
            if (Peek() == '\n')
            {
                return;
            }
            if (Peek() == '@')
            {
                Tokens.Add(Token(TokenType.VAR, subString));
                Pop();
                Tokens.Add(Token(TokenType.ARRAYINDEX, subString));
            }
            else if (Peek() == '?')
            {
                Tokens.Add(Token(TokenType.VAR, subString));
                Pop();
                Tokens.Add(Token(TokenType.OP_QUESTIONMARK, "?"));
            }
            else if (Peek() == '[')
            {
                Tokens.Add(Token(TokenType.VAR, subString));
                Pop();
                Tokens.Add(Token(TokenType.ARRAYLEFT, "["));
            }
            else if (Peek() == ']')
            {
                Tokens.Add(Token(TokenType.VAR, subString));
                Pop();
                Tokens.Add(Token(TokenType.ARRAYRIGHT, "]"));
            }

            /*if (Peek() != ' ' || Peek() != '\n')
            {
                throw new InvalidSyntaxException($"Illegal symbol. Error at line {Line}:{Offset}");
            }*/
        }
        /// <summary>
        /// A function to to generate tokens. This is done by reading from the stream.
        /// </summary>
        public void GenerateTokens()
        {
            Peek();
            while (!IsEOF(NextChar)) // EOF
            {
                Pop();
                if (recogniser.IsDigit(CurrentChar))
                    ScanNumeric();
                else if (recogniser.IsAcceptedCharacter(CurrentChar))
                    ScanCharacter();

            }

        }

    }
}
