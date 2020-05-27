using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Core;
using System.Runtime.InteropServices;

namespace Core.Tests
{
    class ProgramTest
    {

        StringWriter writer;

        [OneTimeSetUp]
        public void OTSU()
        {
            using (StreamWriter stream = new StreamWriter("./input.pi"))
            {
                stream.Write("func foo end foo func loop end loop");
            }
        }

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

        [OneTimeTearDown]
        public void OTTD()
        {
            File.Delete("./input.pi");
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
        [TestCase("-l")]
        [TestCase("--logfile")]
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
            Program.Main(new string[] { "./input.pi", "--logfile", "./logfiletest", "-d" });

            Assert.IsTrue(writer.ToString() != "", $"The compiler did not fail to compile when it should\n\nOutput: {writer.ToString()}");
        }

        [TestCase("-b")]
        [TestCase("--boilerplate")]
        public void Test_Main_ShouldPassOnNoFile(string option)
        {
            List<string> argsList = new List<string>();
            argsList.Add("./input.pi");
            argsList.Add(option);
            argsList.Add("-o");
            string[] args = argsList.ToArray();

            Program.Main(args);


            Assert.IsFalse(writer.ToString() != "", $"The compiler fail to compile\n\nOutput: {writer.ToString()}");
        }

        [TestCase("-pr")]
        [TestCase("--proc")]
        public void Test_Main_ShouldPassOnNoFile_2(string option)
        {
            List<string> argsList = new List<string>();
            argsList.Add("./input.pi");
            argsList.Add(option);
            argsList.Add("atmega328p");
            argsList.Add("-o");
            string[] args = argsList.ToArray();

            Program.Main(args);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                Assert.AreEqual(writer.ToString(), "We're on Linux!\nError: No Port Provided. The compiler will try to find one available.\n", $"The compiler fail to compile\n\nOutput: {writer.ToString()}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Assert.AreEqual(writer.ToString(), "We're on Windows!\r\nError: No Port Provided. The compiler will try to find one available.\r\n", $"The compiler fail to compile\r\n\r\nOutput: {writer.ToString()}");
        }

        [TestCase("-a")]
        [TestCase("--arduino")]
        public void Test_Main_ShouldPassOnNoFile_3(string option)
        {
            List<string> argsList = new List<string>();
            argsList.Add("./input.pi");
            argsList.Add(option);
            argsList.Add("uno");
            argsList.Add("-o");
            string[] args = argsList.ToArray();

            Program.Main(args);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                Assert.AreEqual(writer.ToString(), "We're on Linux!\nError: No Port Provided. The compiler will try to find one available.\n", $"The compiler fail to compile\n\nOutput: {writer.ToString()}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Assert.AreEqual(writer.ToString(), "We're on Windows!\r\nError: No Port Provided. The compiler will try to find one available.\r\n", $"The compiler fail to compile\r\n\r\nOutput: {writer.ToString()}");
        }

        [TestCase("-p")]
        [TestCase("--port")]
        public void Test_Main_ShouldPassOnNoFile_4(string option)
        {
            List<string> argsList = new List<string>();
            argsList.Add("./input.pi");
            argsList.Add(option);
            argsList.Add("COM3");
            argsList.Add("-o");
            string[] args = argsList.ToArray();

            Program.Main(args);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                Assert.AreEqual(writer.ToString(), "We're on Linux!\n", $"The compiler fail to compile\n\nOutput: {writer.ToString()}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Assert.AreEqual(writer.ToString(), "We're on Windows!\r\n", $"The compiler fail to compile\r\n\r\nOutput: {writer.ToString()}");

        }

        [TestCase("-p")]
        [TestCase("--port")]
        public void Test_Main_ShouldPassOnNoFile_5(string option)
        {
            List<string> argsList = new List<string>();
            argsList.Add("./input.pi");
            argsList.Add(option);
            argsList.Add("-o");
            string[] args = argsList.ToArray();

            Program.Main(args);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                Assert.AreEqual(writer.ToString(), "Error: No Port Provided. The compiler will try to find one available.\nWe're on Linux!\nError: No Port Provided. The compiler will try to find one available.\n", $"The compiler fail to compile\n\nOutput: {writer.ToString()}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Assert.AreEqual(writer.ToString(), "Error: No Port Provided. The compiler will try to find one available.\r\n", $"The compiler fail to compile\r\n\r\nOutput: {writer.ToString()}");

        }
    }
}
