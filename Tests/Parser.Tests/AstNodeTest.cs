using Parser.Objects;
using NUnit.Framework;
using System;

namespace Parser.Tests
{
    public class AstNodeTest
    {
        [Test]
        public void Test_AstNodeAddChildren_ChildIsNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);
            Assert.Throws<NullReferenceException>(() => node.AddChild(null), "The method accepts a null child.");
        }

        [Test]
        public void Test_AstNodeAddChildren_ChildIsNotNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);

            node.AddChild(new AstNode(ParseToken.EXPR, "", 3,2));

            Assert.IsNotEmpty(node.Children, "Child was not added to node.");
        }
    }
}