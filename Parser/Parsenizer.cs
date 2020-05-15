using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
using Parser.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Objects;
using static Lexer.Objects.TokenType;
using SymbolTable;
using Lexer.Exceptions;

namespace Parser
{
    public class Parsenizer
    {
        int Index = 1;
        private Stack<TokenType> Stack = new Stack<TokenType>();
        // private TokenStream TokenStream;
        List<ScannerToken> Tokens = new List<ScannerToken>();
        public ParseTable ParseTable { get; private set; }
        private int CurrentLine;
        private int CurrentOffset;
        private List<TokenType> _p;
        public static bool HasError { get; set; } = false;
        public Stack<AstNode> Scopes = new Stack<AstNode>();
        private AstNode Current;
        public ProgramNode Root { get; private set; }
        private ParseAction CurrentAction { get; set; }
        private SymbolTableObject _symbolTabelGlobal = new SymbolTableObject();
        private SymbolTableBuilder _builder { get; }
        private int _exprState = 0;
        private Stack<int> RuleStack = new Stack<int>();
        public Parsenizer(List<ScannerToken> tokens)
        {
            Tokens = tokens.Where(tok => tok.Type != TokenType.COMMENT && tok.Type != TokenType.MULT_COMNT).ToList();
            ParseTable = new ParseTable();
            ParseTable.InitTable();
            _builder = new SymbolTableBuilder(_symbolTabelGlobal);
        }

        private TokenType TopOfStack()
        {
            if (Stack.TryPeek(out TokenType token))
            {
                return token;
            }
            new InvalidTokenException("Expected stack not empty but was empty");
            return TokenType.ERROR;
        }

        public void Parse(out string verbosity)
        {
            // AstNode.ShowVal = true;
            Stack = new Stack<TokenType>();
            verbosity = "";
            Stack.Push(TokenType.EOF);
            Stack.Push(TokenType.PROG);
            Index = 0;
            Root = new ProgramNode(0, 0);
            Scopes.Push(Root);

            // verbosity += $"TS: {Tokens[Index]} TSPeek: {Tokens[Index+1]} TOS: {TopOfStack()}\n";
            while (Stack.Any() && Index < Tokens.Count)
            {
                TokenType top = Stack.Pop();
                TokenType token = Tokens[Index].Type;
                CurrentLine = Tokens[Index].Line;
                if (token == OP_RPAREN)
                    System.Console.WriteLine("RPAREN");
                CurrentOffset = Tokens[Index].Offset;
                verbosity += $"Token: {token} Top: {TopOfStack()}".PadRight(35, ' ');
                if (TokenTypeExpressions.IsTerminal(top))
                {
                    if (top == token)
                    {
                        DecorateAstNode(Tokens[Index]);
                        Index++;

                    }
                    else
                    {
                        new InvalidTokenException($"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})");
                        verbosity += $"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})";
                        break;
                    }
                }
                else
                {
                    CurrentAction = ParseTable[top, token];
                    AddToAstNode(top);
                    var rule = CurrentAction.Product;
                    if (rule.Count > 0 && rule.First() == TokenType.ERROR)
                    {
                        new InvalidTokenException($"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})");
                        verbosity += $"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})";
                        break;
                    }
                    for (int i = rule.Count - 1; i >= 0; i--)
                        Stack.Push(rule[i]);
                }
                verbosity += $"\n";
                if (token == TokenType.EOF || Index == Tokens.Count)
                { verbosity += "Input Accepted."; }
            }
            if (HasError) return;
            Clean(Root);
            HasError = false;
        }

        private void Clean(IScope node)
        {
            for (int i = 0; i < node.Statements.Count; i++)
            {
                StatementNode n = node.Statements[i];
                if (n is IScope)
                {
                    if (((IScope)n).Statements.Any())
                        Clean((IScope)n);
                    else
                    {
                        node.Statements.Remove(n);
                        i--;
                    }
                }
            }
        }

        private AstNode TopScope()
        {
            if (Scopes.TryPeek(out AstNode node))
            {
                return node;
            }
            new NoScopeException("No scopes left on scope stack.");
            return null;
        }

