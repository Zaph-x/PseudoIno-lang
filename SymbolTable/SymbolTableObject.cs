using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;
using SymbolTable.Exceptions;
using AbstractSyntaxTree.Objects;

namespace SymbolTable
{
    /// <summary>
    /// The symbol table class object
    /// </summary>
    public class SymbolTableObject
    {
        /// <summary>
        /// The list of symbols in the symboltable
        /// </summary>
        public List<Symbol> Symbols = new List<Symbol>();
        /// <summary>
        /// The child scopes of the symbol table
        /// </summary>
        public List<SymbolTableObject> Children { get; set; } = new List<SymbolTableObject>();
        /// <summary>
        /// The parent scope of the current scope
        /// </summary>
        private SymbolTableObject _parent { get; set; }
        /// <summary>
        /// A static list of function definitions
        /// </summary>
        public static List<FuncNode> FunctionDefinitions { get; set; } = new List<FuncNode>();
        /// <summary>
        /// The declared variables in the current scope
        /// </summary>
        public List<string> DeclaredVars = new List<string>();
        /// <summary>
        /// A static list of function calls to remove unused functions
        /// </summary>
        /// <value>Defined functions</value>
        public static List<CallNode> FunctionCalls { get; set; } = new List<CallNode>();
        /// <summary>
        /// A list of declared arrays in a given scope
        /// </summary>
        public List<ArrayNode> DeclaredArrays { get; set; } = new List<ArrayNode>();
        /// <summary>
        /// A static list of predefined functions
        /// </summary>
        /// <value>Predefined functions</value>
        public static List<FuncNode> PredefinedFunctions { get; set; }
        /// <summary>
        /// The parent scope of the current scope
        /// </summary>
        /// <value>Null if current scope is global scope. Else the parent</value>
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
        /// <summary>
        /// The type of the scope. This is set on creation
        /// </summary>
        /// <value>The type of the scope</value>
        public TokenType Type { get; set; } = TokenType.PROG;
        /// <summary>
        /// The name of the scope to search for, when looking for a child scope
        /// </summary>
        /// <value>A string representation of the scope</value>
        public string Name { get; set; }
        /// <summary>
        /// The constructor for a symboltable. This will set predefined functions and constants
        /// </summary>
        public SymbolTableObject()
        {
            SymbolTableBuilder.TopOfScope.Push(this);
            if (this.Parent == null)
            {
                AddPredefinedFunctions();
                AddPredefinedConstants();
            }
        }
        /// <summary>
        /// Adds most predefined functions to the program
        /// </summary>
        private void AddPredefinedFunctions()
        {
            PredefinedFunctions = new List<FuncNode>();
            ScannerToken predefinedToken = new ScannerToken(TokenType.FUNC, "", 0, 0);
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("min", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("a", predefinedToken), new VarNode("b", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("max", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("a", predefinedToken), new VarNode("b", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("abs", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("x", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("constrain", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("amt", predefinedToken), new VarNode("low", predefinedToken), new VarNode("high", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("round", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("x", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("radians", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("deg", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("degrees", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("rad", predefinedToken) } });
            PredefinedFunctions.Add(new FuncNode(0, 0) { Name = new VarNode("sq", predefinedToken), SymbolType = new TypeContext(TokenType.NUMERIC), FunctionParameters = new List<VarNode>() { new VarNode("x", predefinedToken) } });
        }
        /// <summary>
        /// Adds most predefined constants to the program
        /// </summary>
        private void AddPredefinedConstants()
        {
            this.Symbols.Add(new Symbol("PI", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "PI", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
            this.Symbols.Add(new Symbol("HALF_PI", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "HALF_PI", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
            this.Symbols.Add(new Symbol("TWO_PI", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "TWO_PI", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
            this.Symbols.Add(new Symbol("DEG_TO_RAD", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "DEG_TO_RAD", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
            this.Symbols.Add(new Symbol("RAD_TO_DEG", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "RAD_TO_DEG", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
            this.Symbols.Add(new Symbol("EULER", TokenType.NUMERIC, false, new NumericNode("0", new ScannerToken(TokenType.NUMERIC, "EULER", 0, 0) {SymbolicType = new TypeContext(TokenType.NUMERIC){IsFloat = true}})));
        }
        /// <inheritdoc cref="System.Object.ToString()"/>
        public override string ToString() => $"{Name}";
        /// <summary>
        /// This method will check if a given scope has declared a variable.
        /// </summary>
        /// <param name="node">The node to look for</param>
        /// <returns></returns>
        public bool HasDeclaredVar(AstNode node)
        {
            if (node.IsType(typeof(VarNode)))
            {
                return this.DeclaredVars.Contains((node as VarNode).Id) || (this.Parent?.HasDeclaredVar(node) ?? false);
            }
            else if (node.IsType(typeof(ArrayAccessNode)))
            {
                return this.DeclaredArrays.Contains(((ArrayAccessNode)node).Actual) || (this.Parent?.HasDeclaredVar(node) ?? false);
            }
            new SymbolNotFoundException($"The provided symbol was never declared. Error at {node.Line}:{node.Offset}");
            return false;
        }

        /// <summary>
        /// This method try to find a variable recursively, in the current scope and its parents
        /// </summary>
        /// <param name="var">the variable to look for</param>
        /// <returns>Null if the variable is not found. Else the variable</returns>
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
        /// <summary>
        /// This method will update a type definition for a variable in all scopes
        /// </summary>
        /// <param name="leftHand">The variable to update</param>
        /// <param name="rhs">The typecontext to assign it</param>
        /// <param name="scopeName">The name of the calling scope</param>
        /// <param name="goback">A bool value checking if the child scopes should be updated</param>
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
        /// <summary>
        /// This method will update function parameters of an enclosing function
        /// </summary>
        /// <param name="node">The node to look for</param>
        /// <param name="rhs">The typecontext to assign the variable</param>
        /// <param name="scopeName">The name of the calling scope</param>
        public void UpdateFuncVar(VarNode node, TypeContext rhs, string scopeName)
        {
            SymbolTableObject symobj = SymbolTableBuilder.GlobalSymbolTable.Children.First(symtab => symtab.Name == scopeName);

            FuncNode func = SymbolTableObject.FunctionDefinitions.First(fn => this.Name.Contains(fn.Name.Id) && this.Name.Contains("_" + fn.Line));
            if (func.FunctionParameters.Any(vn => vn.Id == node.Id))
                node.Declaration = false;
            symobj.UpdateTypedef(node, rhs, scopeName, false);
        }
        /// <summary>
        /// Check if a given scope is a child of a function.
        /// </summary>
        /// <returns>True if the scope is a child of a function. Else false</returns>
        public bool IsInFunction()
        {
            SymbolTableObject symtab = this;
            while (this.Parent != null && (!this.Parent.Name?.StartsWith("func_") ?? false))
            {
                symtab = this.Parent;
            }
            return (symtab.Parent?.Name?.StartsWith("func_") ?? false) || (this.Name?.StartsWith("func_") ?? false);
        }
        /// <summary>
        /// Gets the enclosing function scope, of the current scope
        /// </summary>
        /// <returns>The enclosing function of the current scope. Else null</returns>
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
        /// <summary>
        /// This method will find a child scope given the name of it
        /// </summary>
        /// <param name="name">The name of the scope</param>
        /// <returns>The scope found, if found. Else null</returns>
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
        /// <summary>
        /// This method finds an array in the declared array of the current scope.
        /// </summary>
        /// <param name="arrName">The name of the array to look for</param>
        /// <returns>The found array, if found. Else null with an error</returns>
        public ArrayNode FindArray(string arrName)
        {
            if (this.Parent != null && this.Parent.FindArray(arrName) != null)
            {
                return this.Parent.FindArray(arrName);
            }
            else if (this.DeclaredArrays.Any(sym => sym.ActualId.Id == arrName))
            {
                return this.DeclaredArrays.First(sym => sym.ActualId.Id == arrName);
            }
            else
            {
                new SymbolNotFoundException($"The array '{arrName}' was not found");
                return null;
            }
        }
    }
}

