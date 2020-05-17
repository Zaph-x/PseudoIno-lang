using NUnit.Framework;

namespace Parser.Tests
{
    public class ExceptionTest
    {
        [SetUp]
        public void SetUp()
        {
            Parsenizer.HasError = false;
        }

        public void Test_NoScopeException_ExceptionChangesErrorState()
        {
            new NoScopeException("Test Exception");
            Assert.IsTrue(Parsenizer.HasError, "Error state was not changed by exception")
        }
    }
}