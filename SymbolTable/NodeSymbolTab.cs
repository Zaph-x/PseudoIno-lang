using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Objects;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    public class NodeSymbolTab
    {
        public List<NodeSymbolTab> ChildrenList = new List<NodeSymbolTab>();
        /// <summary>
        /// Parent prop
        /// </summary>
        public NodeSymbolTab Parent { get ; set; }
        /// <summary>
        /// Constructor to add name and type for children.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public NodeSymbolTab(string name, TokenType type)
        {
            this.name = name;
            this.type = type;
        }
       //name and type for the node
       public string name;
       public TokenType type;
        
        public void AddNode(string name, TokenType type) 
        {
            ChildrenList.Add(new NodeSymbolTab(name, type) { Parent=this});
            
        }
        
        
        //TODO ID, Add children, Openscope, Closescope Add parent,
        
    }
}
