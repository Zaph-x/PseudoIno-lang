using System.Text;
using NUnit.Framework;
using System.IO;
using Core.Exceptions;
using Core.Objects;

namespace Core.Tests
{
    public class FileCheckerTest
    {
        private string FakeContent = "Hello, World!";
        private byte[] FakeUTF8Bytes;
        private byte[] FakeAsciiBytes;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            FakeAsciiBytes = Encoding.ASCII.GetBytes(FakeContent);
            FakeUTF8Bytes = Encoding.UTF8.GetBytes(FakeContent);
        }

        [Test]
        public void Test_CheckEncoding_CheckerAcceptsASCII()
        {
            MemoryStream FakeAsciiStream = new MemoryStream(FakeAsciiBytes);
            StreamReader FakeReader = new StreamReader(FakeAsciiStream, Encoding.ASCII, false);
            Assert.IsTrue(FileChecker.CheckEncoding(FakeReader));
        }


        [Test]
        public void Test_CheckEncoding_CheckerAcceptsUTF8()
        {
            MemoryStream FakeUTF8Stream = new MemoryStream(FakeUTF8Bytes);
            StreamReader FakeReader = new StreamReader(FakeUTF8Stream, Encoding.UTF8, false);
            Assert.IsTrue(FileChecker.CheckEncoding(FakeReader));
        }



        [Test]
        public void Test_CheckEncoding_CheckerRejectsUTF7()
        {

            MemoryStream FakeUTF7Stream = new MemoryStream(Encoding.UTF7.GetBytes("Hello, World!"));
            StreamReader FakeReader = new StreamReader(FakeUTF7Stream, Encoding.UTF7, false);
            Assert.Throws<EncodingNotSupportedException>(() => { FileChecker.CheckEncoding(FakeReader); });
        }
    }
}