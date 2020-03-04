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
        public void ValTest()
        {
            Tokenizer tokenizer = new Tokenizer("program");
            if (tokenizer.Tokens[0] == new Token(TokenType.VAR,"a",0,0))
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}