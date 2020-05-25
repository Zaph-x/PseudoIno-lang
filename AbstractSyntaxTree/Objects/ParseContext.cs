using System.Collections.Generic;
using System;
using AbstractSyntaxTree.Objects.Nodes;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    public class ParseContext
    {
        public static List<ArrayNode> DeclaredArrays {get;set;} = new List<ArrayNode>();
    }
}