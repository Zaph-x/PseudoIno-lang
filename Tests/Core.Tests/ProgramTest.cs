using System.Text;
using System;
using System.IO;
using NUnit.Framework;
using Core;

namespace Core.Tests
{
    class ProgramTest
    {

        StringWriter writer;

        [SetUp]
        public void Setup()
        {
            writer = new StringWriter();
            Console.SetOut(writer);
            Console.SetError(writer);
        }

        [TearDown]
        public void TearDown()
        {
            writer.Dispose();
        }

        [TestCase("-d")]
        [TestCase("--DryRun")]
        [TestCase("-o")]
        [TestCase("--Output")]
        [TestCase("-v")]
        [TestCase("-b")]
        [TestCase("--boilerplate")]
        [TestCase("-l")]
        [TestCase("--logfile")]
        [TestCase("-pp")]
        [TestCase("--prettyprinter")]
        public void Test_Help_ShouldContainOption(string option)
        {
            Program.Help();

            Assert.IsTrue(writer.ToString().Contains(option), "The expected option was not found in the help message.");
        }

        [TestCase("-d")]
        [TestCase("--DryRun")]
        [TestCase("-o")]
        [TestCase("--Output")]
        [TestCase("-v")]
        [TestCase("--Verbose")]
        [TestCase("-b")]
        [TestCase("--boilerplate")]
        [TestCase("-pp")]
        [TestCase("--prettyprinter")]
        public void Test_Parse_ShouldAcceptValidOptions(string option)
        {
            Program.ParseOptions(new string[] { "test.pi", option });

            Assert.IsEmpty(writer.ToString(), "The expected option gave an exception.");
        }

        [TestCase("-d")]
        [TestCase("--DryRun")]
        [TestCase("-o")]
        [TestCase("--Output")]
        [TestCase("-v")]
        [TestCase("--verbose")]
        [TestCase("-b")]
        [TestCase("--boilerplate")]
        [TestCase("-pp")]
        [TestCase("--prettyprinter")]
        public void Test_Main_ShouldErrorOnNoFile(string option)
        {
            Program.Main(new string[] { option });

            Assert.IsTrue(writer.ToString() != "", $"The compiler did not fail to compile when it should\n\nOutput: {writer.ToString()}");
        }
        [Test]
        public void Test_Main_LogFile()
        {
            Program.Main(new string[] { "./input.pi", "--logfile", "./logfiletest", "-d", "-pr", "atmega32u4" });

            Assert.IsTrue(writer.ToString() != "", $"The compiler did not fail to compile when it should\n\nOutput: {writer.ToString()}");
        }
        [Test]
        public void Test_Main_Fileencoding()
        {
            Program.Main(new string[] { "./input23.pi", "--logfile", "./logfiletest", "-d", "-pr", "atmega32u4" });

            Assert.IsTrue(writer.ToString() != "", $"The compiler did not fail to compile when it should\n\nOutput: {writer.ToString()}");
        }
    }
}