using NUnit.Framework;
using Lexer;
using System.Text.RegularExpressions;
using System;

namespace Lexer.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Recogniser recogniser = new Recogniser();
            recogniser.onetime();
        }

        [Test]
        public void CorrectInputTest()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.InputString("a is 4") == 0)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void WrongInput1()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.InputString("a 2 is") == 0)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void WrongInput2()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.InputString("neiojsfj3324") == 0)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void ScanNum1()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.ScanDigtig("2222") == 2222)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void ScanNum2()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.ScanDigtig("1.1")-1.1 <0.0001)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void ScanNum3()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.ScanDigtig("         22 ") == 22)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void ScanNum4()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.ScanDigtig("   111.1011 ")-111.1011<0.0)
                Assert.Pass();
            Assert.Fail();
        }
        [Test]
        public void ScanNum6()
        {
            Recogniser recogniser = new Recogniser();
            if (recogniser.ScanDigtig("-2222") == -2222)
                Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void ScanNum7()
        {
            Recogniser recogniser = new Recogniser();
            if (recogniser.ScanDigtig("-0111.221")+0111.221<0.0001)
	         Assert.Pass();
            Assert.Fail();
        }
        


        [Test]
        public void SplitCountString()
        {
            string inputString = "a is 1";
            Recogniser recogniser = new Recogniser();
            if (recogniser.SplitCountString(inputString) == 3)
                Assert.Pass();
            Assert.Fail();
        }
        

        [Test]
        public void SplitCountStringExtraWhiteSpace()
        {
            string inputString = "a           is  1";
            Recogniser recogniser = new Recogniser();
            if (recogniser.SplitCountString(inputString) == 3)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void SplitCountString0()
        {
            string inputString = "";
            Recogniser recogniser = new Recogniser();
            if (recogniser.SplitCountString(inputString) == 0)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void SplitCountStringNewLineFail()
        {
            string inputString = "test \n test";
            Recogniser recogniser = new Recogniser();
            if (recogniser.SplitCountString(inputString) == 2)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void FileExist()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.FileExist("fileExist"))
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void FileNotExist()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.FileExist("fileNotExist"))
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void FileLineCountQuals10()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.FileLineCount("fileWith10Lines") == 10)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void FileLineCountNotQuals0()
        {
            Recogniser recogniser = new Recogniser();
            if(recogniser.FileLineCount("fileWith10Lines") == 0)
                Assert.Fail();
            Assert.Pass();
        }
    }
}
