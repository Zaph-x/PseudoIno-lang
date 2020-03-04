using NUnit.Framework;
using Lexer;
using Lexer.Exceptions;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lexer.Tests
{
    public class Tests
    {
        Recogniser recogniser = new Recogniser();

        [SetUp]
        public void Setup()
        {
            
            onetime();
        }

        [Test]
        public void CorrectInputTest()
        {
            
            if(recogniser.InputString("a is 4") == 0)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void WrongInput1()
        {
            
            if(recogniser.InputString("a 2 is") == 0)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void WrongInput2()
        {
            
            if(recogniser.InputString("neiojsfj3324") == 0)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void ScanNum1()
        {
            
            if(recogniser.ScanDigtig("2222") == 2222)
                Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void Test_WrongInputShouldThrowException()
        {
            Assert.Throws<InvalidSyntaxException>(() => {
                recogniser.ScanDigtig("NotAValidDigit");
            }, "Recogniser failed to catch invalid digit");

        }
        
        [Test]
        public void ScanNum2()
        {
            
            if(recogniser.ScanDigtig("1.1")-1.1 <0.0001)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void ScanNum3()
        {
            
            if(recogniser.ScanDigtig("         22 ") == 22)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void ScanNum4()
        {
            
            if(recogniser.ScanDigtig("   111.1011 ")-111.1011<0.0)
                Assert.Pass();
            Assert.Fail();
        }
        [Test]
        public void ScanNum6()
        {
            
            if (recogniser.ScanDigtig("-2222") == -2222)
                Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void ScanNum7()
        {
            
            if (recogniser.ScanDigtig("-0111.221")+0111.221<0.0001)
	         Assert.Pass();
            Assert.Fail();
        }
        
        /*[Test]
        public void ScanNum8()
        {
            
            if (recogniser.IsDigit("a"))
                Assert.Fail();
            Assert.Pass();
        }*/

        [Test]
        public void SplitCountString()
        {
            string inputString = "a is 1";
            
            if (recogniser.SplitCountString(inputString) == 3)
                Assert.Pass();
            Assert.Fail();
        }
        

        [Test]
        public void SplitCountStringExtraWhiteSpace()
        {
            string inputString = "a           is  1";
            
            if (recogniser.SplitCountString(inputString) == 3)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void SplitCountString0()
        {
            string inputString = "";
            
            if (recogniser.SplitCountString(inputString) == 0)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void SplitCountStringNewLineFail()
        {
            string inputString = "test \n test";
            
            if (recogniser.SplitCountString(inputString) == 2)
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void FileExist()
        {
            
            if(recogniser.FileExist("fileExist"))
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void FileNotExist()
        {
            
            if(recogniser.FileExist("fileNotExist"))
                Assert.Fail();
            Assert.Pass();
        }
        
        [Test]
        public void FileLineCountQuals10()
        {
            
            if(recogniser.FileLineCount("fileWith10Lines") == 10)
                Assert.Pass();
            Assert.Fail();
        }
        
        [Test]
        public void FileLineCountNotQuals0()
        {
            
            if(recogniser.FileLineCount("fileWith10Lines") == 0)
                Assert.Fail();
            Assert.Pass();
        }

        // Helper functions

        public void onetime()
        {
            using (FileStream fs = File.Create("fileExist")) {}
            
            List<string> lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(i.ToString());
            }
            
            using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter("fileWith10Lines"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}
