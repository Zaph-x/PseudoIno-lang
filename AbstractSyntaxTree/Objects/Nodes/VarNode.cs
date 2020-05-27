using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the var node class
    /// It inherits from val node class and implements the assignable interface
    /// </summary>
    public class VarNode : ValNode, IAssginable
    {
        /// <summary>
        /// This sets and returns the value of the ID
        /// </summary>
        public string Id {get;set;}
        /// <summary>
        /// This sets and returns the boolean value of decleration,
        /// </summary>
        public bool Declaration { get; set; } = false;
        /// <summary>
        /// This sets and returns the boolean value for isarray
        /// </summary>
        public bool IsArray { get; set; } = false;
        /// <summary>
        /// This is the constructor for var node
        /// Id is assigned to id
        /// </summary>
        /// <param name="id">This is the ID</param>
        /// <param name="token">This is the token</param>
        public VarNode(string id,  ScannerToken token) : base(token)
        {
            this.Id = id;
        }
        /// <summary>
        /// This method converts the ID to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Id}";
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}