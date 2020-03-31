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
        public void Test_ParseTable_Assignment() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_Assignment_With_Expr() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.OP_PLUS,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_Assignment_Array() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.ARRAYLEFT,"",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,7));
            list.Add(new ScannerToken(TokenType.ARRAYRIGHT,"",1,9));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,11));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_Assignment_Double_Array() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.ARRAYLEFT,"",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,7));
            list.Add(new ScannerToken(TokenType.ARRAYRIGHT,"",1,9));
            list.Add(new ScannerToken(TokenType.ARRAYLEFT,"",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,7));
            list.Add(new ScannerToken(TokenType.ARRAYRIGHT,"",1,9));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_While() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.BEGIN,"",1,1));
            list.Add(new ScannerToken(TokenType.WHILE,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.OP_GREATER,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"10",1,5));
            list.Add(new ScannerToken(TokenType.DO,"",1,5));
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"10",1,5));
            list.Add(new ScannerToken(TokenType.END,"",1,5));
            list.Add(new ScannerToken(TokenType.WHILE,"",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_For() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.BEGIN,"",1,1));
            list.Add(new ScannerToken(TokenType.FOR,1,3));
            list.Add(new ScannerToken(TokenType.VAR,"5",1,5));
            list.Add(new ScannerToken(TokenType.IN,"5",1,5));
            list.Add(new ScannerToken(TokenType.RANGE,"10",1,5));
            list.Add(new ScannerToken(TokenType.DO,"",1,5));
            list.Add(new ScannerToken(TokenType.VAR,"a",1,1));
            list.Add(new ScannerToken(TokenType.ASSIGN,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"10",1,5));
            list.Add(new ScannerToken(TokenType.END,"",1,5));
            list.Add(new ScannerToken(TokenType.FOR,"",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_FuncCall_NOARGS() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.FUNC,"a",1,1));
            list.Add(new ScannerToken(TokenType.VAR,1,3));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_FuncCall_WITHARGS_Numeric() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.FUNC,"a",1,1));
            list.Add(new ScannerToken(TokenType.VAR,1,3));
            list.Add(new ScannerToken(TokenType.WITH,1,5));
            list.Add(new ScannerToken(TokenType.NUMERIC,1,7));
            list.Add(new ScannerToken(TokenType.VAL,1,9));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,11));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_FuncCall_WITHARGS_String() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.FUNC,"a",1,1));
            list.Add(new ScannerToken(TokenType.VAR,1,3));
            list.Add(new ScannerToken(TokenType.WITH,1,5));
            list.Add(new ScannerToken(TokenType.STRING,1,7));
            list.Add(new ScannerToken(TokenType.VAL,1,9));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,11));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_FuncCall() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.CALL,"a",1,1));
            list.Add(new ScannerToken(TokenType.FUNC,"",1,3));
            list.Add(new ScannerToken(TokenType.VAR,"5",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
        
        [Test]
        public void Test_ParseTable_If() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.BEGIN,"a",1,1));
            list.Add(new ScannerToken(TokenType.IF,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.OP_GREATER,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.END,"5",1,5));
            list.Add(new ScannerToken(TokenType.IF,"5",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }

        [Test]
        public void Test_ParseTable_If_With_Statement() // - not working
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.BEGIN,"a",1,1));
            list.Add(new ScannerToken(TokenType.IF,1,3));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.OP_GREATER,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAR,"a",1,5));
            list.Add(new ScannerToken(TokenType.ASSIGN,"5",1,5));
            list.Add(new ScannerToken(TokenType.VAL,"5",1,5));
            list.Add(new ScannerToken(TokenType.END,"5",1,5));
            list.Add(new ScannerToken(TokenType.IF,"5",1,5));
            list.Add(new ScannerToken(TokenType.NEWLINE,"",1,7));
            
            Parsenizer parsenizer = new Parsenizer(list);
            parsenizer.CreateAndFillAST();
            
            Assert.Pass();
        }
    }
}