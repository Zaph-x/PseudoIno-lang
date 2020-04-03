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
        /// <summary>
        /// constructer 2 for find child
        /// </summary>
        /// <param name="name"></param>
        public NodeSymbolTab(string name)
        {
            this.name = name;
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
            //foreach (var item in ChildrenList)
            //{
            //    if(item.name == name)
            //    {
            //        return item;
            //    }
            //}
            //todo fix mig skal ikke returnere null men en fejl kode ting.
            //return null;
            //var childreturn = ChildrenList.Where(child => child.name == name).ToList();
            return ChildrenList.Where(child => child.name == name).First();
        }
        //TODO  Openscope, Closescope 

    }
}
