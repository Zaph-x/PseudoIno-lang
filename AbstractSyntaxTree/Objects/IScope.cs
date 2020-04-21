using System.Collections.Generic;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public interface IScope
    {
        List<StatementNode> Statements {get;set;}
    }
}