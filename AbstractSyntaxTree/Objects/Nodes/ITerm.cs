using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the interface for terms of expressions
    /// In implements the interface typed
    /// </summary>
    public interface ITerm : ITyped
    {
        /// <summary>
        /// This method accepts the visited node
        /// </summary>
        /// <param name="visitor">The name of the visited node</param>
        /// <returns>Returns the object</returns>
        object Accept(Visitor visitor);
        /// <summary>
        /// This method checks if the node has been type checked
        /// </summary>
        /// <param name="type">The type of the node</param>
        /// <returns>Returns true or false</returns>
        bool IsType(Type type);
    }
}