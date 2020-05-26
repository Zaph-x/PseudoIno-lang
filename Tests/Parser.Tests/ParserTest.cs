using System;
using System.Collections.Generic;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
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
            Parser.HasError = false;
        }

        [Test]
        public void Test_Parse_CanParseSimpleProgram()
        {
            List<ScannerToken> list = CreateList(CALL, VAR, WITH, VAR);

            Parser parser = new Parser(list);
            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, NUMERIC);

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment_With_Expr() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, NUMERIC, OP_PLUS, NUMERIC);

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_Assignment_Array() // - done
        {
            List<ScannerToken> list = CreateList(VAR, ASSIGN, ARRAYLEFT, NUMERIC, ARRAYRIGHT);

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_While() // - done
        {
            List<ScannerToken> list = CreateList(BEGIN, WHILE, NUMERIC, OP_GREATER, NUMERIC, DO, VAR, ASSIGN, NUMERIC, END, WHILE);

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_For() // - done
        {
            List<ScannerToken> list = CreateList(BEGIN, FOR, VAR, IN, NUMERIC, OP_RANGE, NUMERIC, DO, VAR, ASSIGN, NUMERIC, END, FOR);

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
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
            list.Add(new ScannerToken(TokenType.NUMERIC, "2", 1, 7));
            //list.Add(new ScannerToken(TokenType.OP_RPAREN, 1, 5));
            //list.Add(new ScannerToken(TokenType.NEWLINE, "", 1, 11));
            //list.Add(new ScannerToken(TokenType.EOF, "", 1, 7));

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);


            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_LeftHand_Set() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.LeftHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_LeftHand_Get() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.LeftHand));
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_Operator_Set() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.Operator = new PlusNode(new ScannerToken(OP_PLUS,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_Operator_Get() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.Operator));
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_RightHand_Set() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.RightHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Call_NotImplementedException_RightHand_Get() // - 
        {
            CallNode node = new CallNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.RightHand));
        }

        [Test]
        public void Test_ParseTable_If() // - done
        {
            List<ScannerToken> list = new List<ScannerToken>();
            list.Add(new ScannerToken(TokenType.IF, 1, 3));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.OP_GREATER, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.NUMERIC, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.DO, "5", 1, 5));
            list.Add(new ScannerToken(TokenType.END, "5", 2, 5));
            list.Add(new ScannerToken(TokenType.IF, "5", 2, 5));

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }

        [Test]
        public void Test_ParseTable_If_With_Statement() // - not working
        {
            List<ScannerToken> list = new List<ScannerToken>();
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

            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Array_Assignment() // - 
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_DoubleArray_Assignment() // - 
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT,ARRAYLEFT,NUMERIC,ARRAYRIGHT);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.True(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Assign_To_Array() // - 
        {
            List<ScannerToken> list = CreateList(ARRAYINDEX,NUMERIC,ASSIGN,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.True(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Assign_To_Array_2() // - 
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT,ARRAYINDEX,NUMERIC,ASSIGN,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_LeftHand_Set() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.LeftHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_LeftHand_Get() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.LeftHand));
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_Operator_Set() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.Operator = new PlusNode(new ScannerToken(OP_PLUS,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_Operator_Get() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.Operator));
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_RightHand_Set() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() => node.RightHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_Array_NotImplementedException_RightHand_Get() // - 
        {
            ArrayNode node = new ArrayNode(1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.RightHand));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_LeftHand_Set() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() => node.LeftHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_LeftHand_Get() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.LeftHand));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_Operator_Set() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() => node.Operator = new PlusNode(new ScannerToken(OP_PLUS,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_Operator_Get() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.Operator));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_RightHand_Set() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() => node.RightHand = new ExpressionTerm(new ScannerToken(NUMERIC,1,1)));
        }
        
        [Test]
        public void Test_ParseTable_ArrayAccess_NotImplementedException_RightHand_Get() // - 
        {
            ArrayAccessNode node = new ArrayAccessNode(new ArrayNode(1,1), 1,1);
            Assert.Throws<NotImplementedException>(() =>  Console.WriteLine(node.RightHand));
        }
        
        [Test]
        public void Test_ParseTable_Array()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT,ARRAYINDEX,NUMERIC,ASSIGN,NUMERIC,OP_PLUS,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Array_210()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT,ARRAYINDEX,NUMERIC,ASSIGN,NUMERIC,OP_PLUS,NUMERIC,VAR,ASSIGN,ARRAYINDEX,NUMERIC,OP_PLUS,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Apin_Assign()
        {
            List<ScannerToken> list = CreateList(APIN,ASSIGN,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Apin_109_1()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,APIN);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Apin_109_2()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,APIN,OP_PLUS,NUMERIC);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Apin_109_3()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,OP_LPAREN,APIN,OP_PLUS,NUMERIC,OP_RPAREN);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Apin_109_4()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,APIN,OP_PLUS,OP_LPAREN,NUMERIC,OP_PLUS,NUMERIC,OP_RPAREN);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_Call_Paren()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,OP_LPAREN,OP_LPAREN,CALL,VAR,OP_RPAREN,OP_PLUS,NUMERIC,OP_RPAREN);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_90037()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,OP_LPAREN,OP_LPAREN,NUMERIC,OP_MODULO,NUMERIC,OP_RPAREN,OP_PLUS,NUMERIC,OP_RPAREN);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_return_expr()
        {
            List<ScannerToken> list = CreateList(FUNC,VAR,RETURN,NUMERIC,OP_TIMES,NUMERIC,END,VAR);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_return_expr_paren()
        {
            List<ScannerToken> list = CreateList(FUNC,VAR,RETURN,OP_LPAREN,NUMERIC,OP_TIMES,NUMERIC,OP_RPAREN,END,VAR);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_while_expr()
        {
            List<ScannerToken> list = CreateList(BEGIN,WHILE,VAR,OP_LESS,OP_LPAREN,NUMERIC,OP_DIVIDE,NUMERIC,OP_RPAREN,DO,END, WHILE);
            list[5].Value = "5";
            list[7].Value = "5";
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_116()
        {
            List<ScannerToken> list = CreateList(FUNC,VAR,END,VAR,BEGIN,WHILE,BOOL,DO,END,WHILE);
            list[1].Value = " ";
            list[6].Value = "true";
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_BOOL_not_fail()
        {
            List<ScannerToken> list = CreateList(BEGIN,WHILE,BOOL,DO,END,WHILE);
            Parser parser = new Parser(list);

            Assert.Throws<FormatException>(() => parser.Parse(out nowhere)) ;

            //Assert.True(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_BOOL_expr()
        {
            List<ScannerToken> list = CreateList(BEGIN,WHILE,OP_LPAREN,BOOL,OP_AND,BOOL,OP_RPAREN,DO,END,WHILE);
            list[3].Value = "true";
            list[5].Value = "false";
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_BOOL_expr_long()
        {
            List<ScannerToken> list = CreateList(BEGIN,WHILE,OP_LPAREN,OP_LPAREN,BOOL,OP_AND,BOOL,OP_RPAREN,OP_OR,BOOL,OP_RPAREN,DO,END,WHILE);
            list[4].Value = "true";
            list[6].Value = "false";
            list[9].Value = "false";
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_array_outofrange()
        {
            List<ScannerToken> list = CreateList(VAR,ASSIGN,ARRAYLEFT,NUMERIC,ARRAYRIGHT,ARRAYINDEX,NUMERIC,ASSIGN,NUMERIC);
            list[3].Value = "2";
            list[6].Value = "5";
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.False(Parser.HasError);
        }
        
        [Test]
        public void Test_ParseTable_devision_by_zero_fail()
        {
            List<ScannerToken> list = CreateList(BEGIN,WHILE,VAR,OP_LESS,OP_LPAREN,NUMERIC,OP_DIVIDE,NUMERIC,OP_RPAREN,DO,END, WHILE);
            
            Parser parser = new Parser(list);

            parser.Parse(out nowhere);

            Assert.True(Parser.HasError);
        }

        private List<ScannerToken> CreateList(params TokenType[] tokens)
        {
            int i = 0;
            return tokens.Select(token => new ScannerToken(token, i.ToString(), i, i)).ToList();
        }
    }
}