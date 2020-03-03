using NUnit.Framework;
using Lexer.Objects;

namespace Lexer.Tests
{
    [TestFixture]
    public class TokenTest
    {
        [SetUp]
        public void SetUp() 
        {
        }

        [Test]
        public void Test_CanConstructToken()
        {
            Token token = new Token(TokenType.VAL, "test", 1, 4);

            Assert.AreEqual(TokenType.VAL, token.Type, "The token did not match the desired type");
        }
    }
}
