using NUnit.Framework;
using Lexer.Objects;
using SymbolTable;
using System.Collections.Generic;
using System;

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
            
            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.WHILE);
            Root.AddNode("forloop",  TokenType.FOR);   
            string name = "forloop";
            //TokenType type = TokenType.FOR;
            Assert.AreEqual(name, Root.ChildrenList[0].Parent.name);
         
        }
        [Test]
//        public void AddChildrenDuplicateTest()
//        {
//            
//                NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.WHILE);
//                Root.AddNode("forloop", TokenType.FOR);
//                Root.AddNode("forloop", TokenType.FOR);
//        
//}
        [Test]
        public void AddParentTest()
        {
            

            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.WHILE);
            Root.AddNode("forloop", TokenType.FOR);
            string Parent = "Whileloop";
            
            Assert.AreEqual(Parent, Root.ChildrenList[0].Parent.name);
           

        }
    }
}