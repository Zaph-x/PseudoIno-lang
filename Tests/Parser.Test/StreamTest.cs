using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;
using Parser.Objects;

namespace Parser.Test
{
    public class StreamTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Stream_Current_type()
        {
            List<ScannerToken> scannerTokens = new List<ScannerToken>();
            scannerTokens.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            scannerTokens.Add(new ScannerToken(TokenType.ASSIGN,1,2));
            scannerTokens.Add(new ScannerToken(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(scannerTokens);
            streamToken.Advance();

            Assert.AreEqual(streamToken.Current().Type,TokenType.VAR);
        }
        
        [Test]
        public void Test_Stream_Peek_type()
        {
            List<ScannerToken> tokens = new List<ScannerToken>();
            tokens.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            tokens.Add(new ScannerToken(TokenType.ASSIGN,1,2));
            tokens.Add(new ScannerToken(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(tokens);
            streamToken.Advance();

            Assert.AreEqual(streamToken.Peek().Type,TokenType.ASSIGN);
        }
        
        [Test]
        public void Test_Stream_Advance_type()
        {
            List<ScannerToken> tokens = new List<ScannerToken>();
            tokens.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            tokens.Add(new ScannerToken(TokenType.ASSIGN,1,2));
            tokens.Add(new ScannerToken(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(tokens);
            streamToken.Advance();
            streamToken.Advance();
            Assert.AreEqual(streamToken.Peek().Type,TokenType.NUMERIC_INT);
        }
    }
}