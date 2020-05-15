using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
using static Lexer.Objects.TokenType;
using NUnit.Framework;

namespace Parser.Tests
{
    public class ParserTest
    {
        string nowhere;
        [SetUp]
        public void Setup()
        {
            nowhere = "";
            Parsenizer.HasError = false;
        }

        [Test]
        public void Test_Parse_CanParseSimpleProgram()
        {
            List<ScannerToken> list = CreateList(CALL, VAR, WITH, VAR);

            Parsenizer parsenizer = new Parsenizer(list);
            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, NUMERIC);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment_With_Expr() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, NUMERIC, OP_PLUS, NUMERIC);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment_Array() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, ARRAYLEFT, NUMERIC, ARRAYRIGHT);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment_Double_Array() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, ARRAYLEFT, NUMERIC, ARRAYRIGHT, ARRAYLEFT, NUMERIC, ARRAYRIGHT);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_While() // - done
        {
            List<ScannerToken> list = CreateList(BEGIN, WHILE, NUMERIC, OP_GREATER, NUMERIC, DO, VAR, ASSIGN, NUMERIC, END, WHILE);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_For() // - done
        {
            List<ScannerToken> list = CreateList(BEGIN, FOR, VAR, IN, NUMERIC, OP_RANGE, NUMERIC, DO, VAR, ASSIGN, NUMERIC, END, FOR);

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);

            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall_NOARGS() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, 1, 3));
            list.Add(new ScannerToken(TokenType.VAR, 1, 3));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 7));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall_WITHARGS_Numeric() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, "a", 1, 1));
            list.Add(new ScannerToken(TokenType.VAR, 1, 3));
            list.Add(new ScannerToken(TokenType.WITH, 1, 5));
            //list.Add(new ScannerToken(TokenType.OP_LPAREN, 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, 1, 7));
            //list.Add(new ScannerToken(TokenType.OP_RPAREN, 1, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 11));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall_WITHARGS_String() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, "a", 1, 1));
            list.Add(new ScannerToken(TokenType.VAR, 1, 3));
            list.Add(new ScannerToken(TokenType.WITH, 1, 5));
            //list.Add(new ScannerToken(TokenType.OP_LPAREN, 1, 5));
            list.Add(new ScannerToken(TokenType.STRING, 1, 7));
            //list.Add(new ScannerToken(TokenType.OP_RPAREN, 1, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 11));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall_WITHARGS_Bool() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, "a", 1, 1));
            list.Add(new ScannerToken(TokenType.VAR, 1, 3));
            list.Add(new ScannerToken(TokenType.WITH, 1, 5));
            //list.Add(new ScannerToken(TokenType.OP_LPAREN, 1, 5));
            list.Add(new ScannerToken(TokenType.BOOL, "true", 1, 7));
            //list.Add(new ScannerToken(TokenType.OP_RPAREN, 1, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 11));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, "", 1, 3));
            list.Add(new ScannerToken(TokenType.VAR, "5", 1, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 7));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_FuncCall_1() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.CALL,"a",1,1));
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.CALL, "", 1, 3));
            list.Add(new ScannerToken(TokenType.VAR, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.WITH, "5", 1, 5));
            //list.Add(new ScannerToken(TokenType.OP_LPAREN, 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.SEPARATOR, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.SEPARATOR, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            //list.Add(new ScannerToken(TokenType.OP_RPAREN, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.VAR, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.ASSIGN, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.VAR, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.OP_PLUS, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.VAR, "5", 2, 5));
            //list.Add(new ScannerToken(TokenType.END, "5", 3, 5));
            //list.Add(new ScannerToken(TokenType.VAR, "5", 3, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 7));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_If() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.BEGIN,"a",1,1));
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.IF, 1, 3));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.OP_GREATER, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.DO, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.END, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.IF, "5", 2, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 7));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        [Test]
        public void Test_ParseTable_If_With_Statement() // - not working
        {
            List<ScannerToken> list = new List<ScannerToken>();
            //list.Add(new ScannerToken(TokenType.BEGIN,"a",1,1));
            //list.Add(new ScannerToken(TokenType.PROG, "", 1, 1));
            list.Add(new ScannerToken(TokenType.IF, 1, 3));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.OP_GREATER, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.DO, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.VAR, "a", 2, 5));
            list.Add(new ScannerToken(TokenType.ASSIGN, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.END, "5", 3, 5));
            list.Add(new ScannerToken(TokenType.IF, "5", 3, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 7));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parsenizer parsenizer = new Parsenizer(list);

            parsenizer.Parse(out nowhere);


            Assert.False(Parsenizer.HasError);
        }

        private List<ScannerToken> CreateList(params TokenType[] tokens)
        {
            int i = 0;
            return tokens.Select(token => new ScannerToken(token, i.ToString(), i, i)).ToList();
        }
    }
}