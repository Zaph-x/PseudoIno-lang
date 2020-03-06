using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;
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
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);
 
            Tokenizer tokenizer = new Tokenizer(FakeReader);

            Assert.AreEqual('a', tokenizer.Pop(), "Pop did not set the correct character");
        }

        [Test]
        public void Test_Pop_ShouldNotReturnWrongCharacter()
        {
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);
 
            Tokenizer tokenizer = new Tokenizer(FakeReader);

            Assert.AreNotEqual('W', tokenizer.Pop(), "Pop got the correct character when it should not");
        }

        [Test]
        public void Test_Peek_ShouldPeekCurrectChar()
        {
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();
            
            Assert.AreEqual(' ', tokenizer.Peek(), "Peek did not get the correct character");
        }

        [Test]
        public void Test_Peek_ShouldNotPeekWrongChar()
        {
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();
            
            Assert.AreNotEqual('W', tokenizer.Peek(), "Peek got the correct character when it should not");
        }

        [Test]
        public void Test_Peek_CanPeekMultipleCharacters()
        {
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);

            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.Pop();

            Assert.AreEqual(' ', tokenizer.Peek(), "Tokenizer did not find the correct character");
            Assert.AreEqual('i', tokenizer.Peek(2), "Tokenizer did not find the correct character");
            Assert.AreEqual(' ', tokenizer.Peek(), "Tokenizer did not find the correct character");
        }

        [Test]
        public void Test_GenerateTokens_thing()
        {
        }
    }
}
