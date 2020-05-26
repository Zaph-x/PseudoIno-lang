using System.Runtime.CompilerServices;
using System;
using System.Reflection.Metadata;
using AbstractSyntaxTree.Objects.Nodes;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Lexer.Objects;
using static Lexer.Objects.TokenType;
using SymbolTable;
using Contextual_analysis.Exceptions;

namespace Contextual_analysis
{
    /// <summary>
    /// The type checker of the PseudoIno compiler. Here types are checked and held against eachother.
    /// </summary>
    public class TypeChecker : Visitor
    {
        /// <summary>
        /// The global symbol table. This will never change
        /// </summary>
        private SymbolTableObject GlobalScope = SymbolTableBuilder.GlobalSymbolTable;
        /// <summary>
        /// The current symbol table. This will change when the scope is changed.
        /// </summary>
        private SymbolTableObject CurrentScope = SymbolTableBuilder.GlobalSymbolTable;
        /// <summary>
        /// A temporary scope, used when returning from a function.
        /// </summary>
        private SymbolTableObject _temp;
        /// <summary>
        /// A static bool value signifying an error has been encountered
        /// </summary>
        /// <value>True if an error has been encountered. Else false.</value>
        public static bool HasError { get; set; } = false;

        /// <summary>
        /// This method type checks the TimesNode node in the AST.
        /// </summary>
        /// <param name="timesNode">The node to check.</param>
        /// <returns>Returns OP_TIMES typecontext</returns>
        public override object Visit(TimesNode timesNode)
        {
            return new TypeContext(OP_TIMES);
        }
        /// <summary>
        /// This method type checks the AssignmentNode node in the AST.
        /// </summary>
        /// <param name="assignmentNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(AssignmentNode assignmentNode)
        {
            TypeContext lhs = (TypeContext)assignmentNode.LeftHand.Accept(this);
            TypeContext rhs;
            if (assignmentNode.RightHand.IsType(typeof(ArrayNode)))
            {
                ArrayNode arr = (ArrayNode)assignmentNode.RightHand;
                if (arr.HasBeenAccessed)
                {
                    if (lhs.Type == VAR)
                    {
                        arr.SymbolType = lhs = rhs = (TypeContext)arr.FirstAccess.RightHand.Accept(this);
                        assignmentNode.LeftHand.SymbolType = rhs;
                        assignmentNode.LeftHand.Type = rhs.Type;
                    }
                    else
                        rhs = (TypeContext)arr.FirstAccess.RightHand.Accept(this);
                }
                else
                {
                    new InvalidTypeException($"Array type never set. Error at {arr.Line}:{arr.Offset}");
                    return null;
                }
            }
            else
            {
                rhs = (TypeContext)assignmentNode.RightHand.Accept(this);
            }
            if (assignmentNode.RightHand.IsType(typeof(ArrayAccessNode)))
            {
                if (CurrentScope.FindArray((assignmentNode.LeftHand as ArrayAccessNode).Actual.ActualId.Id).Type == ARR)
                {
                    ArrayAccessNode node = (assignmentNode.LeftHand as ArrayAccessNode);
                    ArrayNode arr = CurrentScope.FindArray(node.Actual.ActualId.Id);
                    if (!(arr.Dimensions.Count >= node.Accesses.Count))
                    {
                        new OutOfRangeException($"Illegal access to {arr.Dimensions.Count} dimensional array. Error at {node.Line}:{node.Offset}");
                        return null;
                    }
                    if (arr.Dimensions.Count > node.Accesses.Count && node.Accesses.Count > 1)
                    {
                        AssignmentNode declaringStatement = new AssignmentNode(node.Line, node.Offset);
                        declaringStatement.LeftHand =
                            new VarNode($"protected_declaration_{arr.ActualId.Id}_{node.Accesses.Count - 1}", new ScannerToken(node.Type, $"protected_declaration_{arr.ActualId.Id}_{node.Accesses.Count - 1}", node.Line, node.Offset))
                            { IsArray = true, SymbolType = new TypeContext(node.Type) };
                        ArrayNode declaringArray = new ArrayNode(node.Line, node.Offset);
                        for (int i = 0; i < node.Accesses.Count; i++)
                        {
                            declaringArray.Dimensions.Add(arr.Dimensions[i]);
                        }
                        ((IScope)assignmentNode.Parent).Statements.Insert(((IScope)assignmentNode.Parent).Statements.IndexOf(assignmentNode), declaringStatement);


                    }
                    node.Actual.FirstAccess.LeftHand.SymbolType = rhs;
                    node.Actual.FirstAccess.LeftHand.Type = rhs.Type;
                    arr.SymbolType = rhs;
                    arr.Type = rhs.Type;
                    lhs = rhs;
                }
            }
            if (lhs.Type == VAR)
            {
                if (!CurrentScope.HasDeclaredVar(assignmentNode.LeftHand as AstNode))
                {
                    (assignmentNode.LeftHand as VarNode).Declaration = true;
                    CurrentScope.DeclaredVars.Add((assignmentNode.LeftHand as VarNode).Id);
                }

                if (assignmentNode.LeftHand.IsType(typeof(VarNode)))
                {
                    if (CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode).Type == VAR)
                    {
                        CurrentScope.UpdateTypedef(assignmentNode.LeftHand as VarNode, rhs, CurrentScope.Name, true);
                    }
                    lhs = CurrentScope.FindSymbol(assignmentNode.LeftHand as VarNode);
                }
                else if (assignmentNode.LeftHand.IsType(typeof(ArrayAccessNode)))
                {

                }

            }
            if (lhs.Type != rhs.Type)
            {
                if ((lhs.Type != DPIN && lhs.Type != APIN) || (rhs.Type != NUMERIC && rhs.Type != BOOL))
                {
                    new InvalidTypeException($"Type {rhs.Type} is not assignable toType {lhs.Type} at {assignmentNode.Line}:{assignmentNode.Offset}");
                }
            }


