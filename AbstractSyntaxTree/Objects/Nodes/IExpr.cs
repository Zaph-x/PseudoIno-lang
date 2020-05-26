using System;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the interface for an expresstion
    /// In implements the interface of typed
    /// </summary>
    public interface IExpr : ITyped
    {
        /// <summary>
        /// This sets and returns the value of the lefthand of an expression
        /// </summary>
        ITerm LeftHand { get; set; }
        /// <summary>
        /// This sets and returns the value of operator node
        /// </summary>
        OperatorNode Operator { get; set; }
        /// This sets and returns the value of the righthand of an expression
        IExpr RightHand { get; set; }
        /// <summary>
        /// This accept the object 
        /// </summary>
        /// <param name="visitor">The name of the visitor</param>
        /// <returns>returns the accepted object</returns>
        object Accept(Visitor visitor);
        /// <summary>
        /// This checks if it has been typed checked
        /// </summary>
        /// <param name="type">This is the type</param>
        /// <returns>This returns true or false</returns>
        bool IsType(Type type);
    }
}