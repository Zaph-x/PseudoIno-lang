using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Objects;
using Lexer;
using static Lexer.Objects.TokenType;
using NUnit.Framework;
using System.IO;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Parser;
using AbstractSyntaxTree.Objects.Nodes;



namespace AbstractSyntaxTree.Tests
{
    class VisitorTest 
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
@"
func foo with c, d
    begin while c less (6 + 5) do
        c is c + 1
    end while
    begin for x in 1..21 do
        x is 21
    end for
    return 3
end foo
a is on
b is 4
c is 4
dpin4 is b less (4 + (3 + 5))

if a equal b do
    if b equal c do
        a is false
    end if
else if b equal c do
    b is 4
else
    c is 4
end if
d is c less 4
call foo with 23, 2
f is call foo with 23, 2
func loop
    a is on
    call foo with 3
    wait 4s
    a is off
end loop
";
        private const string content3 =
@"
func loop
    wait 4s
    wait 4m
    wait 4h
    wait 4ms
end loop
";
        private const string content4 =
@"
func loop
  a is 2
  b is 3
 a is  a + b
  a is a-b
if( a or b) do
 d is b%a
c is ""test1222!""
e is a/d
end if 
if( a > b and b >=a or b<=a) do
#do nothing
apin1 is on
end if  
#a is [2]
#f is a@1
end loop
 ";

        string nowhere;

        [SetUp]
        public void TestInit()
        {
            Parser.Parser.HasError = false;
        }

        [TestCase(content2)]
        [TestCase(content3)]
        [TestCase(content4)]
        public void Test_ASTHelper_Assign_2(string test)
        {
            StreamReader FakeReader = CreateFakeReader(test, Encoding.UTF8);
            Tokeniser tokeniser = new Tokeniser(FakeReader);
            tokeniser.GenerateTokens();
            List<ScannerToken> tokens = tokeniser.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out nowhere);
            if (Parser.Parser.HasError)
                Assert.Fail();
            parser.Root.Accept(new PrettyPrinter());
        }

        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}
