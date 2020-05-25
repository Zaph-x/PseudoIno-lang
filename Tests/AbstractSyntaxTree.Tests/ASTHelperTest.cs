using System.Collections.Generic;
using Lexer.Objects;
using Lexer;
using static Lexer.Objects.TokenType;
using NUnit.Framework;
using System.IO;
using System.Text;
using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Parser;

namespace AbstractSyntaxTree.Tests
{
    public class ASTHelperTest
    {
        string nowhere;

        private const string content = @"# This is a dummy program to test the token generator
            #< This multiline comment
            should also be accepted >#
            a is 4
            b is 6
            c is a + b
            if b > a do
                
            end if
            func test with x, y, z
                d is 10
            end test";

        private const string content2 = @"
        a is 0
        func loop
            a is a + 1
            call foo with 3, 3
            wait 4s
        end loop
        dpin4 is 4 or a and 3 or 5

        if a equal b do
            a is 4
        else if b equal c do
            b is 4
        else
            c is 4
        end if

        func foo with c
            begin while c less 6 do
                c is c + 1
            end while
            begin for x in 1..21 do
                x is 21
            end for
        end foo";

        [SetUp]
        public void TestInit()
        {
            Parser.Parser.HasError = false;
        }

        [TestCase(content)]
        [TestCase(content2)]
        public void Test_ASTHelper_Constructor(string content)
        {
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);
            Tokeniser tokenizer = new Tokeniser(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out nowhere);
            Assert.IsFalse(Parser.Parser.HasError, "The parser encountered an error");
        }

        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}