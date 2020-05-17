using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Contextual_analysis;
using Lexer;
using Lexer.Objects;
using Parser;

namespace CodeGeneration.Tests
{
    class Test_Constructs
    {
        #region Test program in our language 
        //Bar minimum program 
        private const string BarMinimum =
@"
#BarMinimum
func loop
#write youre code to loop forever here
end loop";
        //blink program til arduino
        private const string Blink =@"
       #Builtin led is on digital pin 13
            func blink
              dpin13 is on
              wait 1s
              dpin13 is off
              wait 1s
            end blink

            func loop
             call blink
            end loop";

        //Fade program analog pin
        private const string Fade =
@"
brightness is 0
amountToAdd is 5

func loop
  apin1 is brightness
  brightness is amountToAdd + brightness 
  
  if (brightness less or equal 0) or (brightness greater or equal 255) do
    amountToAdd is amountToAdd * -1
  end if
  wait 30ms
end loop";
        //Blink2 mega med for loop til arduino uno analog pin 0 - 5
        private const string Blink2 = @"
func loop
    begin for brightness in 0..255 do
        apin0 is brightness
        apin1 is brightness
        apin2 is brightness
	    apin3 is brightness
	    apin4 is brightness
	    apin5 is brightness
	wait 2ms
    end for

    begin for brightness in 255..0 do
	     apin0 is brightness
	     apin1 is brightness
	     apin2 is brightness
	     apin3 is brightness
	     apin4 is brightness
	     apin5 is brightness    
    wait 2ms
    end for
wait 100ms
end loop
";
        

        //Fade med If statment else else if 
        private const string FadeIfstatment =
        @"
func trigger with brightness
    amountToAdd is 5
    if (brightness less or equal 0) or (brightness greater or equal 255) do
        amountToAdd is amountToAdd* -1
    else if (brightness equal 0) do
        amountToAdd is 1
    else 
        amountToAdd is 5
    end if
    brightness is amountToAdd + brightness 
    apin1 is brightness 
    wait 1s
end trigger

func loop
    brightness is 0
    call trigger with brightness 
end loop
";

        //blink program med while loop til arduino
        private const string Blinkwhile = @"
            
func blinkWhile
	dpin13 is on
	i is 4
	d is i % 2
	begin while d equal 0  do 
	dpin13 is off
	end while
end blink

func loop
	call blinkWhile
end loop
";



        //kan køre
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
begin while x  do    
x is off
end while
end trigger
func loop
g is 0
  call trigger with g
end loop";

        //kan ikke køre 
        private const string whilestatment3 =
      @"#while statement test
func trigger with g
g is 4
z is 8
begin while (z less or equal 9) or (z is less 9)  do    
g is g +1
z is 9
end while 
z is 10
begin while z greater 9  do    
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
        #endregion
        string nowhere;

        [SetUp]
        public void TestInit()
        {

        }

               #region Language constructs 
        [Test]
        public void Test_Bar_minimum()
        {
            StreamReader FakeReader = CreateFakeReader(BarMinimum, Encoding.UTF8);
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
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
        }
        [Test]
        public void Test_Blink()
        {
            StreamReader FakeReader = CreateFakeReader(Blink, Encoding.UTF8);
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
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
        }
        [Test]
        public void Test_Fade()
        {
            StreamReader FakeReader = CreateFakeReader(Fade, Encoding.UTF8);
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
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
        }
        [Test]
        public void Test_Blink2()
        {
            StreamReader FakeReader = CreateFakeReader(Blink2, Encoding.UTF8);
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
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
        }
        //[Test]
        //public void Test_Fadeif()
        //{
        //    StreamReader FakeReader = CreateFakeReader(FadeIfstatment, Encoding.UTF8);
        //    Tokenizer tokenizer = new Tokenizer(FakeReader);
        //    tokenizer.GenerateTokens();
        //    List<ScannerToken> tokens = tokenizer.Tokens.ToList();
        //    Parsenizer parser = new Parsenizer(tokens);
        //    parser.Parse(out nowhere);
        //    if (Parsenizer.HasError)
        //        Assert.Fail();
        //    /*Symboltablevisitor symboltablevisitor = new Symboltablevisitor();
        //    parser.Root.Accept(symboltablevisitor);*/
        //    //TypeChecker typeChecker = new TypeChecker();
        //    parser.Root.Accept(new TypeChecker());
        //    CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
        //    parser.Root.Accept(codeGenerationVisitor);
        //}
        [Test]
        public void Test_BlinkWhile()
        {
            StreamReader FakeReader = CreateFakeReader(Blinkwhile, Encoding.UTF8);
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
            CodeGenerationVisitor codeGenerationVisitor = new CodeGenerationVisitor("Codegen_output.cpp");
            parser.Root.Accept(codeGenerationVisitor);
        }
        public StreamReader CreateFakeReader(string content, Encoding enc)
        {
            byte[] fakeBytes = enc.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), enc, false);
        }
    }
    #endregion
}
