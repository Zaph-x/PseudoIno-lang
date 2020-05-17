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
        public List<FuncNode> FunctionDefinitions { get; set; } = new List<FuncNode>();
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
                new SymbolNotFoundException($"The symbol '{var.Id}' was not found");
                return null;
            }
        }

        public void UpdateTypedef(VarNode leftHand, TypeContext rhs)
        {
            SymbolTableObject global = this.Parent;
            while (global?.Parent != null)
            {
                global = global.Parent;
            }

            if (global != null)
            {
                foreach (var func in global.FunctionDefinitions)
                {
                    var s = this.Name.Split("func_");
                    if (s.Length == 2)
                    {
                        if (s[1] == func.Name.Id)
                        {
                            foreach (var parameter in func.FunctionParameters)
                            {
                                if (parameter.Id == leftHand.Id)
                                {
                                    leftHand.Declaration = false;
                                    break;
                                }
                                else
                                {
                                    leftHand.Declaration = true;
                                }
                            }
                        }
                        else
                        {
                            leftHand.Declaration = true;
                        }
                    }
                }
            }
            else
            {
                leftHand.Declaration = true;
            }
            if (this.Symbols.Any(sym => sym.Name == leftHand.Id))
                foreach (Symbol sym in this.Symbols.Where(s => s.Name == leftHand.Id))
                {
                    sym.AstNode.SymbolType = rhs;
                    sym.TokenType = rhs.Type;
                }
            else
                this.Symbols.Add(new Symbol(leftHand.Id, rhs.Type, false, leftHand));

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
                if (found != null) break;
            }
            return found;
        }
    }
}

