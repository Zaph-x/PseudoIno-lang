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

        public void UpdateTypedef(VarNode leftHand, TypeContext rhs, string scopeName)
        {
            if (IsInFunction())
            {
                string name = GetEnclosingFunction().Name.Substring(5);
                if (SymbolTableBuilder.GlobalSymbolTable.FunctionDefinitions.First(fn => fn.Name.Id == name)?.FunctionParameters.Any(fnp => fnp.Id == leftHand.Id) ?? false)
                {
                    leftHand.Declaration = false;
                    if (this.Name == scopeName)
                    {
                        foreach (SymbolTableObject child in this.Children)
                        {
                            child.UpdateTypedef(leftHand, rhs, scopeName);
                        }
                    }
                    if (this.Symbols.Any(sym => sym.Name == leftHand.Id))
                        foreach (Symbol sym in this.Symbols.Where(s => s.Name == leftHand.Id))
                        {
                            sym.AstNode.SymbolType = rhs;
                            sym.TokenType = rhs.Type;
                        }
                    return;
                }
            }
            SymbolTableObject global = this.Parent;
            while (global?.Parent != null)
            {
                global.Parent.UpdateTypedef(leftHand, rhs, scopeName);
                global = global.Parent;
            }

            if (global != null)
            {
                foreach (FuncNode func in global.FunctionDefinitions)
                {
                    string[] s = this.Name.Split("func_");
                    if (s.Length == 2)
                    {
                        if (s[1] == func.Name.Id)
                        {
                            foreach (VarNode parameter in func.FunctionParameters)
                            {
                                if (parameter.Id == leftHand.Id)
                                {
                                    parameter.SymbolType = rhs;
                                    parameter.Declaration = false;
                                    break;
                                }
                                else
                                {
                                    parameter.Declaration = true;
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
            if (this.Name == scopeName)
            {
                foreach (SymbolTableObject child in this.Children)
                {
                    child.UpdateTypedef(leftHand, rhs, scopeName);
                }
            }
            if (this.Symbols.Any(sym => sym.Name == leftHand.Id))
                foreach (Symbol sym in this.Symbols.Where(s => s.Name == leftHand.Id))
                {
                    sym.AstNode.SymbolType = rhs;
                    sym.TokenType = rhs.Type;
                }
        }

        public bool IsInFunction()
        {
            SymbolTableObject symtab = this;
            while (this.Parent != null && (!this.Parent.Name?.StartsWith("func_") ?? false))
            {
                symtab = this.Parent;
            }
            return symtab.Parent?.Name?.StartsWith("func_") ?? false;
        }
        public SymbolTableObject GetEnclosingFunction()
        {
            SymbolTableObject symtab = this;
            while (this.Parent != null && (!this.Parent.Name?.StartsWith("func_") ?? false))
            {
                symtab = this.Parent;
            }
            if (symtab.Parent?.Name?.StartsWith("func_") ?? false)
                return symtab.Parent;
            return null;
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

