using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace Lexer.Tests
{
    [TestFixture]
    public class TokenizerTest
    { 
        private string FakeContent = @"a is 
5";
        private byte[] FakeUTF8Bytes;

        [OneTimeSetUp]
        public void OnetimeSetUp() 
        {
            FakeUTF8Bytes = Encoding.UTF8.GetBytes(FakeContent);
        }

        [Test]
        public void ATest()
        {
            Assert.Pass();
        }
        
        [Test]
        public void Test_Pop_ReturnsCorrectCharacter()
        {
            string content = @"a is 4";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8); 
 
            Tokenizer tokenizer = new Tokenizer(FakeReader);

            Assert.AreEqual('a', tokenizer.Pop(), "Pop did not set the correct character");
        }

        [Test]
        public void Test_Pop_ShouldNotReturnWrongCharacter()
        { 
            string content = @"a is 4";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8); 

            Tokenizer tokenizer = new Tokenizer(FakeReader);

            Assert.AreNotEqual('W', tokenizer.Pop(), "Pop got the correct character when it should not");
        }

        [Test]
        public void Test_Peek_ShouldPeekCurrectChar()
        {   
            string content = @"a is 4";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8); 

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();
            
            Assert.AreEqual(' ', tokenizer.Peek(), "Peek did not get the correct character");
        }

        [Test]
        public void Test_Peek_ShouldNotPeekWrongChar()
        {
            string content = @"a is 4";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();
            
            Assert.AreNotEqual('W', tokenizer.Peek(), "Peek got the correct character when it should not");
        }

        [Test]
        public void Test_Peek_CanPeekMultipleCharacters()
        {
            string content = @"a is 4";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8); 

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();

            Assert.AreEqual(' ', tokenizer.Peek(), "Tokenizer did not find the correct character");
            Assert.AreEqual('i', tokenizer.Peek(2), "Tokenizer did not find the correct character");
            Assert.AreEqual(' ', tokenizer.Peek(), "Tokenizer did not find the correct character");
        }

        [Test]
        public void Test_Pop_ReturnProvidesCorrectLineNumber()
        {
            string content = "a is 4\nb is 5\nc is a + b";
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);
            
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            while(tokenizer.Pop() != '\n')
            {
                Console.WriteLine(tokenizer.Current());
            }

            tokenizer.Pop();

            Assert.AreEqual(2, tokenizer.Line, "Tokenizer did not return correct line number.");
        }


        // Helper functions
        
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(FakeContent);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}
