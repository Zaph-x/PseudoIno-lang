using NUnit.Framework;
using Lexer;
using Lexer.Exceptions;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lexer.Tests
{
    public class RecogniserTest
    {
        Recogniser recogniser = new Recogniser();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            onetime();
        }

        [Test]
        public void Test_InputString_RecogniseStringInLanguage()
        {
            Assert.AreEqual(0, recogniser.InputString("a is 4"), "Recogniser did not recognise the string");
        }

        [Test]
        public void Test_InputString_DoNotRecogniseMalformedString()
        {
            Assert.AreNotEqual(0, recogniser.InputString("a 2 is"), "Recogniser recognised the string when it should not");

        }

        [Test]
        public void Test_InputString_DoNotRecogniseStringNotInLanguage()
        {
            Assert.AreNotEqual(0, recogniser.InputString("neiojsfj3324"), "Recogniser recognised the string when it should not");

        }
        
        [Test]
        public void Test_IsDigit_AcceptsADigitChar()
        {
            Assert.IsTrue(recogniser.IsDigit('2'), "The character '2' was not recignised as a digit");
        }

        [Test]
        public void Test_IsDigit_RejectsAlphabeCharacters()
        {
            Assert.IsFalse(recogniser.IsDigit('a'), "The character 'a' was accepted when it shouldn't be");
        }

        [Test]
        public void Test_IsAcceptedCharacter_AcceptActualCharacters()
        {
            Assert.IsTrue(recogniser.IsAcceptedCharacter('a'), "The character 'a' was not recognised as a character");
        }

        [Test]
        public void Test_IsAcceptedCharacter_RejectDigits()
        {
            Assert.IsFalse(recogniser.IsAcceptedCharacter('4'), "The character '4' was not rejected when it should be");
        }

        [Test]
        public void Test_ScanDigit_CanScanIntegers()
        {
            Assert.AreEqual(2222, recogniser.ScanDigit("2222"), "Recogniser did not recognise the digit (Int)");
        }

        [Test]
        public void Test_WrongInputShouldThrowException()
        {
            Assert.Throws<InvalidSyntaxException>(() =>
            {
                recogniser.ScanDigit("NotAValidDigit");
            }, "Recogniser failed to catch invalid digit");

        }

        [Test]
        public void Test_ScanDigit_CanScanFloats()
        {
            Assert.AreEqual(1.1f, recogniser.ScanDigit("1.1"), "Recogniser did not recognise the digit (Float)");
        }

        [Test]
        public void Test_ScanDigit_CanRemoveWhitespaces()
        {
            Assert.AreEqual(22, recogniser.ScanDigit("                22"), "Recogniser did not recognise the digit (Int)");
        }

        [Test]
        public void Test_ScanDigit_CanRemoveWhitespacesForFloats()
        {
            Assert.AreEqual(111.1011f, recogniser.ScanDigit("      111.1011"), "Recogniser did not recognise the digit (Float)");
        }
        [Test]
        public void Test_ScanDigit_CanScanNegativeDigits()
        {
            Assert.AreEqual(-2222, recogniser.ScanDigit("-2222"), "Recogniser did not recognise the digit (Int)");
        }

        [Test]
        public void Test_ScanDigit_CanScanNegativeFloats()
        {
            Assert.AreEqual(-1.1f, recogniser.ScanDigit("-1.1"), "Recogniser did not recognise the digit (Float)");
        }

        [Test]
        public void Test_SplitCountString_ReturnsCorrectAmountOfStrings()
        {
            string inputString = "a is 1";
            List<string> substrings = recogniser.SplitString(inputString);
            Assert.AreEqual(3, substrings.Count, "The recogniser did not find the correct amount of substrings");
        }


        [Test]
        public void Test_SplitCountString_ReturnsCorrectWithExtraWhiteSpace()
        {
            string inputString = "a           is  1";
            List<string> substrings = recogniser.SplitString(inputString);
            Assert.AreEqual(3, substrings.Count, "Whitespaces were not ignored.");
        }

        [Test]
        public void Test_SplitCountString_ReturnsZeroOnEmptyString()
        {
            string inputString = "";
            List<string> substrings = recogniser.SplitString(inputString);
            Assert.AreEqual(0, substrings.Count, "Emptystring was recognised as a string");
        }

        [Test]
        public void Test_SplitCountString_DoNotRecogniseNewLine()
        {
            string inputString = "test \n test";
            List<string> substrings = recogniser.SplitString(inputString);
            Assert.AreNotEqual(2, substrings.Count, "Newline was recognised as part of the string");
        }

        [Test]
        public void Test_ReadFile_ReturnsCorrectAmountOfLines()
        {

            List<string> lines = recogniser.ReadFile("fileWith10Lines");
            Assert.AreEqual(10, lines.Count, "File did not contain 10 lines when it should.");
        }

        [Test]
        public void Test_FileLineCount_ReturnsNonZeroAmountOfLines()
        {
            List<string> lines = recogniser.ReadFile("fileWith10Lines");
            Assert.AreNotEqual(0, lines.Count, "File contained 0 lines when it should contain more.");
        }

        // Helper functions

        public void onetime()
        {
            using (FileStream fs = File.Create("fileExist")) { }

            List<string> lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(i.ToString());
            }

            using (StreamWriter file = new StreamWriter("fileWith10Lines"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}