            return null;
        }
        /// <summary>
        /// This method type checks the WaitNode node in the AST.
        /// </summary>
        /// <param name="waitNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(WaitNode waitNode)
        {
            return null;
        }
        /// <summary>
        /// This method type checks the VarNode node in the AST.
        /// </summary>
        /// <param name="varNode">The node to check.</param>
        /// <returns>The type of the symbol in the current or global scope</returns>
        public override object Visit(VarNode varNode)
        {
            return (CurrentScope ?? GlobalScope).FindSymbol(varNode);
        }

        /// <summary>
        /// This method type checks the TimeSecondNode node in the AST.
        /// </summary>
        /// <param name="timeSecondNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeSecondNode timeSecondNode)
        {
            return null;
        }
        /// <summary>
        /// This method type checks the TimeMinutesNode node in the AST.
        /// </summary>
        /// <param name="timeMinuteNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            return null;
        }
        /// <summary>
        /// This method type checks the TimeMillisecondNode node in the AST.
        /// </summary>
        /// <param name="timeMillisecondNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            return null;
        }
        /// <summary>
        /// This method type checks the TimeHourNode node in the AST.
        /// </summary>
        /// <param name="timeHourNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeHourNode timeHourNode)
        {
            return null;
        }
        /// <summary>
        /// This method type checks the NumericNode node in the AST.
        /// </summary>
        /// <param name="numericNode">The node to check.</param>
        /// <returns>Numeric type context</returns>
        public override object Visit(NumericNode numericNode)
        {
            return numericNode.SymbolType;
        }

        /// <summary>
        /// This method type checks the EqualNode node in the AST.
        /// </summary>
        /// <param name="equalNode">The node to check.</param>
        /// <returns>Equal operator type context</returns>
        public override object Visit(EqualNode equalNode)
        {
            return new TypeContext(OP_EQUAL);

        }

        /// <summary>
        /// This method type checks the ProgramNode node in the AST.
        /// This is the entry point for the type checker
        /// </summary>
        /// <param name="programNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ProgramNode programNode)
        {

            programNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            programNode.FunctionDefinitons.ForEach(func =>
            {
                if (!(SymbolTableObject.FunctionDefinitions.Where(fn => fn.Name.Id == func.Name.Id && func.FunctionParameters.Count == fn.FunctionParameters.Count).Count() > 1))
                {
                    if (SymbolTableObject.FunctionCalls.Any(cn => cn.Id.Id == func.Name.Id && cn.Parameters.Count == func.FunctionParameters.Count))
                    {
                        CallNode cn = SymbolTableObject.FunctionCalls.First(cn => cn.Id.Id == func.Name.Id && cn.Parameters.Count == func.FunctionParameters.Count);
                        FuncNode declaredFunc = SymbolTableObject.FunctionDefinitions.First(fn => fn.Name.Id == func.Name.Id);
                        SymbolTableObject scope = GlobalScope.FindChild($"func_{declaredFunc.Name.Id}_{declaredFunc.Line}");
                        for (int i = 0; i < cn.Parameters.Count; i++)
                        {
                            if (cn.Parameters[i].SymbolType.Type == VAR)
                                continue;
                            scope.UpdateTypedef(declaredFunc.FunctionParameters[i], cn.Parameters[i].SymbolType, scope.Name, true);
                        }
                    }
                    func.Accept(this);
                }
                else
                {
                    new MultipleDefinedException($"A function '{func.Name.Id}' with {func.FunctionParameters.Count} parameters has already been defined. Error at {func.Line}:{func.Offset}");
                }
            });

            if (programNode.LoopFunction == null)
            {
                new NotDefinedException("Loop function was not defined.");
                return null;
            }
            programNode.LoopFunction.Accept(this);
            return null;
        }
        /// <summary>
        /// This method type checks the CallNode node in the AST.
        /// </summary>
        /// <param name="callNode">The node to check.</param>
        /// <returns>Type context of the function called</returns>
        public override object Visit(CallNode callNode)
        {
            try
            {
                if (SymbolTableObject.FunctionDefinitions.Any(func => func.Name.Id == callNode.Id.Id && func.FunctionParameters.Count == callNode.Parameters.Count))
                {
                    FuncNode n = SymbolTableObject.FunctionDefinitions.First(node => node.Name.Id == callNode.Id.Id && node.FunctionParameters.Count == callNode.Parameters.Count);
                    TypeContext ctx;
                    if (n.Statements.Any())
                    {
                        if (n.Statements.Last().IsType(typeof(ReturnNode)))
                        {
                            _temp = CurrentScope;
                            CurrentScope = GlobalScope.FindChild($"func_{n.Name.Id}_{n.Line}");
                            ctx = (TypeContext)n.Statements.Last().Accept(this);
                            CurrentScope = _temp;
                        }
                        else { ctx = new TypeContext(TokenType.FUNC); }
                    }
                    else
                    {
                        ctx = new TypeContext(TokenType.FUNC);
                    }
                    return ctx;
                }
                else if (SymbolTableObject.PredefinedFunctions.Any(func => func.Name.Id == callNode.Id.Id && func.FunctionParameters.Count == callNode.Parameters.Count))
                {
                    TypeContext ctx = SymbolTableObject.PredefinedFunctions.First(node => node.Name.Id == callNode.Id.Id && node.FunctionParameters.Count == callNode.Parameters.Count).SymbolType;
                    return ctx;
                }
                else
                {
                    new NotDefinedException($"A function '{callNode.Id.Id}' with {callNode.Parameters.Count} parameters has not been defined. Error at {callNode.Line}:{callNode.Offset} ");
                    return null;
                }
            }
            catch
            {
                new NotDefinedException($"Undefined function call at {callNode.Line}:{callNode.Offset}");
                return null;
            }
        }
        /// <summary>
        /// This method type checks the AndNode node in the AST.
        /// </summary>
        /// <param name="andNode">The node to check.</param>
        /// <returns>And operator type context</returns>
        public override object Visit(AndNode andNode)
        {
            return new TypeContext(OP_AND);

        }
        /// <summary>
        /// This method type checks the APinNode node in the AST.
        /// </summary>
        /// <param name="apinNode">The node to check.</param>
        /// <returns>APin type context</returns>
        public override object Visit(APinNode apinNode)
        {
            return new TypeContext(APIN);
        }
        /// <summary>
        /// This method type checks the DPinNode node in the AST.
        /// </summary>
        /// <param name="dpinNode">The node to check.</param>
        /// <returns>DPin type context</returns>
        public override object Visit(DPinNode dpinNode)
        {
            return new TypeContext(DPIN);
        }
        /// <summary>
        /// This method type checks the DivideNode node in the AST.
        /// </summary>
        /// <param name="divideNode">The node to check.</param>
        /// <returns>Divide operator type context</returns>
        public override object Visit(DivideNode divideNode)
        {
            return new TypeContext(OP_DIVIDE);
        }
        /// <summary>
        /// This method type checks the BinaryExpression node in the AST.
        /// </summary>
        /// <param name="expressionNode">The node to check.</param>
        /// <returns>The type context of the evaluated expression</returns>
        public override object Visit(BinaryExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
            if (lhs.Type == VAR)
            {
                if (opctx == null)
                {
                }
                else if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                lhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode);

            }
            if (rhs?.Type == VAR)
            {
                if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                rhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode);

            }
            if (opctx == null)
            {
                return expressionNode.SymbolType = lhs;
            }
            if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_LEQ, OP_GEQ, OP_LESS, OP_GREATER, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, BOOL) && IsOfTypes(rhs, BOOL) && IsOfTypes(opctx, OP_EQUAL, OP_AND, OP_OR))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_PLUS, OP_MODULO, OP_MINUS, OP_TIMES, OP_DIVIDE))
            {
                return expressionNode.SymbolType = new TypeContext(NUMERIC) { IsFloat = (lhs.IsFloat || rhs.IsFloat) };
            }
            else if (IsOfTypes(opctx, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            if (IsOfTypes(rhs, BOOL) && lhs.Type == expressionNode.RightHand.LeftHand.SymbolType.Type)
            {
                return expressionNode.SymbolType = rhs;
            }
            new InvalidTypeException($"Expression {lhs} {opctx} {rhs} is invalid (types) at {expressionNode.Line}:{expressionNode.Offset}");
            return null;
        }
        /// <summary>
        /// This method type checks the ParenthesisExpression node in the AST.
        /// </summary>
        /// <param name="expressionNode">The node to check.</param>
        /// <returns>The type context of the evaluated expression</returns>
        public override object Visit(ParenthesisExpression expressionNode)
        {
            TypeContext lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            TypeContext rhs = (TypeContext)expressionNode.RightHand?.Accept(this);
            TypeContext opctx = (TypeContext)expressionNode.Operator?.Accept(this);
            if (lhs.Type == VAR)
            {
                if (opctx == null) { }
                else if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                lhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.LeftHand).LeftHand as VarNode);

            }
            if (rhs?.Type == VAR)
            {
                if (IsOfTypes(opctx, OP_LESS, OP_LEQ, OP_GREATER, OP_GEQ, OP_PLUS, OP_MINUS, OP_DIVIDE, OP_TIMES, OP_MODULO))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(NUMERIC), CurrentScope.Name, true);
                }
                else if (IsOfTypes(opctx, OP_AND, OP_OR))
                {
                    CurrentScope.UpdateTypedef(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode, new TypeContext(BOOL), CurrentScope.Name, true);
                }
                rhs = CurrentScope.FindSymbol(((ExpressionTerm)expressionNode.RightHand.LeftHand).LeftHand as VarNode);

            }
            if (rhs == null && opctx == null)
            {
                return expressionNode.SymbolType = lhs;
            }
            if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_LEQ, OP_GEQ, OP_LESS, OP_GREATER, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, BOOL) && IsOfTypes(rhs, BOOL) && IsOfTypes(opctx, OP_EQUAL, OP_AND, OP_OR))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            else if (IsOfTypes(lhs, NUMERIC) && IsOfTypes(rhs, NUMERIC) && IsOfTypes(opctx, OP_PLUS, OP_MODULO, OP_MINUS, OP_TIMES, OP_DIVIDE))
            {
                return expressionNode.SymbolType = new TypeContext(NUMERIC) { IsFloat = (lhs.IsFloat || rhs.IsFloat) };
            }
            else if (IsOfTypes(opctx, OP_EQUAL))
            {
                return expressionNode.SymbolType = new TypeContext(BOOL);
            }
            if (IsOfTypes(rhs, BOOL) && lhs.Type == expressionNode.RightHand.LeftHand.SymbolType.Type)
            {
                return expressionNode.SymbolType = rhs;
            }
            new InvalidTypeException($"Expression {lhs} {opctx} {rhs} is invalid (types) at {expressionNode.Line}:{expressionNode.Offset}");
            return null;
        }
        /// <summary>
        /// This method type checks the ExpressionTerm node in the AST.
        /// </summary>
        /// <param name="expressionNode">The node to check.</param>
        /// <returns>The type context of the term</returns>
        public override object Visit(ExpressionTerm expressionNode)
        {
            TypeContext lhs;
            if (expressionNode.LeftHand.IsType(typeof(ArrayAccessNode)))
            {
                lhs = (TypeContext)CurrentScope.FindArray(((ArrayAccessNode)expressionNode.LeftHand).Actual.ActualId.Id).Accept(this);
                return expressionNode.SymbolType = lhs;
            }
            lhs = (TypeContext)expressionNode.LeftHand.Accept(this);
            if (lhs.Type == VAR)
            {
                lhs = CurrentScope.FindSymbol(expressionNode.LeftHand as VarNode);
            }
            return expressionNode.SymbolType = lhs;
        }
        /// <summary>
        /// This method type checks the ForNode node in the AST.
        /// </summary>
        /// <param name="forNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ForNode forNode)
        {

            CurrentScope = GlobalScope.FindChild($"LOOPF_{forNode.Line}");
            TypeContext fromType = (TypeContext)forNode.From.Accept(this);
            TypeContext toType = (TypeContext)forNode.To.Accept(this);
            if (null == CurrentScope.FindSymbol(forNode.CountingVariable)) CurrentScope.Symbols.Add(new Symbol(forNode.CountingVariable.Id, NUMERIC, false, forNode.CountingVariable));
            else CurrentScope.UpdateTypedef(forNode.CountingVariable, new TypeContext(TokenType.NUMERIC) { IsFloat = false }, CurrentScope.Name, true);
            if (fromType.Type != toType.Type)
                new InvalidTypeException($"Mismatch in range types at {forNode.Line}:{forNode.Offset}");
            forNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the FuncNode node in the AST.
        /// </summary>
        /// <param name="funcNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(FuncNode funcNode)
        {
            CurrentScope = GlobalScope.FindChild($"func_{funcNode.Name.Id}_{funcNode.Line}");
            funcNode.Statements.ForEach(stmnt =>
            {
                if (stmnt is CallNode)
                {
                    if (((CallNode)stmnt).Id.Id == funcNode.Name.Id)
                    {
                        new InvalidOperationException($"Illegal recursion at {stmnt.Line}:{stmnt.Offset}");
                    }
                }
                stmnt.Accept(this);
            });
            if (funcNode.Statements.Any() && funcNode.Statements.Last().Type == TokenType.RETURN)
            {
                {
                    funcNode.SymbolType = (TypeContext)funcNode.Statements.Last().Accept(this);
                    CurrentScope = CurrentScope.Parent ?? GlobalScope;
                    return funcNode.SymbolType;
                }
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the GreaterNode node in the AST.
        /// </summary>
        /// <param name="greaterNode">The node to check.</param>
        /// <returns>Greater operator type context</returns>
        public override object Visit(GreaterNode greaterNode)
        {
            return new TypeContext(OP_GREATER);
        }
        /// <summary>
        /// This method type checks the IfStatementNode node in the AST.
        /// </summary>
        /// <param name="ifStatementNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(IfStatementNode ifStatementNode)
        {
            CurrentScope = GlobalScope.FindChild($"{ifStatementNode.Type}_{ifStatementNode.Line}");
            if (((TypeContext)ifStatementNode.Expression.Accept(this)).Type == BOOL)
            {
                ifStatementNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"If statement expected a boolean expression at {ifStatementNode.Line}:{ifStatementNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the LessNode node in the AST.
        /// </summary>
        /// <param name="lessNode">The node to check.</param>
        /// <returns>Less operator type context</returns>
        public override object Visit(LessNode lessNode)
        {
            return new TypeContext(OP_LESS);
        }
        /// <summary>
        /// This method type checks the PlusNode node in the AST.
        /// </summary>
        /// <param name="plusNode">The node to check.</param>
        /// <returns>Plus operator type context</returns>
        public override object Visit(PlusNode plusNode)
        {
            return new TypeContext(OP_PLUS);
        }
        /// <summary>
        /// This method type checks the MinusNode node in the AST.
        /// </summary>
        /// <param name="minusNode">The node to check.</param>
        /// <returns>Minus operator type context</returns>
        public override object Visit(MinusNode minusNode)
        {
            return new TypeContext(OP_MINUS);
        }
        /// <summary>
        /// This method type checks the ModuloNode node in the AST.
        /// </summary>
        /// <param name="moduloNode">The node to check.</param>
        /// <returns>Modulo operator type context</returns>
        public override object Visit(ModuloNode moduloNode)
        {
            return new TypeContext(OP_MODULO);
        }
        /// <summary>
        /// This method type checks the OrNode node in the AST.
        /// </summary>
        /// <param name="orNode">The node to check.</param>
        /// <returns>Or operator type context</returns>
        public override object Visit(OrNode orNode)
        {
            return new TypeContext(OP_OR);
        }
        /// <summary>
        /// This method type checks the StringNode node in the AST.
        /// </summary>
        /// <param name="stringNode">The node to check.</param>
        /// <returns>String type context</returns>
        public override object Visit(StringNode stringNode)
        {
            return new TypeContext(STRING);
        }
        /// <summary>
        /// This method type checks the WhileNode node in the AST.
        /// </summary>
        /// <param name="whileNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(WhileNode whileNode)
        {
            CurrentScope = GlobalScope.FindChild($"LOOPW_{whileNode.Line}");
            if (((TypeContext)whileNode.Expression.Accept(this)).Type == BOOL)
            {
                whileNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"While statement expected a boolean expression at {whileNode.Line}:{whileNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the ElseStatementNode node in the AST.
        /// </summary>
        /// <param name="elseStatement">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ElseStatementNode elseStatement)
        {
            CurrentScope = GlobalScope.FindChild($"ELSESTMNT_{elseStatement.Line}");
            elseStatement.Statements.ForEach(stmnt => stmnt.Accept(this));
            CurrentScope = CurrentScope?.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the ElseIfStatementNode node in the AST.
        /// </summary>
        /// <param name="elseifStatementNode">The node to check.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            CurrentScope = GlobalScope.FindChild($"{elseifStatementNode.Type}_{elseifStatementNode.Line}");
            if (((TypeContext)elseifStatementNode.Expression.Accept(this)).Type == BOOL)
            {
                elseifStatementNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            else
            {
                new InvalidTypeException($"Else if statement expected a boolean expression at {elseifStatementNode.Line}:{elseifStatementNode.Offset}");
            }
            CurrentScope = CurrentScope.Parent ?? GlobalScope;
            return null;
        }
        /// <summary>
        /// This method type checks the ReturnNode node in the AST.
        /// </summary>
        /// <param name="returnNode">The node to check.</param>
        /// <returns>The type of the evaluated return expression</returns>
        public override object Visit(ReturnNode returnNode)
        {
            return returnNode.ReturnValue.Accept(this);
        }
        /// <summary>
        /// This method type checks the GreaterOrEqualNode node in the AST.
        /// </summary>
        /// <param name="greaterNode">The node to check.</param>
        /// <returns>Greater or Equal operator type context</returns>
        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            return new TypeContext(OP_GEQ);
        }
        /// <summary>
        /// This method type checks the LessOrEqualNode node in the AST.
        /// </summary>
        /// <param name="lessNode">The node to check.</param>
        /// <returns>Less or Equal operator type context</returns>
        public override object Visit(LessOrEqualNode lessNode)
        {
            return new TypeContext(OP_LEQ);
        }
        /// <summary>
        /// This method type checks the BoolNode node in the AST.
        /// </summary>
        /// <param name="boolNode">The node to check.</param>
        /// <returns>Bool type context</returns>
        public override object Visit(BoolNode boolNode)
        {
            return new TypeContext(BOOL);
        }
        /// <summary>
        /// This method compares a type context to a list of tokentypes
        /// </summary>
        /// <param name="ctx">The context to compare</param>
        /// <param name="types">An array of types to compare to</param>
        /// <returns></returns>
        public bool IsOfTypes(TypeContext ctx, params TokenType[] types)
        {
            return types.Any(t => t == ctx.Type);
        }
        /// <summary>
        /// This method type checks the ArrayNode node in the AST.
        /// </summary>
        /// <param name="arrayNode">The node to check.</param>
        /// <returns>Typecontext of the array if it has been access. Else null</returns>
        public override object Visit(ArrayNode arrayNode)
        {
            if (arrayNode.HasBeenAccessed)
                return arrayNode.SymbolType;
            else
                return null;
        }
        /// <summary>
        /// This method type checks the ArrayAccessNode node in the AST.
        /// </summary>
        /// <param name="arrayAccess">The node to check.</param>
        /// <returns>The type of the accessed array</returns>
        public override object Visit(ArrayAccessNode arrayAccess)
        {
            int accessLocation = 0;
            foreach (int access in arrayAccess.Accesses.Where(n => n.IsType(typeof(NumericNode))).Select(n => ((NumericNode)n).IValue))
            {
                if (arrayAccess.Actual.Dimensions[accessLocation++].IValue <= access)
                {
                    new OutOfRangeException($"Illegal access to array '{arrayAccess.Actual.ActualId.Id}'. Access out of range. Error at {arrayAccess.Line}:{arrayAccess.Offset}");
                }
            }
            return (CurrentScope ?? GlobalScope).FindArray(arrayAccess.Actual.ActualId.Id).Accept(this);
        }
    }
}
