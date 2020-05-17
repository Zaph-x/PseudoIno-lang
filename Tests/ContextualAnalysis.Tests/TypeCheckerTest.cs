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
        [SetUp]
        public void Setup()
        {
            Tokenizer.HasError = false;
            Parsenizer.HasError = false;
            TypeChecker.HasError = false;
        }

        [TearDown]
        public void TearDown() { nowhere = ""; }

        [TestCase(program1)]
        [TestCase(program2)]
        public void Test_TypeChecker_CheckHasNoErrors(string program)
        {
            StreamReader reader = CreateFakeReader(program);
            Tokenizer tokenizer = new Tokenizer(reader);
            tokenizer.GenerateTokens();
            Parsenizer parser = new Parsenizer(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
            parser.Root.Accept(new TypeChecker());
            Assert.IsFalse(TypeChecker.HasError, "Typechecker encountered an error.");
        }

        [Test]
        public void Test_AssignmentNode_NodeHasNoOperator()
        {
            StreamReader reader = CreateFakeReader(program2);
            Tokenizer tokenizer = new Tokenizer(reader);
            tokenizer.GenerateTokens();
            Parsenizer parser = new Parsenizer(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
            parser.Root.Accept(new TypeChecker());
            Assert.IsNull(((AssignmentNode)parser.Root.Statements[0]).Operator, "Operator was not null for assignment node");
        }

        const string exceptionProgram =
@"call foo
func loop
end loop";
        [TestCase(exceptionProgram)]
        public void Test_TypeChecker_CanThrowExceptions(string program)
        {
            StreamReader reader = CreateFakeReader(program);
            Tokenizer tokenizer = new Tokenizer(reader);
            tokenizer.GenerateTokens();
            Parsenizer parser = new Parsenizer(tokenizer.Tokens.ToList());
            parser.Parse(out nowhere);
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