using System.Collections;
using System.Collections.Generic;


namespace Parser
{
    //TODO gem tokens fra parser i en liste<T> og call AST får at køre preorder tree walk . print tree.
    //TODO gem værdierne i tree structur.
    /// <summary>
    /// This is the AST main class
    /// </summary>
    public class AST:List<AST>
    {
        public Lexer.Objects.TokenType type;
        public string value;
        public int line, offset;
        //List of children
        public List<AST> ListChildren = new List<AST>();

        public AST(Lexer.Objects.TokenType Type, string Value, int Line, int Offset )
        {
            this.type = Type;
            this.value = Value;
            this.line = Line;
            this.offset = Offset;
        }
        /// <summary>
        /// Add child to the list of the AST nodes.
        /// </summary>
        /// <param name="child">Ast child element</param>
        public void addChild(AST child)
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
        /// <returns>AST node</returns>
        public AST getChild(int index)
        {
            return ListChildren[index];
        }
        /// <summary>
        ///   Gets the first child in the AST node list.  
        /// </summary>
        /// <returns></returns>
        public AST getFirstChild()
        {
            return this.getChild(0);
        }
        /// <summary>
        /// Get the last child in the AST node list.
        /// </summary>
        /// <returns>AST node</returns>
        public AST getLastChild()
        {
            return this.getChild(this.Count);
        }

    }
}

