using System.Collections.Generic;
using AbstractSyntaxTree.Objects.Nodes;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// Using this interface, 
    /// </summary>
    public interface IScope
    {
        List<StatementNode> Statements {get;set;}
    }
}