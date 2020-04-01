using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Objects;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    class NodeSymbolTab
    {
        List<NodeSymbolTab> ChildrenList = new List<NodeSymbolTab>();
        public NodeSymbolTab Parent { get; set; }
       
        string name;
        TokenType type;

        //TODO ID, Add children, Openscope, Closescope Add parent,
        
    }
}
