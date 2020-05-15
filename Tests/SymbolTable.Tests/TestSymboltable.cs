//using NUnit.Framework;
//using Lexer.Objects;
//using Parser.Objects;
//using SymbolTable;
//using System.Collections.Generic;
//using System;

//namespace SymbolTable.Tests
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void AddChildrenTest()
//        {
            
//            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", new ParseToken(TokenType.WHILE,"", 0,0));
//            Root.AddNode("forloop", new ParseToken(TokenType.FOR,"",1,1));   
//            string name = "forloop";
//            //TokenType Type = TokenType.FOR;
//            Assert.AreEqual(name, Root.ChildrenList[0].Name);
         
//        }
//        //Test af at man ikke kan tilføje dubletter til listen childrenlist
//       // [Test]
//        //        public void AddChildrenDuplicateTest()
//        //        {
//        //            
//        //                NodeSymbolTab Root = new NodeSymbolTab("Whileloop", TokenType.WHILE);
//        //                Root.AddNode("forloop", TokenType.FOR);
//        //                Root.AddNode("forloop", TokenType.FOR);
//        //        
//        //}
//        [Test]
//        public void AddParentTest()
//        {
         
//            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", new ParseToken(TokenType.WHILE,"",0,0));
//            Root.AddNode("forloop", new ParseToken(TokenType.FOR,"",1,1));
//            string Parent = "Whileloop";
            
//            Assert.AreEqual(Parent, Root.ChildrenList[0].Parent.Name);
          
//        }

//        [Test]
//        public void FindChildTest()
//        {

//            NodeSymbolTab Root = new NodeSymbolTab("Whileloop", new ParseToken(TokenType.FUNCTION,"",0,0));
//            Root.AddNode("forloop", new ParseToken(TokenType.FOR,"",1,1));
//            Root.AddNode("whileloop", new ParseToken(TokenType.WHILE,"",2,2));
//            Root.AddNode("IFSTMNT", new ParseToken(TokenType.IFSTMNT,"",3,3));
//            Root.AddNode("PIN", new ParseToken(TokenType.PIN,"",4,4));
//            string name = "PIN";
//            string name1 = "forloop";
//            NodeSymbolTab Pin = new NodeSymbolTab("PIN", new ParseToken(TokenType.PIN,"",4,4));
//            //TokenType Type = TokenType.FOR;
//            Assert.AreEqual(name1, Root.ChildrenList[3].Findnode("forloop").Name);  
//        Assert.AreEqual(Pin.Name, Root.Findnode(name).Name);
//        Assert.AreEqual(Pin.Type, Root.Findnode(name).Type);
//        }
//    }
//}