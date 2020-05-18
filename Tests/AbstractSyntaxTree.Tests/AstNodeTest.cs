using NUnit.Framework;
using System;
using Lexer.Objects;
using AbstractSyntaxTree.Objects.Nodes;


namespace AbstractSyntaxTree.Tests
{
    public class AstNodeTest
    {
        [Test]
        public void Test_ToString_NumericTypeIsCorrect()
        {
            NumericNode node = new NumericNode("1", new ScannerToken(TokenType.NUMERIC, "", 0, 0));

            Assert.AreEqual("Type=NUMERIC", node.ToString(), "Type was not assigned correctly");
        }

        [Test]
        public void Test_Constructors_ObjectIsCreated()
        {
            ExpressionNode node = new BinaryExpression(TokenType.BOOL, new ScannerToken(TokenType.BOOL, "true", 0, 0));

            Assert.IsNotNull(node);
        }

        [Test]
        public void Test_ToString_ExpressionIsReturnedCorrectly()
        {
            BinaryExpression node = new BinaryExpression(0,0);
            Assert.AreEqual("  ", node.ToString(), "Tostring did not return the correct string");
        }
    }
}