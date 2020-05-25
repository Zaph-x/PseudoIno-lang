using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Contextual_analysis;
using Lexer;
using Lexer.Objects;
using Parser;
using System;

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
        private const string Blink = @"
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
        private const string FadeIfstatment = @"  func trigger with brightness
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
end loop";
        //        @"
        //func trigger with brightness
        //    amountToAdd is 5
        //    if (brightness == 0)  do
        //        amountToAdd is amountToAdd* -1
        //    else if (brightness > 0) do
        //        amountToAdd is 1

        //    else 
        //        amountToAdd is 5
        //end if
        //    end if
        //    brightness is amountToAdd + brightness 
        //    apin1 is brightness 
        //    wait 1s
        //end trigger

        //func loop
        //    brightness is 0
        //    call trigger with brightness 
        //end loop
        //";


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


        #endregion
        string nowhere;
     
        [SetUp]
        public void TestInit()
        { }

        [OneTimeTearDown]
        public void TearDown()
        {
            File.Delete(AppContext.BaseDirectory + "Codegen_output.cpp");
        }

        #region Language constructs tests
        //Bar minimum program 
        [TestCase(BarMinimum)]
        //blink program til arduino
        [TestCase(Blink)]
        //Blink2 mega med for loop til arduino uno analog pin 0 - 5
        [TestCase(Blink2)]
        //Fade program analog pin
        [TestCase(Fade)]
        //Fade med If statment else else if 
        [TestCase(FadeIfstatment)]
        [TestCase(Blinkwhile)]
        public void Test_Program(string program)
        {
            StreamReader FakeReader = CreateFakeReader(program, Encoding.UTF8);
            Tokeniser tokenizer = new Tokeniser(FakeReader);
            tokenizer.GenerateTokens();
            List<ScannerToken> tokens = tokenizer.Tokens.ToList();
            Parser.Parser parser = new Parser.Parser(tokens);
            parser.Parse(out nowhere);
            if (Parser.Parser.HasError)
                Assert.Fail();
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
