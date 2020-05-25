using System.Collections.Generic;
using System;
using AbstractSyntaxTree.Objects.Nodes;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// This class get and set the list of arrays
    /// </summary>
    public class ParseContext
    {
        /// <summary>
        /// A list of declared arrays which is then inserted into a list of arraynodes
        /// </summary>
        /// <typeparam name="ArrayNode">the name of the array</typeparam>
        /// <returns></returns>
        public static List<ArrayNode> DeclaredArrays {get;set;} = new List<ArrayNode>();
    }
}