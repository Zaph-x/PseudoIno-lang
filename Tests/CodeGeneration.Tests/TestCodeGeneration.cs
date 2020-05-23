using System;
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
g is 4

func trigger with a,b,c
  dpin13 is on
  wait 1s
  dpin13 is off
  wait 1s
  a is on
  b is 3.3
  c is 1
  return b
end trigger

func loop
  a is on
  b is 3.3
  c is 1
  call trigger with a, b, c
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

        //If statment test
        private const string Ifstatment =
        @"# Builtin led is on digital pin 13

func trigger with a, b, c
    brightness is 0
    amountToAdd is 5
    if (brightness less or equal 0) or (brightness greater or equal 255) do
        amountToAdd is amountToAdd * -1
    else if (brightness equal 0) do
        amountToAdd is 1
    else 
        amountToAdd is 5
    end if
end trigger

func loop
    a is on
    b is 3.3
    c is 1
    call trigger with a, b, c
end loop";

        //blink program til arduino
        private const string blink =
       @"#Builtin led is on digital pin 13
func blink
    dpin13 is on
    wait 1s
    dpin13 is off
    wait 1s
end blink

func loop
    call blink
end loop";

        //kan k�re
        private const string whilestatment =
@"#while statement test
func trigger with g
    g is 4
    z is on
    begin while z  do    
        g is g +1
        z is off
    end while 

end trigger

func loop
g is 0
  call trigger with g
end loop";

        private const string whilestatment2 =
              @"#while statement test


func trigger with g
    g is 4
    z is 8
    begin while z less 9  do    
        g is g +1
        z is 9
    end while 
    z is 10

    #while test 2
    begin while z greater 9  do    
        g is g +1
        z is 9
    end while  
    y is 10

    #test while bool
    x is on
    begin while x do    
        x is off
    end while
end trigger
func loop
    g is 0
    call trigger with g
end loop";

        private const string whilestatment3 =
      @"#while statement test
func trigger with g
    g is 4
    z is 8
    begin while (z less or equal 9) or (z less 9) do    
        g is g +1
        z is 9
    end while  

end trigger

func loop
    g is 0
  call trigger with g
end loop";


        private const string forstatmenttest =
      @"#for statement test
func trigger with g
  g is 4
  begin for x in 1..5 do
    g is x
  end for
end trigger

func loop
  g is 0
  call trigger with g
end loop";
        const string time =
@"wait 1ms
wait 1s
wait 2m
wait 3h
func loop
end loop";
        const string stringTest =
@"a is ""Test string""
func loop
end loop";

        const string program4 =
@"a is 4 less or equal 4
b is 6 greater or equal 1
func loop
end loop";

        const string program5 =
@"a is 4 % 2 % 1
b is 3 - 1 - 1
func loop
end loop";

        string dbg;

        [SetUp]
        public void TestInit()
        {
            CodeGenerationVisitor.HasError = false;
            Parsenizer.HasError=false;
            TypeChecker.HasError = false;
            dbg = "";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            File.Delete(AppContext.BaseDirectory + "Codegen_output.cpp");
        }

        [TestCase(content)]
        [TestCase(content2)]
        [TestCase(blink)]
        [TestCase(forstatmenttest)]
        [TestCase(Ifstatment)]
        [TestCase(whilestatment)]
        [TestCase(whilestatment2)]
        [TestCase(whilestatment3)]
        [TestCase(time)]
        [TestCase(stringTest)]
        [TestCase(program4)]
        [TestCase(program5)]
        public void Test_CodeGenVisitor_content(string prog)
        {
            StreamReader FakeReader = CreateFakeReader(prog, Encoding.UTF8);
            Tokeniser tokenizer = new Tokeniser(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parsenizer parser = new Parsenizer(tokens);
            parser.Parse(out dbg);
            if (Parsenizer.HasError)
                Assert.Fail("The parser encountered an error\n\n"+dbg);
            parser.Root.Accept(new TypeChecker());
            Assert.IsFalse(TypeChecker.HasError, "Typechecker visitor encountered an error");
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
            Assert.IsFalse(CodeGenerationVisitor.HasError, "Code gen visitor encountered an error");
        }
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}
