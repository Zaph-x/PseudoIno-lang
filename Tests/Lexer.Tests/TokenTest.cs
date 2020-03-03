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

            Assert.IsTrue(token.Type == TokenType.VAL);
        }
    }
}
