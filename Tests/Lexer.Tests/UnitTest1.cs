using Compiler;
using NUnit.Framework;
using Compiler;

namespace Lexer.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Recogniser r = new Recogniser();
            if(r.InputString("a is 4") == 0)
                Assert.Pass();
            Assert.Fail();
        }
    }
}