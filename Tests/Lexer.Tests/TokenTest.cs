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
            ScannerToken token = new ScannerToken(TokenType.VAL, "test", 1, 4);

            Assert.AreEqual(TokenType.VAL, token.Type, "The token did not match the desired type");
        }
        
        [Test]
        public void Test_TokenDoesNotAssignWrongType()
        {
            ScannerToken token = new ScannerToken(TokenType.VAL, "test", 1, 4);

            Assert.AreNotEqual(TokenType.ASSIGN, token.Type, "The token matched unexpectedly");
        }

        [Test]
        public void Test_CanConstructToken_WithThreeParams()
        {
            ScannerToken token = new ScannerToken(TokenType.VAL, 1, 4);

            Assert.AreEqual(TokenType.VAL, token.Type, "The token with three params did not match the desired type");
        }
        
        [Test]
        public void Test_TokenDoesNotAssignWrongType_WithThreeParams()
        {
            ScannerToken token = new ScannerToken(TokenType.VAL, 1, 4);

            Assert.AreNotEqual(TokenType.ASSIGN, token.Type, "The token with three params matched unexpectedly");
        }


    }
}
