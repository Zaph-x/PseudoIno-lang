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
            func test with (numeric x, numeric y, numeric z)
                d is 10
                x + y equals z?
            end test";

        [Test]
        public void Test_ASTHelper_Constructor()
        {
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            ASTHelper helper = new ASTHelper(tokenizer.Tokens);
        }

        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }

        [Test]
        public void Test_ASTHelper_Assign()
        {
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            ASTHelper helper = new ASTHelper(tokenizer.Tokens);
        }
    }
}