using System.Collections.Generic;
using System;
using AbstractSyntaxTree.Objects.Nodes;


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
        /// <returns></returns>
        public static List<ArrayNode> DeclaredArrays {get;set;} = new List<ArrayNode>();
    }
}