using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Lexer
{
    /// <summary>
    /// The class responsible for recognising characters in the language
    /// </summary>
    public class Recogniser
    {
        /// <summary>
        /// A function to check if a char is in a specific range, inclusive.
        /// </summary>
        /// <param name="checking">The character to check</param>
        /// <param name="lower">The lower bound</param>
        /// <param name="upper">the upper bound</param>
        /// <returns>
        /// True if the character is in the range, otherwise false.
        /// </returns>
        private bool IsBetween(char checking, char lower, char upper)
        {
            return checking >= lower && checking <= upper;
        }

        /// <summary>
        /// A function to check if a char is a digit. This is done by calling IsBetween.
        /// </summary>
        /// <param name="character">The character to validate</param>
        /// <returns>
        /// True if the char is a digit
        /// </returns>
        public bool IsDigit(char character)
        {
            return IsBetween(character, '0', '9');
        }

        /// <summary>
        /// A function that checks if a char is a character in the alphabet.
        /// </summary>
        /// <param name="character">The character to validate</param>
        /// <returns>
        /// True if the char is a character in the alphabet
        /// </returns>
        public bool IsAcceptedCharacter(char character)
        {
            return IsBetween(character, 'a', 'z') || IsBetween(character, 'A', 'Z');
        }

        /// <summary>
        /// A function that checks if a given string is a keyword.
        /// </summary>
        /// <param name="input">A string to check</param>
        /// <returns>
        /// True if the given string is a keyword
        /// </returns>
        public bool IsKeyword(string input)
        {
            return Keywords.Keys.ContainsKey(input);
        }
    }
}
