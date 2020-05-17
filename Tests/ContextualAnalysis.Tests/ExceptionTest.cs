using System;
using Contextual_analysis;
using Contextual_analysis.Exceptions;
using NUnit.Framework;

namespace ContextualAnalysis.Tests
{
    public class ExceptionTest
    {
        [SetUp]
        public void SetUp()
        {
            TypeChecker.HasError = false;
        }

        [Test]
        public void Test_InvalidTypeExceptions_ChangeErrorState()
        {
            new InvalidTypeException("Test Exception");
            Assert.IsTrue(TypeChecker.HasError, "Error state not changed");
        }
        
        [Test]
        public void Test_InvalidReturnExceptions_ChangeErrorState()
        {
            new InvalidReturnException("Test Exception");
            Assert.IsTrue(TypeChecker.HasError, "Error state not changed");
        }

        
        [Test]
        public void Test_NotDefinedExceptions_ChangeErrorState()
        {
            new NotDefinedException("Test Exception");
            Assert.IsTrue(TypeChecker.HasError, "Error state not changed");
        }
    }
}