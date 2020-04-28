using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Objects;
using Parser.Objects;
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
        
        //Id
        public Guid Id = Guid.NewGuid();
        
        //Name and Type for the node
        public string Name,Value;
        public TokenType Type;
        
        /// <summary>
        /// Line og offset for every token
        /// </summary>
        public int Line, Offset;

        /// <summary>
        /// Constructor to add Name,Type, Line and offset attributes for children.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        public NodeSymbolTab(string name, ScannerToken token)
        {
            this.Value = token.Value;
            this.Type = token.Type;
            this.Name = name;
            this.Line = token.Line;
            this.Offset = token.Offset;
           
        }

        //list af tokentype ind 
        //list af scanner token ud

        /// <summary>
        /// Add child to Childrenlist. Set parent property of the child node. Input parameters are Name and Type of the node.
        /// </summary>
        /// <param Name="name"></param>
        /// <param Name="Type"></param>
        public void AddNode(string name,ScannerToken token)
        {
            if (ChildrenList.Any(x => x.Name == name) == false)
            {
                ChildrenList.Add(new NodeSymbolTab(name, token) { Parent = this });
            }
            else
            {
                throw new Exception($"Symbol table contains the Name {name}");
            }
        }
        /// <summary>
        /// Findnode methode to recursively find a node. It searches in curent scope , then parents scope , parents parent scope etc.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NodeSymbolTab Findnode(string name)
        {
            if(ChildrenList.Any(child => child.Name == name))
            {
                return ChildrenList.Where(child => child.Name == name).First();
            }
            else
            {
                return Parent.Findnode(name);
            }
        }
        

    }
}
