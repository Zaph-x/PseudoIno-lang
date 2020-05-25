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
    /// <summary>
    /// This is the main class for the parser. Here be parsing.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// The current index in the token stream, when parsing.
        /// </summary>
        int Index = 1;
        /// <summary>
        /// The stack of tokens used to parse.
        /// </summary>
        /// <returns>The top token in the parser</returns>
        private Stack<TokenType> Stack = new Stack<TokenType>();
        /// <summary>
        /// A list of scanner tokens from the scanner. This is passed from the scanner to the parser.
        /// </summary>
        /// <returns>The token to parse</returns>
        List<ScannerToken> Tokens = new List<ScannerToken>();
        /// <summary>
        /// The parse table used to parse.
        /// </summary>
        /// <value>Always set when the parser is created</value>
        public ParseTable ParseTable { get; private set; }
        /// <summary>
        /// The line of the token currently being parsed
        /// </summary>
        private int CurrentLine;
        /// <summary>
        /// The offset of the token currently being parsed
        /// </summary>
        private int CurrentOffset;
        /// <summary>
        /// A static boolean representing the error state of the parser.
        /// </summary>
        /// <value>True if an error has been encountered. Else false</value>
        public static bool HasError { get; set; } = false;
        /// <summary>
        /// The scope stack, to put items into the correct scopes.
        /// </summary>
        /// <returns>The innermost scope</returns>
        public Stack<AstNode> Scopes = new Stack<AstNode>();
        /// <summary>
        /// The current node in the AST
        /// </summary>
        private AstNode Current;
        /// <summary>
        /// The root of the AST
        /// </summary>
        /// <value>A program node</value>
        public ProgramNode Root { get; private set; }
        /// <summary>
        /// The current action to use in the parser. This has a number and a set of production rules to follow.
        /// </summary>
        /// <value>Determined by the current token, as well as the top of the token stack <see cref="ParseAction"/></value>
        private ParseAction CurrentAction { get; set; }
        /// <summary>
        /// The global symboltable used throughout the compiler.
        /// </summary>
        /// <returns>The global symboltable</returns>
        private SymbolTableObject _symbolTabelGlobal = new SymbolTableObject();
        /// <summary>
        /// The symboltable builder used to insert items into the symbol table
        /// </summary>
        /// <value>Set on parser construction</value>
        private SymbolTableBuilder _builder { get; }
        /// <summary>
        /// The constructor for the parser.
        /// Here the parsetable is created, along with the global scope being set up.
        /// Furthermore, the tokens are being passed from the scanner.
        /// </summary>
        /// <param name="tokens">The tokens that are to be parsed.</param>
        public Parser(List<ScannerToken> tokens)
        {
            Tokens = tokens.Where(tok => tok.Type != TokenType.COMMENT && tok.Type != TokenType.MULT_COMNT).ToList();
            ParseTable = new ParseTable();
            ParseTable.InitTable();
            _builder = new SymbolTableBuilder(_symbolTabelGlobal);
        }

        /// <summary>
        /// This method gets the top token for the parser, from the token stack.
        /// </summary>
        /// <returns>The top token from the token stack</returns>
        private TokenType TopOfStack()
        {
            if (Stack.TryPeek(out TokenType token))
            {
                return token;
            }
            new InvalidTokenException("Expected stack not empty but was empty");
            return TokenType.ERROR;
        }

        /// <summary>
        /// The main method of the parser, which parses the provided tokens.
        /// </summary>
        /// <param name="verbosity">An output string used handle verbosity</param>
        public void Parse(out string verbosity)
        {
            Stack = new Stack<TokenType>();
            verbosity = "";
            Stack.Push(TokenType.EOF);
            Stack.Push(TokenType.PROG);
            Index = 0;
            Root = new ProgramNode(0, 0);
            Scopes.Push(Root);

            while (Stack.Any() && Index < Tokens.Count)
            {
                TokenType top = Stack.Pop();
                TokenType token = Tokens[Index].Type;
                CurrentLine = Tokens[Index].Line;
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
                        new InvalidTokenException($"Bad input {token} top {top} at ({CurrentLine}:{CurrentOffset})");
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
        /// <summary>
        /// This method removes unused scope nodes in the AST once the AST has been constructed.
        /// </summary>
        /// <param name="node">The root of the AST</param>
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

        /// <summary>
        /// This method will return the innermost scope to the user, from the top of the scope stack.
        /// </summary>
        /// <returns>An AST node implementing the IScope interface</returns>
        private AstNode TopScope()
        {
            if (Scopes.TryPeek(out AstNode node))
            {
                return node;
            }
            new NoScopeException("No scopes left on scope stack.");
            return null;
        }
        
        /// <summary>
        /// This method is used to decorate the AST. This is done from the production rules of the ParseAction.
        /// This is done by using a switch case to determine which node to add to what parent node.
        /// </summary>
        /// <param name="token">The token used to decorate the AST</param>
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
                    {
                        VarNode node = new VarNode(token.Value, token);
                        _builder.AddSymbol(node);
                        ((FuncNode)Current).FunctionParameters.Add(node);
                    }
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
                case 108 when token.Type == NUMERIC:
                    ((ArrayNode)Current).Dimensions.Add(new NumericNode(token.Value, token));
                    break;
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
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new NumericNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent).Operator.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent)?.Operator?.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            ((IExpr)Current).LeftHand = term;
                        }
                    }
                    else if (Current.Type == CALL)
                    { ((CallNode)Current).Parameters.Add(new NumericNode(token.Value, token)); }
                    else if (Current.Type == WHILE)
                    {

                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent).Operator.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { LeftHand = term };
                            ((IExpr)((WhileNode)Current).Expression).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 102:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new VarNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionTerm term = new ExpressionTerm(token);
                        term.LeftHand = new VarNode(token.Value, token);
                        if (((IExpr)Current).LeftHand != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 109:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new APinNode(token.Value, token){Parent = (AstNode)expr}, Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new APinNode(token.Value, token){Parent = Current};
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new APinNode(token.Value, token){Parent = Current});
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new APinNode(token.Value, token){Parent = Current};
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 110:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new DPinNode(token.Value, token) {Parent = (AstNode)expr}, Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new DPinNode(token.Value, token) {Parent = Current};
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new DPinNode(token.Value, token) {Parent = Current});
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new DPinNode(token.Value, token) {Parent = term};
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new StringNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new StringNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)Current).LeftHand = term;
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new StringNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new StringNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new NumericNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent).Operator.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent).Operator.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            ((IExpr)Current).LeftHand = term;
                        }
                    }
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new NumericNode(token.Value, token));
                    else if (Current.Type == WHILE)
                    {

                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new NumericNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            if (((IExpr)((ExpressionNode)Current).Parent).Operator.Type == OP_DIVIDE)
                            {
                                if (((NumericNode)term.LeftHand).FValue == 0f)
                                {
                                    new DivisionByZeroException($"Division by zero is not possible. Error at {CurrentLine}:{CurrentOffset}");
                                }
                            }
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new BoolNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new BoolNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                        term.LeftHand = new BoolNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
                            ((IExpr)Current).RightHand = binExpr;
                            Current = binExpr;
                        }
                        else
                            ((IExpr)((WhileNode)Current).Expression).LeftHand = term;
                    }
                    break;
                case 210:
                    if (Current.Type == ASSIGNMENT)
                    {
                        IExpr expr = new BinaryExpression(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).RightHand = (ExpressionNode)expr;
                        ExpressionTerm term = new ExpressionTerm(token) { LeftHand = new BoolNode(token.Value, token), Parent = (ExpressionNode)expr };
                        expr.LeftHand = term;
                        Current = (BinaryExpression)expr;
                    }
                    else if (Current.Type == EXPR)
                    {
                        ExpressionNode term = new ExpressionTerm(token);
                        term.LeftHand = new BoolNode(token.Value, token);
                        term.Parent = (ExpressionNode)Current;
                        if (((IExpr)Current).LeftHand != null && ((IExpr)Current).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                        term.LeftHand = new BoolNode(token.Value, token);
                        if (((IExpr)((WhileNode)Current).Expression).LeftHand != null && ((IExpr)((WhileNode)Current).Expression).Operator != null)
                        {
                            BinaryExpression binExpr = new BinaryExpression(term.Line, term.Offset) { Parent = (ExpressionNode)Current, LeftHand = term };
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
                    {
                        ExpressionNode retExpr = new BinaryExpression(token.Line, token.Offset);
                        ((IExpr)retExpr).LeftHand = expr74;
                        expr74.Parent = retExpr;
                        ((IExpr)((ReturnNode)Current).ReturnValue).LeftHand = retExpr;
                    }
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
                        ExpressionNode whileExpr = new BinaryExpression(token.Line, token.Offset);
                        ((IExpr)whileExpr).LeftHand = expr74;
                        expr74.Parent = whileExpr;
                        ((WhileNode)Current).Expression = whileExpr;
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
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 78;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new MinusNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new MinusNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 87:

                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new TimesNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 87;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new TimesNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new TimesNode(token);
                        ((WhileNode)Current).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 88:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new DivideNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 88;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new DivideNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new DivideNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 86:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new ModuloNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 86;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new ModuloNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new ModuloNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 85:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new PlusNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(NUMERIC);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 85;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new PlusNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new PlusNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(NUMERIC);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 96:
                    if (Current.Type == EXPR)
                    {
                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new LessNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 96;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new LessNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new LessNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 95:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new OrNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 95;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new OrNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new OrNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == IFSTMNT)
                    {
                        ((IExpr)((IfStatementNode)Current).Expression).Operator = new OrNode(token);
                        ((IExpr)((IfStatementNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 94:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new AndNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 94;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new AndNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new AndNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 97:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new GreaterNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 97;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new GreaterNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new GreaterNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                case 98:
                    if (Current.Type == EXPR)
                    {

                        if (((IExpr)Current).Operator == null)
                        {
                            ((IExpr)Current).Operator = new EqualNode(token);
                            ((IExpr)Current).SymbolType = new TypeContext(BOOL);
                            ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)Current };
                            ((IExpr)Current).RightHand = next;
                            Current = next;
                        }
                        else
                        {
                            if (((ExpressionNode)Current).Parent != null)
                            {
                                Current = Current.Parent;
                                goto case 98;
                            }
                        }
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((IExpr)((AssignmentNode)Current).RightHand).Operator = new EqualNode(token);
                        ((IExpr)((AssignmentNode)Current).RightHand).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = (ExpressionNode)((AssignmentNode)Current).RightHand };
                        ((AssignmentNode)Current).RightHand = next;
                        Current = next;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((IExpr)((WhileNode)Current).Expression).Operator = new EqualNode(token);
                        ((IExpr)((WhileNode)Current).Expression).SymbolType = new TypeContext(BOOL);
                        ExpressionNode next = new BinaryExpression(CurrentLine, CurrentOffset) { Parent = ((WhileNode)Current).Expression };
                        ((IExpr)((WhileNode)Current).Expression).RightHand = next;
                        Current = next;
                    }
                    break;
                #endregion Operators
                case 115:
                    if (token.Type == VAR)
                    {
                        SymbolTableObject.FunctionCalls.Add((CallNode)Current);
                        ((CallNode)Current).Id = new VarNode(token.Value, token);
                        break;
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
                    else if (Current.Type == EXPR)
                    {
                        CallNode node = new CallNode(CurrentLine, CurrentOffset);
                        node.Parent = Current;
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
                case 75:
                    if (token.Value != "")
                    {
                        if (token.Type == ARRAYINDEX)
                        {
                            if (Current == null)
                            {
                                Current = new AssignmentNode(CurrentLine, CurrentOffset);
                                ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                            }

                            try
                            {
                                ArrayNode arr = _builder.CurrentSymbolTable.FindArray(token.Value);
                                arr.FirstAccess = (AssignmentNode)Current;
                                ArrayAccessNode node = new ArrayAccessNode(arr, CurrentLine, CurrentOffset);
                                if (((AssignmentNode)Current).LeftHand == null)
                                {
                                    ((AssignmentNode)Current).LeftHand = node;
                                }
                                else
                                {
                                    ((AssignmentNode)Current).RightHand.LeftHand = new ExpressionTerm(token) { LeftHand = node };
                                }
                            }
                            catch (NullReferenceException)
                            {
                                new InvalidTokenException($"Array {token.Value} not declared before use at {token.Line}:{token.Offset}");
                            }
                            
                        }
                    }
                    break;
                case 205:
                case 206:
                    if (token.Type == NUMERIC)
                    {
                        if (Current.Type == ASSIGNMENT)
                        {
                            try
                            {
                                if (((AssignmentNode)Current).LeftHand.Type == ARRAYACCESSING)
                                {
                                    ((ArrayAccessNode)((AssignmentNode)Current).LeftHand).Accesses.Add(new NumericNode(token.Value, token));
                                }
                                else
                                {
                                    ((ArrayAccessNode)((ExpressionTerm)((AssignmentNode)Current).RightHand.LeftHand).LeftHand).Accesses.Add(new NumericNode(token.Value, token));
                                }
                            }
                            catch (NullReferenceException)
                            {
                                new InvalidTokenException($"Array {token.Value} not declared before use at {token.Line}:{token.Offset}");
                            }
                        }
                    }
                    else if (token.Type == VAR)
                    {
                        ((ArrayAccessNode)((AssignmentNode)Current).LeftHand).Accesses.Add(new VarNode(token.Value, token));
                    }
                    break;
                case 90037:
                case 90014:
                    if (Current.Type == CALL)
                    {
                        ExpressionNode previousExpr = (ExpressionNode)Current.Parent;
                        if (previousExpr.LeftHand == null)
                            ((IExpr)previousExpr).LeftHand = (ITerm)Current;
                        else
                        {
                            ((IExpr)previousExpr).RightHand = new ExpressionTerm(token) { LeftHand = (ITerm)Current };
                        }
                        while (!((AstNode)Current).Parent.IsType(typeof(ParenthesisExpression)))
                        {
                            Current = ((ExpressionNode)Current).Parent;
                        }
                        Current = ((AstNode)Current).Parent;
                        Current = ((ExpressionNode)Current).Parent;
                    }
                    else
                    {
                        while (!((ExpressionNode)Current).Parent.IsType(typeof(ParenthesisExpression)))
                        {
                            Current = ((ExpressionNode)Current).Parent;
                        }
                        Current = ((ExpressionNode)Current).Parent;
                        Current = ((ExpressionNode)Current).Parent;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This will add a new nonterminal to the AST.
        /// This is done by adding the nonterminal to the AST.
        /// The type of terminal is decided based on the current parse action.
        /// This method is also responsible for opening and closing scopes when encountered in the parser.
        /// </summary>
        /// <param name="token">The tokentype of the nonterminal</param>
        public void AddToAstNode(TokenType token)
        {

            if (CurrentAction == null) return;
            ScannerToken previous = Tokens[0];
            if (Index > 0)
                previous = Tokens[Index - 1];
            switch (CurrentAction.Type)
            {
                case 17:
                    if (Current == null)
                    {
                        Current = new CallNode(CurrentLine, CurrentOffset);
                        ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    }
                    break;
                case 30:
                case 31:
                case 32:
                case 203:
                    Current = new AssignmentNode(CurrentLine, CurrentOffset);
                    // ((AssignmentNode)Current).RightHand = new ExpressionTerm()
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Current.Parent = TopScope();
                    break;
                case 38:
                    ArrayNode node = new ArrayNode(CurrentLine, CurrentOffset);
                    ((AssignmentNode)Current).RightHand = node;
                    ((VarNode)((AssignmentNode)Current).LeftHand).IsArray = true;
                    ((VarNode)((AssignmentNode)Current).LeftHand).Declaration = true;
                    node.ActualId = (VarNode)((AssignmentNode)Current).LeftHand;
                    _builder.AddArray(node);
                    Current = node;
                    ParseContext.DeclaredArrays.Add(node);
                    break;
                case 129:
                    Current = new WaitNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case 100:
                    ((IExpr)((ExpressionNode)Current).Parent).Operator = GetOrEqualNode(((IExpr)((ExpressionNode)Current).Parent).Operator);
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
                        SymbolTableObject.FunctionDefinitions.Add((FuncNode)Current);
                        _builder.OpenScope(token, $"func_{Tokens[Index + 1].Value}_{CurrentLine}");
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

        /// <summary>
        /// This method will convert a numeric comparison operator to a Greater or Equal, or a Less or Equal node.
        /// </summary>
        /// <param name="node">The node to convert</param>
        /// <returns>GEQ node if operator is Greater node. LEQ node if less node. Null if neither</returns>
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