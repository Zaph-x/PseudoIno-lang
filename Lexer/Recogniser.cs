using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Lexer
{
    public class Recogniser
    {
        private bool IsBetween(char checking, char lower, char upper)
        {
            return checking >= lower && checking <= upper;
        }

        public bool IsDigit(char character)
        {
            return IsBetween(character, '0', '9');
        }

        public bool IsAcceptedCharacter(char character)
        {
            return IsBetween(character, 'a', 'z') || IsBetween(character, 'A', 'Z');
        }

        public bool IsKeyword(string input)
        {
            return Keywords.Keys.ContainsKey(input);
        }
    }
}
