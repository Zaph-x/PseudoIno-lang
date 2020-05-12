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
            @"#Builtin led is on digital pin 13


func trigger
  dpin13 is on
  wait 1s
  dpin13 is off
  wait 1s
a is 3
  return a
end trigger

func loop
  call trigger
end loop";

        private const string content2 =
            @"brightness is 0
amountToAdd is 5

func loop
  apin9 is brightness + amountToAdd
  
  if (brightness less or equal 0) or (brightness greater or equal 255) do
    amountToAdd is amountToAdd * -1
  end if
  wait 30ms
wait 30m
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
     
            parser.Root.Accept(new TypeChecker());
            parser.Root.Accept(codeGenerationVisitor);
        }
        
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}