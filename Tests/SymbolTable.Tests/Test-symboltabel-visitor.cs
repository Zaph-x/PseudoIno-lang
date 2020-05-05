using AbstractSyntaxTree.Objects;
using Lexer;
using Lexer.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SymbolTable;
using Parser;

namespace SymbolTable.Tests
{
    class Test_symboltabel_visitor
    {

        // private const string content = @"# This is a dummy program to test the token generator
        //     #< This multiline comment
        //     should also be accepted >#
        //     a is 4
        //     b is 6
        //     c is a + b
        //     if b > a do

        //     end if
        //     func test with x, y, z
        //         d is 10
        //     end test";

        private const string content2 =
@"call a
a is on
b is 4
func loop
    a is a + 1
    call foo with 3
    wait 4s
    a is a + 1
end loop
dpin4 is b and (4 or (a less (3 + 5)))

if a equal b do
    if b equal c do
        a is 4
    end if
else if b equal c do
    b is 4
else
    c is 4
end if
d is c less 4
call foo
f is call foo with 23

func foo with c, d
    begin while c less (6 + 5) do
        c is c + 1
    end while
    begin for x in 1..21 do
        x is 21
    end for
    return 3
end foo";

        string nowhere;

        [SetUp]
        public void TestInit()
        {
            Parsenizer.HasError = false;
        }

        [Test]
        public void Test_SymboltableVisitor()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            // PrettyPrinter printer = new PrettyPrinter();
            //printer.Visit(parser.Root);
            parser.Root.Accept(new Symboltablevisitor());

            // FIXME Denne linje giver Stack Overflow Exception
            //base.Visit(parser.Root);
        }

        [Test]
        public void Test_SymboltableVisitor_main()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual("main",symboltablevisitor._symbolTableBuilder.FinalSymbolTable[0][0].Name); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_prog()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual(TokenType.PROG,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[0][0].Type); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_1_count()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual(1,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[0].Count); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_2_count()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual(5,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[1].Count); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_3_count()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual(3,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[2].Count); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_3_1_name()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual("while",symboltablevisitor._symbolTableBuilder.FinalSymbolTable[2][0].Name); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_3_2_name()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual("for",symboltablevisitor._symbolTableBuilder.FinalSymbolTable[2][1].Name); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_3_3_name()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual("if",symboltablevisitor._symbolTableBuilder.FinalSymbolTable[2][2].Name); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_Depth_1_symbols()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            Assert.AreEqual(7,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[0][0].Symbols.Count); 
        }
        
        [Test]
        public void Test_SymboltableVisitor_ScopeCheck()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            ScopeCheck scopeCheck = new ScopeCheck(symboltablevisitor._symbolTableBuilder);
            Assert.AreEqual(7,symboltablevisitor._symbolTableBuilder.FinalSymbolTable[0][0].Symbols.Count); 
        }

        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}

