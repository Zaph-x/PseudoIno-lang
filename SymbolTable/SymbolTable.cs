using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lexer.Objects;
using Parser.Objects;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    public class SymbolTable
    {
        
        public List<Symbol> Symbols = new List<Symbol>();
        
        public SymbolTable Parent { get; set; }
        public TokenType Type { get; set; }
        public string Name { get; set; }
        
        public int Depth { get; set; }
        
        public SymbolTable()
        {
            //Parent = parent;
        }

        /*public override int GetHashCode()
        {
            return (Parent.GetHashCode()+Type.GetHashCode()).GetHashCode();
        }*/

        
        
        /// <summary>
        /// Findnode methode to recursively find a node. It searches in curent scope , then parents scope , parents parent scope etc.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
        
    }
}

