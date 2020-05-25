using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using Lexer;
using Parser;
using Contextual_analysis;
using System;
using SymbolTable;
using Lexer.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace ContextualAnalysis.Tests
{
    public class TypeCheckerTest
    {
        string nowhere;
        const string program1 =
@"begin for i in 0..2 do
    i is i + 1
end for
func loop
end loop";


        const string program2 =
@"a is true and false
b is 5 / 2
func loop
end loop";

        const string program3 =
@"wait 1ms
wait 2s
wait 3m
wait 4h
func loop
end loop";

        const string program4 =
@"a is 4 less or equal 4
b is 6 greater or equal 1
func loop
end loop";

        const string program5 =
@"a is 4 % 2
b is 3 - 1
func loop
end loop";
        const string program6 =
@"
func loop
a is [5]
a@1 is 1
b is a@1
c is 3
a@2 is c
end loop";
        const string program7 =
@"
func dafunc with a,b
b is 3
a is b
return a
end dafunc
func blink
    dpin13 is on
    wait 1s
    dpin13 is off
    wait 1s
end blink
func blink2
end blink
func loop
a is 0
b is 0
d is call dafunc with a,b
call blink
call blink2
call sq with 2
wait 4s
wait 4m
wait 4h
wait 4ms

end loop";
        const string program8 = @"
func loop
a is true
b is false
if (a == b) do
end if
if (a or b) do
b is false
end if
d is 2
c is 1
if (d < c) do
xx is 12
end if
end loop
";
        [SetUp]
        public void Setup()
        {
            Tokeniser.HasError = false;
            Parser.Parser.HasError = false;
            TypeChecker.HasError = false;
        }

        [TearDown]
        public void TearDown() { nowhere = ""; }

        [TestCase(program1)]
        [TestCase(program2)]
        [TestCase(program3)]
        [TestCase(program4)]
        [TestCase(program5)]
        [TestCase(program6)]
        [TestCase(program7)]
        [TestCase(program8)] 
        public void Test_TypeChecker_CheckHasNoErrors(string program)
        {
            StreamReader reader = CreateFakeReader(program);
            Tokeniser tokenizer = new Tokeniser(reader);
            tokenizer.GenerateTokens();
            Parser.Parser parser = new Parser.Parser(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
            Assert.IsFalse(Parser.Parser.HasError, "Parser encountered an error state:\n\n"+ nowhere);
            parser.Root.Accept(new TypeChecker());
            Assert.IsFalse(TypeChecker.HasError, "Typechecker encountered an error.");
        }

        [Test]
        public void Test_AssignmentNode_NodeHasNoOperator()
        {
            StreamReader reader = CreateFakeReader(program2);
            Tokeniser tokenizer = new Tokeniser(reader);
            tokenizer.GenerateTokens();
            Parser.Parser parser = new Parser.Parser(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
            Assert.IsFalse(Parser.Parser.HasError, "Parser encountered an error state:\n\n"+ nowhere);
            parser.Root.Accept(new TypeChecker());
            Assert.IsNull(((AssignmentNode)parser.Root.Statements[0]).Operator, "Operator was not null for assignment node");
        }

        const string exceptionProgram1 =
@"call foo
func loop
end loop";
        const string exceptionProgram2 =
@"wait 1s";
        const string exceptionProgram3 =
@"func foo with a
    a is on;
end foo
func foo with a
    a is on
end foo
func loop
end loop
";
        [TestCase(exceptionProgram1)]
        [TestCase(exceptionProgram2)]
        [TestCase(exceptionProgram3)]
        public void Test_TypeChecker_CanThrowExceptions(string program)
        {
            StreamReader reader = CreateFakeReader(program);
            Tokeniser tokenizer = new Tokeniser(reader);
            tokenizer.GenerateTokens();
            Parser.Parser parser = new Parser.Parser(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
            Assert.IsFalse(Parser.Parser.HasError, "Parser encountered an error state:\n\n"+ nowhere);
            parser.Root.Accept(new TypeChecker());
            Assert.IsTrue(TypeChecker.HasError, "The error was not caught");
        }

        public StreamReader CreateFakeReader(string content)
        {
            byte[] fakeBytes = Encoding.UTF8.GetBytes(content);
            return new StreamReader(new MemoryStream(fakeBytes), Encoding.UTF8, false);
        }
       
    }
}