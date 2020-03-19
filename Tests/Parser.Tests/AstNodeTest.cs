using Parser.Objects;
using NUnit.Framework;
using System;

namespace Parser.Tests
{
    public class AstNodeTest
    {
        [Test]
        public void Test_AddChild_ChildIsNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);
            Assert.Throws<NullReferenceException>(() => node.AddChild(null), "The method accepts a null child.");
        }

        [Test]
        public void Test_AddChild_ChildIsNotNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);

            node.AddChild(new AstNode(ParseToken.EXPR, "", 3,2));

            Assert.IsNotEmpty(node.Children, "Child was not added to node.");
        }

        [Test]
        public void Test_AddChild_ChildIsAssignedParent()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);

            node.AddChild(new AstNode(ParseToken.EXPR, "", 3,2));

            Assert.IsNotNull(node.Children[0].Parent, "Child was not assigned a parent.");
        }

        [Test]
        public void Test_AddChild_ChildIsAssignedCorrectParent()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);

            node.AddChild(new AstNode(ParseToken.EXPR, "", 3,2));

            Assert.AreEqual(node, node.Children[0].Parent, "Child was not assigned correct parent.");
        }

        [Test]
        public void Test_RemoveChild_ChildIsNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);
            Assert.Throws<NullReferenceException>(() => node.RemoveChild(null), "The method accepts a null child.");
        }

        [Test]
        public void Test_RemoveChild_ChildIsRemoved()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);
            AstNode expr = new AstNode(ParseToken.EXPR, "", 3,2);
            node.AddChild(expr);

            Assert.IsNotEmpty(node.Children, "Child was not added to node.");

            node.RemoveChild(expr);
            Assert.IsEmpty(node.Children, "Child was not removed.");
        }

        [Test]
        public void Test_RemoveChild_ParentIsNull()
        {
            AstNode node = new AstNode(ParseToken.BEGIN, "", 3,2);
            AstNode expr = new AstNode(ParseToken.EXPR, "", 3,2);
            node.AddChild(expr);

            Assert.IsNotEmpty(node.Children, "Child was not added to node.");

            node.RemoveChild(expr);

            Assert.IsNull(expr.Parent);
        }
    }
}