        public void DecorateAstNode(ScannerToken token)
        {
            AstNode symbolNode;
            ScannerToken previous = Tokens[0];
            if (Index > 0)
                previous = Tokens[Index - 1];
            if (Current == null)
                return;
            switch (CurrentAction.Type)
            {
                case 118:
                case 119:
                    if (token.Type == VAR)
                        ((FuncNode)Current).FunctionParameters.Add(new VarNode(token.Value, token));
                    break;
                case 30 when token.Type == VAR:
                    {
                        symbolNode = new VarNode(token.Value, token) { SymbolType = new TypeContext(VAR) };
                        ((AssignmentNode)Current).LeftHand = (ITerm)symbolNode;
                        ((AssignmentNode)Current).RightHand = new BinaryExpression(token);
                        _builder.AddSymbol(symbolNode);
                        break;
                    }
                case 31 when token.Type == APIN:
                    {
                        symbolNode = new APinNode(token.Value, token) { SymbolType = new TypeContext(NUMERIC) };
                        ((AssignmentNode)Current).LeftHand = (ITerm)symbolNode;
                        ((AssignmentNode)Current).RightHand = new BinaryExpression(token);
                        _builder.AddSymbol(symbolNode);
                        break;
                    }
                case 32 when token.Type == DPIN:
                    {
                        symbolNode = new DPinNode(token.Value, token) { SymbolType = new TypeContext(BOOL) };
                        ((AssignmentNode)Current).LeftHand = (ITerm)symbolNode;
                        ((AssignmentNode)Current).RightHand = new BinaryExpression(token);
                        _builder.AddSymbol(symbolNode);
                        break;
                    }
                case 116 when token.Type == VAR:
                    ((FuncNode)TopScope()).Name = new VarNode(token.Value, token);
                    break;
                case 126:
                    ((WhileNode)TopScope()).Expression = new BinaryExpression(token.Line, token.Offset);
                    break;

                #region Assignables
                case 101:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        expr.LeftHand = new NumericNode(token.Value, token);
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new NumericNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 102:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new VarNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new VarNode(token.Value, token);
                        if (((IExpr)Current).LeftHand != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = (ExpressionNode)term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new VarNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new VarNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 103:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new APinNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new APinNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new APinNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new APinNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 104:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new DPinNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                        ((IExpr)Current).LeftHand = new DPinNode(token.Value, token);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new DPinNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new DPinNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 106:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new StringNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                        ((IExpr)Current).LeftHand = new StringNode(token.Value, token);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new StringNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new DPinNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 107 when token.Type == NUMERIC:
                    token.Value = "-" + token.Value;
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new NumericNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                        ((IExpr)Current).LeftHand = new NumericNode(token.Value, token);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new NumericNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 105:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(token);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = new BoolNode(token.Value, token);
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new BoolNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) {Parent = (ExpressionNode)Current, LeftHand = term};
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                #endregion Assignables
                #region Expressions
                // EXPRESSIONS
                case 74:
                    ExpressionNode expr74 = new ParenthesisExpression(token.Line, token.Offset);
                    if (Current.Type == ASSIGNMENT)
                    {
                        ExpressionNode assignmentExpr = new BinaryExpression(token.Line, token.Offset);
                        ((IExpr)assignmentExpr).LeftHand = expr74;
                        expr74.Parent = assignmentExpr;
                        ((IExpr)((AssignmentNode)Current).RightHand).LeftHand = assignmentExpr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        expr74.Parent = (ExpressionNode)Current;
                        DifferLHSAndRHSExpr(expr74, (ExpressionNode)Current);
                    }
                    else if (Current.Type == RETURN)
                        ((IExpr)((ReturnNode)Current).ReturnValue).LeftHand = expr74;
                    else if (Current.Type == IFSTMNT)
                    {
                        if (((IExpr)((IfStatementNode)Current).Expression).LeftHand != null)
                        {
                            ((IExpr)((IfStatementNode)Current).Expression).RightHand = (ExpressionNode)expr74;
                            expr74.Parent = ((IfStatementNode)Current).Expression;
                            Current = expr74;
                        }
                        else
                        {
                            ((IExpr)((IfStatementNode)Current).Expression).LeftHand = (ExpressionNode)expr74;
                            expr74.Parent = (ExpressionNode)Current;
                            Current = expr74;
                        }

                    }
                    else if (Current.Type == WHILE)
                    {
                        expr74.Parent = ((WhileNode)Current).Expression;
                        ((WhileNode)Current).Expression = expr74;
                    }
                    Current = (AstNode)expr74;
                    break;
                #endregion Expressions
                #region Operators
                // OPERATORS
                case 78:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new MinusNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new MinusNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new MinusNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new MinusNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                    }
                    break;
                case 87:

                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new TimesNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new TimesNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new TimesNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new TimesNode(token);
                        ((WhileNode)Current).SymbolType = new TypeContext(NUMERIC);
                    }
                    break;
                case 88:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new DivideNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new DivideNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new DivideNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new DivideNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                    }
                    break;
                case 86:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new ModuloNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new ModuloNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new ModuloNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new ModuloNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                    }
                    break;
                case 85:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new PlusNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new PlusNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new PlusNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new PlusNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                    }
                    break;
                case 96:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new LessNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                ((IExpr)Current).Operator = new LessNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new LessNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new LessNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    break;
                case 95:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new OrNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = ((ExpressionNode)Current).Parent;
                                ((IExpr)Current).Operator = new OrNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new OrNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new OrNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == IFSTMNT)
                    {
                        ((IExpr)((IfStatementNode)Current).Expression).Operator = new OrNode(token);
                        ((IExpr)((IfStatementNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    break;
                case 94:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new AndNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                ((IExpr)Current).Operator = new AndNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new AndNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new AndNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    break;
                case 97:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new GreaterNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                ((IExpr)Current).Operator = new GreaterNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new GreaterNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new GreaterNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    break;
                case 98:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new EqualNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                ((IExpr)Current).Operator = new EqualNode(token);
                                ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new EqualNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new EqualNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                    }
                    break;
                #endregion Operators
                case 115:
                    if (token.Type == VAR)
                    {
                        ((CallNode)Current).Id = new VarNode(token.Value, token);
                        break;
                    }

                    if (Current == null)
                    {
                        CallNode node = new CallNode(CurrentLine, CurrentOffset);
                        Root.Statements.Add(node);
                        Current = node;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        if (((AssignmentNode)Current).RightHand.LeftHand != null)
                        {
                            Current = new CallNode(CurrentLine, CurrentOffset);
                            ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                            break;
                        }
                        CallNode node = new CallNode(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (IExpr)node;
                        Current = node;
                    }
                    else
                    {
                        Current = new CallNode(CurrentLine, CurrentOffset);
                        ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    }
                    break;

                case 123:
                    if (token.Type == VAR)
                        ((ForNode)Current).CountingVariable = new VarNode(token.Value, token);
                    break;
                case 128:
                    if (token.Type == NUMERIC)
                        if (((ForNode)Current).From == null)
                            ((ForNode)Current).From = new NumericNode(token.Value, token);
                        else if (((ForNode)Current).To == null)
                            ((ForNode)Current).To = new NumericNode(token.Value, token);
                    break;
                case 129:
                    if (token.Type == NUMERIC)
                        ((WaitNode)Current).TimeAmount = new NumericNode(token.Value, token);
                    break;
                case 130:
                    ((WaitNode)Current).TimeModifier = new TimeHourNode(token);
                    break;
                case 131:
                    ((WaitNode)Current).TimeModifier = new TimeMinuteNode(token);
                    break;
                case 132:
                    ((WaitNode)Current).TimeModifier = new TimeSecondNode(token);
                    break;
                case 133:
                    ((WaitNode)Current).TimeModifier = new TimeMillisecondNode(token);
                    break;
                case 90037:
                case 90014:
                    Current = Current.Parent;
                    break;
                default:
                    break;
            }
        }

        // TODO Dette skal opdateres i rapporten
        public void AddToAstNode(TokenType token)
        {

            if (CurrentAction == null) return;
            ScannerToken previous = Tokens[0];
            if (Index > 0)
                previous = Tokens[Index - 1];
            switch (CurrentAction.Type)
            {
                case 129:
                    Current = new WaitNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case 100:
                    if (((ExpressionNode)Current).Operator != null)
                        ((ExpressionNode)Current).Operator = GetOrEqualNode(((IExpr)Current).Operator);
                    break;
                case 30:
                case 31:
                case 32:
                    Current = new AssignmentNode(CurrentLine, CurrentOffset);
                    // ((AssignmentNode)Current).RightHand = new ExpressionTerm()
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case 126:
                    Current = new WhileNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    _builder.OpenScope(token, $"{token}_{CurrentLine}");
                    Scopes.Push(Current);
                    break;
                case 123:
                    Current = new ForNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    _builder.OpenScope(token, $"{token}_{CurrentLine}");
                    Scopes.Push(Current);
                    break;
                case 116:
                    Current = new FuncNode(CurrentLine, CurrentOffset);
                    ((ProgramNode)TopScope()).FunctionDefinitons.Add((FuncNode)Current);
                    try
                    {
                        _builder.OpenScope(token, $"func_{Tokens[Index + 1].Value}");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        new InvalidProgramException($"Function definitions must have a function name at {CurrentLine}:{CurrentOffset}");
                    }
                    Scopes.Push(Current);
                    break;
                case 111:
                    ExpressionNode expr111 = new BinaryExpression(CurrentLine, CurrentOffset);
                    Current = new IfStatementNode(CurrentLine, CurrentOffset) { Expression = expr111 };
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    _builder.OpenScope(token, $"{token}_{CurrentLine}");
                    Scopes.Push(Current);
                    Current = ((IfStatementNode)Current).Expression;
                    break;
                case 113:
                    Scopes.Pop();
                    Current = new ElseStatementNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    _builder.CloseScope();
                    _builder.OpenScope(token, $"{token}_{CurrentLine}");
                    Scopes.Push(Current);
                    break;
                case 114:
                    Scopes.Pop();
                    Current = new ElseifStatementNode(CurrentLine, CurrentOffset);
                    ExpressionNode expr114 = new BinaryExpression(CurrentLine, CurrentOffset);
                    ((ElseifStatementNode)Current).Expression = expr114;
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    _builder.CloseScope();
                    _builder.OpenScope(token, $"{token}_{CurrentLine}");
                    Scopes.Push(Current);
                    Current = expr114;
                    break;
                case 124:
                case 127:
                case 112:
                case 117:
                    _builder.CloseScope();
                    IScope endingNode = (IScope)Scopes.Pop();
                    Current = TopScope();
                    if (endingNode is FuncNode)
                        if (((FuncNode)endingNode).Name.Id == "loop")
                        {
                            Root.FunctionDefinitons.Remove((FuncNode)endingNode);
                            Root.LoopFunction = (FuncNode)endingNode;
                        }
                    break;

                case 120:
                    Current = TopScope();
                    ReturnNode retNode = new ReturnNode(CurrentLine, CurrentOffset);
                    IExpr expr120 = new BinaryExpression(CurrentLine, CurrentOffset);
                    retNode.ReturnValue = (ExpressionNode)expr120;
                    ((FuncNode)Current).Statements.Add(retNode);
                    Current = (ExpressionNode)expr120;
                    break;

                default:
                    return;
            }
        }

        private void DifferLHSAndRHSExpr(ExpressionNode node, ExpressionNode current)
        {
            if (((IExpr)current).LeftHand == null)
            {
                ((IExpr)current).LeftHand = (ExpressionNode)node;
                current = node;
            }
            else
            {
                if (((IExpr)current).RightHand == null)
                {
                    ((IExpr)current).RightHand = (ExpressionNode)node;
                    current = node;
                }
                else
                {
                    if (node.Parent.GetType().IsSubclassOf(typeof(ExpressionNode)))
                        DifferLHSAndRHSExpr(node, node.Parent);
                }
            }
        }

        public OperatorNode GetOrEqualNode(OperatorNode node)
        {
            switch (node.Type)
            {
                case OP_GREATER:
                    return new GreaterOrEqualNode(node);
                case OP_LESS:
                    return new LessOrEqualNode(node);
                default:
                    return null;
            }
        }
    }
}