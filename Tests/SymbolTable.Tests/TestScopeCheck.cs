using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lexer;
using Lexer.Objects;
using NUnit.Framework;
using Parser;

namespace SymbolTable.Tests
{
    public class TestScopeCheck
    {
        private const string content3 =
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
f is call foo with 23";
        
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
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            ScopeCheck scopeCheck = new ScopeCheck(symboltablevisitor._symbolTableBuilder);
        }
        
        [Test]
        public void Test_SymboltableVisitor_2()
        {
            StreamReader FakeReader = CreateFakeReader(content3, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);
            try
            {
                ScopeCheck scopeCheck = new ScopeCheck(symboltablevisitor._symbolTableBuilder);
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}