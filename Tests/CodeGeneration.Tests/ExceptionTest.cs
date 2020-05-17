using NUnit.Framework;
using CodeGeneration.Exceptions;

namespace CodeGeneration.Tests
{
    public class ExceptionTest
    {
        [SetUp]
        public void SetUp()
        {
            CodeGenerationVisitor.HasError = false;
        }

        [Test]
        public void Test_InvalidCodeException_ExceptionChangesErrorState()
        {
            new InvalidCodeException("Test Exception");
            Assert.IsTrue(CodeGenerationVisitor.HasError,"Exception did not change error state");
        }
    }
}