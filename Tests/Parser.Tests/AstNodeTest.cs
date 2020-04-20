using Parser.Objects;
using NUnit.Framework;
using System;
using Lexer.Objects;
using Parser.Objects.Nodes;

namespace Parser.Tests
{
    public class AstNodeTest
    {
        [Test]
        public void Test_AddChild_ChildIsNull()
        {
            BeginNode node = new BeginNode(1,1);
            //Assert.Throws<NullReferenceException>(() => node.AddChild(null), "The method accepts a null child.");
        }

        [Test]
        public void Test_Visitor_ProgramNodeIsVisted()
        {
            ProgramNode prog = new ProgramNode(1,1);
            FunctionLoopNode loop = new FunctionLoopNode(1,1);
            prog.LoopFunction = loop;
            PrettyPrinter printer = new PrettyPrinter();

            printer.Visit(prog);
        }

        // [Test]
        // public void Test_AddChild_ChildIsNotNull()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);

        //     node.AddChild(new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2));

        //     Assert.IsNotEmpty(node.Children, "Child was not added to node.");
        // }

        // [Test]
        // public void Test_AddChild_ChildIsAssignedParent()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);

        //     node.AddChild(new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2));

        //     Assert.IsNotNull(node.Children[0].Parent, "Child was not assigned a parent.");
        // }

        // [Test]
        // public void Test_AddChild_ChildIsAssignedCorrectParent()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);

        //     node.AddChild(new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2));

        //     Assert.AreEqual(node, node.Children[0].Parent, "Child was not assigned correct parent.");
        // }

        // [Test]
        // public void Test_RemoveChild_ChildIsNull()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);
        //     Assert.Throws<NullReferenceException>(() => node.RemoveChild(null), "The method accepts a null child.");
        // }

        // [Test]
        // public void Test_RemoveChild_ChildIsRemoved()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);
        //     AstNode expr = new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2);
        //     node.AddChild(expr);

        //     Assert.IsNotEmpty(node.Children, "Child was not added to node.");

        //     node.RemoveChild(expr);
        //     Assert.IsEmpty(node.Children, "Child was not removed.");
        // }

        // [Test]
        // public void Test_RemoveChild_ParentIsNull()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);
        //     AstNode expr = new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2);
        //     node.AddChild(expr);

        //     Assert.IsNotEmpty(node.Children, "Child was not added to node.");

        //     node.RemoveChild(expr);

        //     Assert.IsNull(expr.Parent);
        // }

        // [Test]
        // public void Test_RemoveChild_ChildIsRemoved_WithIndex()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);
        //     AstNode expr = new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2);
        //     node.AddChild(expr);

        //     Assert.IsNotEmpty(node.Children, "Child was not added to node.");

        //     node.RemoveChild(0);
        //     Assert.IsEmpty(node.Children, "Child was not removed.");
        // }

        // [Test]
        // public void Test_RemoveChild_ParentIsNull_WithIndex()
        // {
        //     AstNode node = new AstNode(new ParseToken(TokenType.BEGIN,"",3,2), "", 3,2);
        //     AstNode expr = new AstNode(new ParseToken(TokenType.EXPR,"",3,2), "", 3,2);
        //     node.AddChild(expr);

        //     Assert.IsNotEmpty(node.Children, "Child was not added to node.");

        //     node.RemoveChild(0);

        //     Assert.IsNull(expr.Parent);
        // }
    }
}