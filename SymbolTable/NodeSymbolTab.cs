using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Objects;
using System.Linq;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    public class NodeSymbolTab
    {
        /// <summary>
        /// Children list 
        /// </summary>
        public List<NodeSymbolTab> ChildrenList = new List<NodeSymbolTab>();

        /// <summary>
        /// Parent property
        /// </summary>
        public NodeSymbolTab Parent { get; set; }
        public Guid Id = Guid.NewGuid();

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

        /// <summary>
        /// Add child to Childrenlist. Set parent property of the child node. Input parameters are name and type of the node.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public void AddNode(string name, TokenType type)
        {

            if (ChildrenList.Any(x => x.name == name) == false)
            {
                ChildrenList.Add(new NodeSymbolTab(name, type) { Parent = this });
            }
            else
            {
                throw new Exception($"Symbol table contains the name {name}");
            }
        }
        public NodeSymbolTab Findnode(string name)
        {
            return ChildrenList.Where(child => child.name == name).First();
        }
        //TODO  Openscope, Closescope 

    }
}
