using System;
using System.Collections.Generic;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;
using SymbolTable.Exceptions;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    public class SymbolTableObject
    {

        public List<Symbol> Symbols = new List<Symbol>();
        public List<SymbolTableObject> Children { get; set; } = new List<SymbolTableObject>();
        private SymbolTableObject _parent { get; set; }
        public SymbolTableObject Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
                this._parent.Children.Add(this);
            }
        }
        public TokenType Type { get; set; } = TokenType.PROG;
        public string Name { get; set; }

        public int Depth { get; set; }

        public SymbolTableObject()
        {
            SymbolTableBuilder.TopOfScope.Push(this);
        }

        public override string ToString() => $"{Name}";

        public TypeContext FindSymbol(VarNode var)
        {
            if (this.Symbols.Any(sym => sym.Name == var.Id))
            {
                return new TypeContext(this.Symbols.First(sym => sym.Name == var.Id).TokenType);
            }
            else if (this.Parent != null)
            {
                return this.Parent.FindSymbol(var);
            }
            else
            {
                throw new SymbolNotFoundException($"Symbol {var} was not found in the symboltable");
                return null;
            }
        }

        public void UpdateTypedef(VarNode leftHand, TypeContext rhs)
        {
            foreach (Symbol sym in this.Symbols.Where(s => s.Name == leftHand.Id))
            {
                sym.AstNode.SymbolType = rhs;
                sym.TokenType = rhs.Type;
            }
                
            foreach (SymbolTableObject child in this.Children)
            {
                // if (child.Type == TokenType.FUNCDECL)
                child.UpdateTypedef(leftHand, rhs);
            }
        }

        public SymbolTableObject FindChild(string name)
        {
            SymbolTableObject found = null;
            foreach (SymbolTableObject child in this.Children)
            {
                if (child.Name == name)
                    return child;
                found = child.FindChild(name);
            }
            return found;
        }

        /// <summary>
        /// Findnode methode to recursively find a node. It searches in curent scope , then parents scope , parents parent scope etc.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    }
}

