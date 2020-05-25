using System.Collections.Generic;
using System;
using AbstractSyntaxTree.Objects.Nodes;
namespace AbstractSyntaxTree.Objects
{
    public class ParseContext
    {
        public static List<ArrayNode> DeclaredArrays {get;set;} = new List<ArrayNode>();
    }
}