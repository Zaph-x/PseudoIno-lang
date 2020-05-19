using System.Runtime.CompilerServices;
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
        public List<string> DeclaredVars = new List<string>();
        public static List<CallNode> FunctionCalls {get;set;} = new List<CallNode>();
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

        public void AddCallReference(CallNode call)
        {
            if (!FunctionCalls.Any(cn => cn.Id.Id == call.Id.Id && cn.Parameters.Count == call.Parameters.Count))
            {
                FunctionCalls.Add(call);
            }
        }

        public override string ToString() => $"{Name}";
        public bool HasDeclaredVar(string name)
        {
            return this.DeclaredVars.Contains(name) || (this.Parent?.HasDeclaredVar(name) ?? false);
        }
        public TypeContext FindSymbol(VarNode var)
        {
            if (this.Parent != null && this.Parent.FindSymbol(var) != null)
            {
                return this.Parent.FindSymbol(var);
            }
            else if (this.Symbols.Any(sym => sym.Name == var.Id))
            {
                return new TypeContext(this.Symbols.First(sym => sym.Name == var.Id).TokenType);
            }
            else
            {
                new SymbolNotFoundException($"The symbol '{var.Id}' was not found");
                return null;
            }
        }

        public void UpdateTypedef(VarNode leftHand, TypeContext rhs, string scopeName, bool goback)
        {
            if (!goback)
            {
                if (this.Name == SymbolTableBuilder.GlobalSymbolTable.Name)
                {
                    foreach (SymbolTableObject child in this.Children)
                    {
                        child.UpdateTypedef(leftHand, rhs, scopeName, goback);
                    }
                }
            }
            else
            {
                if (this.IsInFunction())
                {
                    GetEnclosingFunction().UpdateFuncVar(leftHand, rhs, GetEnclosingFunction().Name);
                }
            }
            if (this.Symbols.Any(sym => sym.Name == leftHand.Id))
            {
                this.Symbols.FindAll(sym => sym.Name == leftHand.Id).ForEach(node =>
                {
                    node.TokenType = rhs.Type;
                });
                leftHand.SymbolType = rhs;
                leftHand.Type = rhs.Type;
            }
        }



        public void UpdateFuncVar(VarNode node, TypeContext rhs, string scopeName)
        {
            SymbolTableObject symobj = SymbolTableBuilder.GlobalSymbolTable.Children.First(symtab => symtab.Name == scopeName);

            FuncNode func = SymbolTableBuilder.GlobalSymbolTable.FunctionDefinitions.First(fn => this.Name.Contains(fn.Name.Id) && this.Name.Contains("_" + fn.Line));
            if (func.FunctionParameters.Any(vn => vn.Id == node.Id))
                node.Declaration = false;
            symobj.UpdateTypedef(node, rhs, scopeName, false);
        }

        public bool IsInFunction()
        {
            SymbolTableObject symtab = this;
            while (this.Parent != null && (!this.Parent.Name?.StartsWith("func_") ?? false))
            {
                symtab = this.Parent;
            }
            return (symtab.Parent?.Name?.StartsWith("func_") ?? false) || (this.Name?.StartsWith("func_") ?? false);
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
            else if ((this.Name?.StartsWith("func_") ?? false))
                return this;
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

