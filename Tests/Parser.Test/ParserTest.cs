using System.Collections.Generic;
using Lexer.Objects;
using NUnit.Framework;

namespace Parser.Test
{
    public class ParserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ParseTable_Apply()
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.LINEBREAK,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
    }
}