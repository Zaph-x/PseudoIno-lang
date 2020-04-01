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
            Assert.AreEqual(name, Root.ChildrenList[0].name);
         
        }
        //Test af at man ikke kan tilføje dubletter til listen childrenlist
       // [Test]
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

        [Test]
        public void FindChildTest()
        {

            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.FUNCTION);
            Root.AddNode("forloop", TokenType.FOR);
            Root.AddNode("whileloop", TokenType.WHILE);
            Root.AddNode("IFSTMNT", TokenType.IFSTMNT);
            Root.AddNode("PIN", TokenType.PIN);
            string name = "PIN";
            NodeSymbolTab Pin = new NodeSymbolTab("PIN", TokenType.PIN);
            //TokenType type = TokenType.FOR;
          
        Assert.AreEqual(Pin.name, Root.Findnode(name).name);
            Assert.AreEqual(Pin.type, Root.Findnode(name).type);
        }
    }
}