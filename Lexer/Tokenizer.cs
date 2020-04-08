using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Lexer
{
    /// <summary>
    /// The class responsible for generating the tokens from the source language
    /// </summary>
    public class Tokenizer
    {
        /// <summary>
        /// The list of tokens generated when the source language is being scanned
        /// </summary>
        public List<Token> Tokens = new List<Token>();

        /// <summary>
        /// Initialisation of the recogniser
        /// </summary>
        Recogniser recogniser = new Recogniser();
        /// <summary>
        /// The current character in the sequencce
        /// </summary>
        /// <value>Is set when <c>Pop()</c> is called</value>
        private char CurrentChar { get; set; }
        /// <summary>
        /// The next character in the sequence
        /// </summary>
        /// <value>Is set when <c>Peek()</c> is called</value>
        private char NextChar { get; set; }
        /// <summary>
        /// The current line of the current character
        /// </summary>
        /// <value>Is incremented whenever <c>Pop()</c> reaches a newline character</value>
        public int Line { get; private set; }
        /// <summary>
        /// The current offset of the current character
        /// </summary>
        /// <value>Is incremented whenever <c>Pop()</c> is called. Resets when the Line is updated</value>
        public int Offset { get; private set; }
        /// <summary>
        /// The offset of the buffer. This is set such that the scanner can look ahead.
        /// </summary>
        /// <value>Is incremented each tile <c>Pop()</c> is called</value>
        public long BufferOffset { get; private set; }

        /// <summary>
        /// A bool value to check if the tokenizer found any illegal syntax
        /// </summary>
        /// <value>False, unless a syntax error has been found</value>
        public static bool HasError {get; set;}

        /// <summary>
        /// The stream that the scanner is reading from
        /// </summary>
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
        /// A function to generate a token with a value. 
        /// This will be used for tokens where it is imperative that the value is carried over to the target language.
        /// </summary>
        /// <param name="type">The tokentype to return</param>
        /// <param name="val">The value of the token</param>
        /// <returns>
        /// A token from the value.
        /// </returns>
        public Token Token(TokenType type, string val)
        {
            return new Token(type, val, Line, Offset);
        }

        /// <summary>
        /// A function to generate a token without a value.
        /// This will be used to generate tokens where the value does not have to be carried over to the target language
        /// </summary>
        /// <param name="type">The token type</param>
        /// <returns>
        /// A token without a value.
        /// </returns>
        public Token Token(TokenType type)
        {
            return new Token(type, "", Line, Offset);
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
        /// </summary>
        /// <returns>
        /// True if the current character is end of line, otherwise false.
        /// </returns>
        private bool IsEOL()
        {
            return CurrentChar == '\n';
        }
        /// <summary>
        /// Checks if a character is the end of line.
        /// </summary>
        /// <param name="character">The character to check</param>
        /// <returns>
        /// True if the current character is end of line, otherwise false.
        /// </returns>
        private bool IsEOL(char character)
        {
            return character == '\n';
        }

        /// <summary>
        /// Checks if the current character is the end of file character
        /// </summary>
        /// <returns>
        /// True if the current character is end of file, otherwise false.
        /// </returns>
        private bool IsEOF()
        {
            return (int)CurrentChar == '\uffff';
        }

        /// <summary>
        /// Checks if a given character is the end of file character
        /// </summary>
        /// <param name="character">The character to check</param>
        /// <returns>
        /// True if the character is the end of file character, otherwise false.
        /// </returns>
        private bool IsEOF(char character)
        {
            return (int)character == '\uffff';
        }

        /// <summary>
        /// Checks if the current character is a whitespace
        /// </summary>
        /// <returns>
        /// true if the current character is a whitespace, otherwise false
        /// </returns>
        private bool IsSpace()
        {
            return CurrentChar == ' ';
        }

        /// <summary>
        /// Checks if a given character is a whitespace
        /// </summary>
        /// <param name="character">The character to check</param>
        /// <returns>
        /// True if the character is a whitespace, otherwise false.
        /// </returns>
        private bool IsSpace(char character)
        {
            return character == ' ';
        }

        /// <summary>
        /// This function will scan a secquence of characters beginning with a digit.
        /// The function will traverse the stream until it sees a character that is no longer recognized as a digit.
        /// If the non-digit character is a period (.) and it is the first period in the sequence, the function will treat the following digits as floating points.
        /// If there are no digits after the period character, the scanner will append a 0 to the value, to ensure the value is a float in the target language.
        /// <example>The following sequences are recognized as numeric:
        /// <code>
        /// 2
        /// 42
        /// 42.1
        /// 54. # Will become 54.0
        /// </code>
        /// </example>
        /// </summary>
        private void ScanNumeric()
        {
            string subString = CurrentChar.ToString();
            while (recogniser.IsDigit(Peek()))
            {
                Pop();
                subString += CurrentChar;
            }
            // Make sure it isn't a range
            if (Peek() == '.' && recogniser.IsDigit(Peek(2)))
            {
                Pop();
                subString += CurrentChar;

                while (recogniser.IsDigit(Peek()))
                {
                    Pop();
                    subString += CurrentChar;
                }
                if (subString.Last() == '.')
                {
                    subString += "0";
                }
                Tokens.Add(Token(TokenType.NUMERIC_FLOAT, subString));
                return;
            }
            Tokens.Add(Token(TokenType.NUMERIC_INT, subString));
        }

        /// <summary>
        /// This function will scan a sequence of characters providing a range token to the token list.
        /// The function will traverse while the next character is a period (.) symbol.
        /// <exception cref="Lexer.Exceptions.InvalidSyntaxException">When the traversed sequence does not produce a token with the value of exavtly '..'</exception>
        /// </summary>
        private void ScanRange()
        {
            string subString = CurrentChar.ToString();
            // This should in reality only run once
            while (Peek() == '.')
            {
                Pop();
                subString += CurrentChar;
            }
            if (subString.Length != 2)
            {
                new InvalidSyntaxException($"Invalid range symbol. Range symbol must be '..' but was '{subString}'. Error at line {Line}:{Offset}.");
            }
            Tokens.Add(Token(TokenType.RANGE));
        }

        /// <summary>
        /// This function will scan a sequence of characters, providing a string token.
        /// The function will traverse the sequence, until it meets a quotation (") symbol.
        /// <example>An example of a valid string:
        /// <c>
        /// "Hello, World!"
        /// "I am a valid string,.!-/()*?#$"
        /// </c>
        /// </example>
        /// <exception cref="Lexer.Exceptions.InvalidSyntaxException">When the traversed sequence is not closed by a quotation symbol</exception>
        /// </summary>
        private void ScanString()
        {
            string subString = CurrentChar.ToString();

            while (Peek() != '"' && !IsEOF(Peek()))
            {
                Pop();
                subString += CurrentChar;
            }
            if (NextChar == '"')
            {
                Pop();
                subString += CurrentChar;
            }
            if (!subString.EndsWith('"'))
            {
                new InvalidSyntaxException($"Strings must be closed. Error at line {Line}:{Offset}.");
            }
            Tokens.Add(Token(TokenType.STRING, subString));
        }

        /// <summary>
        /// This function will scan a sequence of characters, providing a comment token.
        /// The function will traverse the sequence, until it reaches the end of line or end of file character.
        /// </summary>
        private void ScanComment()
        {
            string subString = CurrentChar.ToString();
            while (!IsEOL(Peek()) && !IsEOF(Peek()))
            {
                Pop();
                subString += CurrentChar;
            }
            Tokens.Add(Token(TokenType.COMMENT, subString));
        }

        /// <summary>
        /// This function will scan a sequence of characters, providing a multiline comment token.
        /// The function will traverse the sequence, until it meets a sequence representing the end of a multiline comment.
        /// <example>An example of a valid multiline comment:
        /// <c>
        /// &lt;# This is a valid multiline comment #&gt;
        /// &lt;# This is also
        /// a valid multiline comment#&gt;
        /// </c>
        /// </example>
        /// <exception cref="Lexer.Exceptions.InvalidSyntaxException">When the traversed sequence is not closed by a quotation symbol</exception>
        /// </summary>
        private void ScanMultiLineComment()
        {
            string subString = CurrentChar.ToString();
            while (!IsEOF(Peek()) && !subString.Contains(">#"))
            {
                Pop();
                subString += CurrentChar;
            }
            if (!subString.Contains(">#"))
            {
                new InvalidSyntaxException($"Multiline comments must be closed before reaching end of file. Error at line {Line}:{Offset}.");
            }
            Tokens.Add(Token(TokenType.MULT_COMNT, subString));
        }

        /// <summary>
        /// This function will scan a sequence of characters providing either a VAR token, a KEYWORD token, an INDEX token, a QUESTIONMARK token, or any ARRAY token.
        /// This is done by traversing the sequence of characters until it meets either a newline or any of the symbols representing any of the rest of the tokens.
        /// The time to look up a keyword is O(1) as this is done by using a hashed value in a dictionary.
        /// This function can produce multiple tokens within itself.
        /// <example> An example of possible accepted tokens:
        /// <c>
        /// a # Variable name
        /// is # Keyword
        /// for # Keyword
        /// a@2 # While the 2 is not recognised in this function this will result in tokens "VAR ARRAYINDEX NUMERIC"
        /// a[4] # This will result in the tokens "VAR ARRAYLEFT NUMERIC ARRAYRIGHT"
        /// </c>
        /// </example>
        /// </summary>
        private void ScanWord()
        {
            string subString = CurrentChar.ToString();
            while (recogniser.IsAcceptedCharacter(Peek()))
            {
                subString += Pop();
            }
            if (Keywords.Keys.TryGetValue(subString, out TokenType tokenType))
            {
                Tokens.Add(Token(tokenType));
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
                Pop();
                Tokens.Add(Token(TokenType.ARRAYINDEX));
            }
            else if (Peek() == '?')
            {
                Pop();
                Tokens.Add(Token(TokenType.OP_QUESTIONMARK));
            }
            else if (Peek() == '[')
            {
                Pop();
                Tokens.Add(Token(TokenType.ARRAYLEFT));
            }
            else if (Peek() == ']')
            {
                Pop();
                Tokens.Add(Token(TokenType.ARRAYRIGHT));
            }
        }

        /// <summary>
        /// This function will add an operator token to the token list.
        /// If the current character is a plus, a plus operator token will be added
        /// If the current character is a minus, a minus operator will be added
        /// If the current character is an asterisk, a times operator will be added
        /// If the current character is a forward slash, a divide operator will be added
        /// If the current character is a percentage symbol, a modulo operator will be added
        /// <exception cref="Lexer.Exceptions.InvalidSyntaxException">Should never be thrown</exception>
        /// </summary>
        private void ScanOperators()
        {
            switch (CurrentChar)
            {
                case '+':
                    Tokens.Add(Token(TokenType.OP_PLUS));
                    break;
                case '-':
                    Tokens.Add(Token(TokenType.OP_MINUS));
                    break;
                case '*':
                    Tokens.Add(Token(TokenType.OP_TIMES));
                    break;
                case '/':
                    Tokens.Add(Token(TokenType.OP_DIVIDE));
                    break;
                case '%':
                    Tokens.Add(Token(TokenType.OP_MODULO));
                    break;
                case '(':
                    Tokens.Add(Token(TokenType.OP_LPAREN));
                    break;
                case ')':
                    Tokens.Add(Token(TokenType.OP_RPAREN));
                    break;
                default:
                    new InvalidSyntaxException($"'{CurrentChar}' was not recognised as a valid operator. Error at line {Line}:{Offset}.");
                    return;
            }
        }

        /// <summary>
        /// A function to to generate tokens. This is done by reading from the stream and using any of the scan functions.
        /// </summary>
        public void GenerateTokens()
        {
            // Ensure we are not dealing with an empty file.
            while (!IsEOF(Peek()))
            {
                Pop();
                if (IsSpace()) { continue; }
                else if (recogniser.IsDigit(CurrentChar)) { ScanNumeric(); }
                else if (recogniser.IsAcceptedCharacter(CurrentChar)) { ScanWord(); }
                else if (CurrentChar == '.') { ScanRange(); }
                else if (CurrentChar == '_' && (recogniser.IsAcceptedCharacter(Peek()) || recogniser.IsDigit(Peek()))) { ScanWord(); }
                else if (CurrentChar == '#' && Peek() != '<') { ScanComment(); }
                else if (CurrentChar == '#' && Peek() == '<') { ScanMultiLineComment(); }
                else if ("+-*/%()".Contains(CurrentChar)) { ScanOperators(); }
                else if (CurrentChar == '"') { ScanString(); }

            }

        }
    }
}
