using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Contextual_analysis;
using Lexer;
using Lexer.Objects;
using NUnit.Framework;
using Parser;
using SymbolTable;

namespace CodeGeneration.Tests
{
    public class TestCodeGeneration
    {
        private const string content =
            @"a is 0
func loop
    a is a + 1
    call foo with 3, 3
    wait 4s
end loop
dpin4 is (4 or (a and 3) or 5)

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

        private const string content2 =
            @"brightness is 0
amountToAdd is 5

func loop
  apin9 is brightness + amountToAdd
  
  if (brightness less or equal 0) or (brightness greater or equal 255) do
    amountToAdd is amountToAdd * -1
  end if
  wait 30ms
end loop";
        
        string nowhere;
        
        [SetUp]
        public void TestInit()
        {
            
        }

        [Test]
        public void Test_CodeGenVisitor_content()
        {
            StreamReader FakeReader = CreateFakeReader(content, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            /*Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
            parser.Root.Accept(symboltablevisitor);*/
            //TypeChecker typeChecker = new TypeChecker();
            parser.Root.Accept(new TypeChecker());
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor();
            parser.Root.Accept(codeGenerationVisitor);
        }
        
        [Test]
        public void Test_CodeGenVisitor_content2()
        {
            StreamReader FakeReader = CreateFakeReader(content2, Encoding.UTF8);
            Tokenizer tokenizer = new Tokenizer(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out nowhere);
            if (Parsenizer.HasError)
                Assert.Fail();
            parser.Root.Accept(new TypeChecker());
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor();
            parser.Root.Accept(codeGenerationVisitor);
        }
        
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}