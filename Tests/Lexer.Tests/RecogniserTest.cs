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
        private string FakeContent = "Hello, World!";
        private byte[] FakeUTF8Bytes;
        Recogniser recogniser = new Recogniser();
        private MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
        private StreamReader FakeReader;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            FakeUTF8Bytes = Encoding.UTF8.GetBytes(FakeContent);
            FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);
            onetime();
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
    }
}
