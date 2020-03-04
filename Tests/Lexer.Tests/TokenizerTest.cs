using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;

namespace Lexer.Tests
{
    [TestFixture]
    public class TokenizerTest
    { 
        [SetUp]
        public void SetUp() 
        {
            List<string> lines = new List<string>();
            lines.Add("a is 5");
            
            using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter("program"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        [Test]
        public void ATest()
        {
            Assert.Pass();
        }
        
        [Test]
        public void ValTypeTest()
        {
            Tokenizer tokenizer = new Tokenizer("program");
            if (tokenizer.Tokens[0].Type == new Token(TokenType.VAR,"a",0,0).Type)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void KeywordTypeTest()
        {
            Tokenizer tokenizer = new Tokenizer("program");
            if (tokenizer.Tokens[1].Type == new Token(TokenType.ASSIGN,"is",0,0).Type)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void KeywordValTest()
        {
            Tokenizer tokenizer = new Tokenizer("program");
            if (tokenizer.Tokens[1].Value == new Token(TokenType.ASSIGN,"is",0,0).Value)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void DigitTypeTest()
        {
            Tokenizer tokenizer = new Tokenizer("program");
            if (tokenizer.Tokens[2].Type == new Token(TokenType.NUMERIC,"5",0,0).Type)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}