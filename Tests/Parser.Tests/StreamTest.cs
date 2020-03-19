using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;
using Parser.Objects;

namespace Parser.Tests
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
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.VAR,"a",1,1));
            tokens.Add(new Token(TokenType.ASSIGN,1,2));
            tokens.Add(new Token(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(tokens);
            
            Assert.AreEqual(streamToken.Current().Type,TokenType.VAR);
        }
        
        [Test]
        public void Test_Stream_Peek_type()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.VAR,"a",1,1));
            tokens.Add(new Token(TokenType.ASSIGN,1,2));
            tokens.Add(new Token(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(tokens);
            
            Assert.AreEqual(streamToken.Peek().Type,TokenType.ASSIGN);
        }
        
        [Test]
        public void Test_Stream_Advance_type()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.VAR,"a",1,1));
            tokens.Add(new Token(TokenType.ASSIGN,1,2));
            tokens.Add(new Token(TokenType.NUMERIC_INT,"5",1,2));

            TokenStream streamToken = new TokenStream(tokens);
            streamToken.Advance();
            Assert.AreEqual(streamToken.Peek().Type,TokenType.NUMERIC_INT);
        }
    }
}