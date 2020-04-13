using Lexer.Objects;
using NUnit.Framework;
using Parser.Objects;

namespace Parser.Tests
{
    class ParseTableCopyTest
    {
        [Test]
        public void Test_Constructor_CanFillTable()
        {
            ParseTableCopy parseTable = new ParseTableCopy();

            Assert.IsNotNull(parseTable[TokenType.PROG, TokenType.VAR]);
        }

        [Test]
        public void Test_Access_ShouldReturnErrorActionIfInvalidSyntax()
        {
            ParseTableCopy parseTable = new ParseTableCopy();

            Assert.AreEqual("Type=Error", parseTable[TokenType.STMNT, TokenType.NUMERIC].ToString());
        }
    }
}