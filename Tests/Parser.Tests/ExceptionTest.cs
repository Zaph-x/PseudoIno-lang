using NUnit.Framework;

namespace Parser.Tests
{
    public class ExceptionTest
    {
        [SetUp]
        public void SetUp()
        {
            Parser.HasError = false;
        }

        public void Test_NoScopeException_ExceptionChangesErrorState()
        {
            new NoScopeException("Test Exception");
            Assert.IsTrue(Parser.HasError, "Error state was not changed by exception");
        }
    }
}