using NUnit.Framework;

namespace ASTTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ASTNode()
        {
            Parser.ASTNode TestNode = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0,0);
            Parser.ASTNode TestNode2 = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0, 0);
            int Count = 2;
            TestNode2.addChild(TestNode);
            TestNode2.addChild(TestNode);
            Assert.AreEqual(Count,TestNode2.ListChildren.Count);
          
        }
        [Test]
        public void Test_ASTNodeLastChild()
        {
            Parser.ASTNode TestNode = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0, 0);
            Parser.ASTNode TestNode2 = new Parser.ASTNode(Lexer.Objects.TokenType.APIN, "true", 0, 0);
            Parser.ASTNode TestNode3 = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0, 0);
            int Count = 2;
            TestNode3.addChild(TestNode);
            TestNode3.addChild(TestNode2);
            Assert.AreEqual(TestNode2.type, TestNode3.getLastChild().type);

        }
        [Test]
        public void Test_ASTNodeFirstChild()
        {
            Parser.ASTNode TestNode = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0, 0);
            Parser.ASTNode TestNode2 = new Parser.ASTNode(Lexer.Objects.TokenType.APIN, "true", 0, 0);
            Parser.ASTNode TestNode3 = new Parser.ASTNode(Lexer.Objects.TokenType.BOOL, "true", 0, 0);
            int Count = 2;
            TestNode3.addChild(TestNode);
            TestNode3.addChild(TestNode2);
            Assert.AreEqual(TestNode.type, TestNode3.getFirstChild().type);

        }
    }
}