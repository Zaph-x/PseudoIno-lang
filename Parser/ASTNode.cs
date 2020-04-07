using System.Collections;
using System.Collections.Generic;


namespace Parser
{
    /// <summary>
    /// This is the AST main class
    /// </summary>
    public class ASTNode:List<ASTNode>
    {
        public Lexer.Objects.TokenType type;
        public string value;
        public int line, offset;
        //List of children
        public List<ASTNode> ListChildren = new List<ASTNode>();

        public ASTNode(Lexer.Objects.TokenType Type, string Value, int Line, int Offset )
        {
            this.type = Type;
            this.value = Value;
            this.line = Line;
            this.offset = Offset;
        }
        /// <summary>
        /// Add child to the list of the ASTNode nodes.
        /// </summary>
        /// <param name="child">Ast child element</param>
        public void addChild(ASTNode child)
        {
            if (child != null)
            {
                this.ListChildren.Add(child);
            }
        }
        /// <summary>
        /// Gets a child in the ASTChildren list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>ASTNode node</returns>
        public ASTNode getChild(int index)
        {
            return ListChildren[index];
        }
        /// <summary>
        ///   Gets the first child in the ASTNode node list.  
        /// </summary>
        /// <returns></returns>
        public ASTNode getFirstChild()
        {
            return this.getChild(0);
        }
        /// <summary>
        /// Get the last child in the ASTNode node list.
        /// </summary>
        /// <returns>ASTNode node</returns>
        public ASTNode getLastChild()
        {
            return this.getChild(ListChildren.Count-1);
        }

    }
}

