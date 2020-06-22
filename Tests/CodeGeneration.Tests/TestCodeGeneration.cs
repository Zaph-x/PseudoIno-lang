using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AbstractSyntaxTree.Objects.Nodes;
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
        
private const string Array_Declaration = @"
a is [2]
func loop
	b is true
end loop
";
private const string Array_Assignment = @"
a is [2]
a@0 is -10
func loop
	b is true
end loop
";
private const string Array_Access = @"
a is [2]
a@0 is -10
func loop
	b is a@0
end loop
";

private const string func_float =
    @"
func foo
  b is 4.2
  return b
end foo

func loop
  a is 5.1
  a is call foo
end loop";

private const string func_string =
    "\n func foo \n b is \"HelloWorld\" \n return b \n end foo \n func loop \n a is call foo \n end loop";

private const string func_bool =
    @"
func foo
  b is true
  return b
end foo

func loop
  a is false
  a is call foo
end loop";

private const string call_2_param =
    @"
func foo with c, f
  b is c + f
  return b
end foo

func loop
  a is call foo with 2.1, 5.8
end loop";

private const string call_2_param_string =
    "func foo with c, f \n b is c \n return b \n end foo \n func loop \n a is call foo with \"hello\", \"world\" \n end loop";

private const string call_2_param_divide =
    @"
func foo with c, f
  b is c / f
  return b
end foo

func loop
  a is call foo with 2.1, 5.8
end loop";

private const string call_2_param_and =
    @"
func foo with c, f
  b is c && f
  return b
end foo

func loop
  a is call foo with true, false
end loop";

private const string pwm =
    @"
apin9 is 0
apin9 is 128
func loop

end loop";

private const string pin_fail =
    @"
dpin9 is 0
a is dpin9
func loop

end loop";

string dbg;

        [SetUp]
        public void TestInit()
        {
            CodeGenerationVisitor.HasError = false;
            Parser.Parser.HasError = false;
            TypeChecker.HasError = false;
            SymbolTableObject.FunctionCalls = new List<CallNode>();
            SymbolTableObject.FunctionDefinitions = new List<FuncNode>();
            SymbolTableObject.PredefinedFunctions = new List<FuncNode>();
            dbg = "";
        }

        [TearDown]
        public void TearDown()
        {
            CodeGenerationVisitor.HasError = false;
            Parser.Parser.HasError = false;
            TypeChecker.HasError = false;
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            File.Delete(AppContext.BaseDirectory + "Codegen_output.cpp");
        }

        [TestCase(0,content)]
        [TestCase(1,content2)]
        [TestCase(2,blink)]
        [TestCase(3,forstatmenttest)]
        [TestCase(4,Ifstatment)]
        [TestCase(5,whilestatment)]
        [TestCase(6,whilestatment2)]
        [TestCase(7,whilestatment3)]
        [TestCase(8,time)]
        [TestCase(9,stringTest)]
        [TestCase(10,program4)]
        [TestCase(11,program5)]
        //[TestCase(12,Array_Declaration)]
        [TestCase(13,Array_Assignment)]
        [TestCase(14,Array_Access)]
        [TestCase(15,func_float)]
        [TestCase(16,func_string)]
        [TestCase(17,func_bool)]
        [TestCase(18,call_2_param)]
        [TestCase(19,call_2_param_divide)]
        [TestCase(20,call_2_param_and)]
        //[TestCase(21,call_2_param_string)]
        [TestCase(22,pwm)]
        public void Test_CodeGenVisitor_content(int n, string prog)
        {
            StreamReader FakeReader = CreateFakeReader(prog, Encoding.UTF8);
            Tokeniser tokeniser = new Tokeniser(FakeReader);
            tokeniser.GenerateTokens();
            List<ScannerToken> tokens = tokeniser.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out dbg);
            if (Parser.Parser.HasError)
                Assert.Fail("The parser encountered an error\n\n" + dbg);
            parser.Root.Accept(new TypeChecker(new List<string>() {"3","5","6","9","10","11"}));
            Assert.IsFalse(TypeChecker.HasError, "Typechecker visitor encountered an error");
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp", new List<string>());
            parser.Root.Accept(codeGenerationVisitor);
            Assert.IsFalse(CodeGenerationVisitor.HasError, "Code gen visitor encountered an error");
        }
        
        [Test]
        public void Test_CodeGenVisitor_content_fail()
        {
            StreamReader FakeReader = CreateFakeReader(Array_Declaration, Encoding.UTF8);
            Tokeniser tokeniser = new Tokeniser(FakeReader);
            tokeniser.GenerateTokens();
            List<ScannerToken> tokens = tokeniser.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out dbg);
            if (Parser.Parser.HasError)
                Assert.Fail("The parser encountered an error\n\n" + dbg);
            parser.Root.Accept(new TypeChecker(new List<string>() {"3","5","6","9","10","11"}));
            Assert.IsTrue(TypeChecker.HasError, "Typechecker visitor encountered an error");
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp", new List<string>());
            parser.Root.Accept(codeGenerationVisitor);
            Assert.IsFalse(CodeGenerationVisitor.HasError, "Code gen visitor encountered an error");
        }
        
        [Test]
        public void Test_CodeGenVisitor_pin_fail()
        {
            StreamReader FakeReader = CreateFakeReader(pin_fail, Encoding.UTF8);
            Tokeniser tokeniser = new Tokeniser(FakeReader);
            tokeniser.GenerateTokens();
            List<ScannerToken> tokens = tokeniser.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out dbg);
            if (Parser.Parser.HasError)
                Assert.Fail("The parser encountered an error\n\n" + dbg);
            parser.Root.Accept(new TypeChecker(new List<string>() {"3","5","6","9","10","11"}));
            Assert.IsFalse(TypeChecker.HasError, "Typechecker visitor encountered an error");
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp", new List<string>());
            parser.Root.Accept(codeGenerationVisitor);
            Assert.IsTrue(CodeGenerationVisitor.HasError, "Code gen visitor encountered an error");
        }
        
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
}
