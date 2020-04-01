using NUnit.Framework;
using Lexer.Objects;
using SymbolTable;
using System.Collections.Generic;

namespace SymbolTable.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddChildrenTest()
        {
            //Input tilføj nodesymbol
            //output skal passe med nodesymbol
            
            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.WHILE);
            Root.AddNode("forloop",  TokenType.FOR);
            string name = "forloop";
            TokenType type = TokenType.FOR;
            Assert.AreEqual(name, Root.ChildrenList[0].name);
            //arrange
            //act
            //assert

        }
    }
}