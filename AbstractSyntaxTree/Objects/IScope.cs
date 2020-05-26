using System.Collections.Generic;
using AbstractSyntaxTree.Objects.Nodes;


namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// Using this interface, get and set the value of the statementlist
    /// It is used when scopes are opend and closed
    /// </summary>
    public interface IScope
    {
        /// <summary>
        /// Get the list of statement.
        /// </summary>
        /// <value>The value is set in the constructor of the node which has statements</value>
        List<StatementNode> Statements {get;set;}
    }
}