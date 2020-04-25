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
        private const string content = @"# This is a dummy program to test the token generator
            #< This multiline comment
            should also be accepted >#
            a is 4
            b is 6
            c is a + b
            if b > a
                
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

       

        string nowhere;
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
        [Test]
        public void Test_ASTHelper_Assign_2()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(tokens, out nowhere);
            ASTHelper helper = new ASTHelper(tokenizer.Tokens.ToList());
            //pretty printer
            helper.Root.Accept(new PrettyPrinterVisitor());
            

        }
        //public new void Visit(ProgramNode node)
        //{
        //    Console.WriteLine( "Jeg er i roden nu ");

        //}
    }
}
