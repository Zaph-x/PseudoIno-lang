using System.Xml.Linq;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
using Parser.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Objects;
using static Lexer.Objects.TokenType;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        int Index = 1;
        private Stack<TokenType> Stack = new Stack<TokenType>();
        // private TokenStream TokenStream;
        List<ScannerToken> Tokens = new List<ScannerToken>();
        public ParseTable ParseTable { get; private set; }
        private bool _accepted;
        private int CurrentLine;
        private int CurrentOffset;
        private List<TokenType> _p;
        public static bool HasError { get; set; } = false;
        public Stack<AstNode> Scopes = new Stack<AstNode>();
        private AstNode Current;
        public ProgramNode Root { get; private set; }
        private ParseAction CurrentAction { get; set; }
        public Parsenizer(List<ScannerToken> tokens)
        {
            Tokens = tokens.Where(tok => tok.Type != TokenType.COMMENT && tok.Type != TokenType.MULT_COMNT).ToList();
            ParseTable = new ParseTable();
            ParseTable.InitTable();
        }

        private TokenType TopOfStack()
        {
            if (Stack.TryPeek(out TokenType token))
            {
                return token;
            }
            // FIXME Skal ikke smide en exception da dette dræber compileren
            throw new InvalidTokenException("Expected stack not empty but was empty");
        }

        public void Parse(List<ScannerToken> Tokens, out string verbosity)
        {
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
            Optimize(Root);
        }

        private void Optimize(IScope node)
        {
            for (int i = 0; i < node.Statements.Count; i++)
            {
                StatementNode n = node.Statements[i];
                if (n is IScope)
                {
                    if (((IScope)n).Statements.Any())
                        Optimize((IScope)n);
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
            return null;
            // FIXME Skal ikke smide en exception da dette dræber compileren
            // throw new InvalidTokenException("Expected stack not empty but was empty");
        }

        public void DecorateAstNode(ScannerToken token)
        {
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
                        ((FuncNode)Current).FunctionParameters.Add(new VarNode(token.Value, CurrentLine, CurrentOffset));
                    break;
                case 30 when token.Type == VAR:
                case 31 when token.Type == APIN:
                case 32 when token.Type == DPIN:
                    ((AssignmentNode)Current).Var = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 116 when token.Type == VAR:
                    ((FuncNode)TopScope()).Name = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 126:
                    ((WhileNode)TopScope()).Expression = new ExpressionNode(CurrentLine, CurrentOffset);
                    break;

                // ASSIGNABLES
                case 48:
                    var expr48 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == ASSIGNMENT)
                        ((AssignmentNode)Current).Assignment = expr48;
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = expr48;
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = expr48;
                    Current = expr48;
                    break;
                case 101:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new NumericNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new NumericNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new NumericNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new NumericNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 102:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new VarNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 103:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new APinNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new APinNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new APinNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new APinNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 104:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new DPinNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new DPinNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new DPinNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new DPinNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 106:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new StringNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new StringNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new StringNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new StringNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 107:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new NumericNode("-" + token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new NumericNode("-" + token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new NumericNode("-" + token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new NumericNode("-" + token.Value, CurrentLine, CurrentOffset);
                    break;
                case 105:
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = new BoolNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = new BoolNode(token.Value, CurrentLine, CurrentOffset);
                    else if (Current.Type == CALL)
                        ((CallNode)Current).Parameters.Add(new BoolNode(token.Value, CurrentLine, CurrentOffset));
                    else if (Current.Type == WHILE)
                        ((WhileNode)Current).Expression.Term = new BoolNode(token.Value, CurrentLine, CurrentOffset);
                    break;

                // EXPRESSIONS
                case 74:
                    ExpressionNode expr74 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == ASSIGNMENT)
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Term = expr74;
                    else if (Current.Type == EXPR)
                        ((ExpressionNode)Current).Term = expr74;
                    else if (Current.Type == RETURN)
                        ((ExpressionNode)((ReturnNode)Current).ReturnValue).Term = expr74;
                    // else if (Current.Type == WHILE)
                    Current = expr74;
                    break;

                // OPERATORS
                case 78:
                    var expr78 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new MinusNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr78;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new MinusNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr78;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new MinusNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr78;
                    }
                    Current = expr78;
                    break;
                case 85:
                    var expr85 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new PlusNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr85;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new PlusNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr85;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new PlusNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr85;
                    }
                    Current = expr85;
                    break;
                case 96:
                    var expr96 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new LessNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr96;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new LessNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr96;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new LessNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr96;
                    }
                    Current = expr96;
                    break;
                case 95:
                    var expr95 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new OrNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr95;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new OrNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr95;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new OrNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr95;
                    }
                    Current = expr95;
                    break;
                case 94:
                    var expr94 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new AndNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr94;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new AndNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr94;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new AndNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr94;
                    }
                    Current = expr94;
                    break;
                case 97:
                    var expr97 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new GreaterNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr97;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new GreaterNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr97;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new GreaterNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr97;
                    }
                    Current = expr97;
                    break;
                case 98:
                    var expr98 = new ExpressionNode(CurrentLine, CurrentOffset);
                    if (Current.Type == EXPR)
                    {
                        ((ExpressionNode)Current).Operator = new EqualNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)Current).Expression = expr98;
                    }
                    else if (Current.Type == ASSIGNMENT)
                    {
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Operator = new EqualNode(CurrentLine, CurrentOffset);
                        ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr98;
                    }
                    else if (Current.Type == WHILE)
                    {
                        ((WhileNode)Current).Expression.Operator = new EqualNode(CurrentLine, CurrentOffset);
                        ((WhileNode)Current).Expression.Expression = expr98;
                    }
                    Current = expr98;
                    break;

                case 115:
                    if (token.Type == VAR)
                        ((CallNode)Current).Id = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    break;

                case 123:
                    if (token.Type == VAR)
                        ((ForNode)Current).CountingVariable = new VarNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                case 128:
                    if (token.Type == NUMERIC)
                        if (((ForNode)Current).From == null)
                            ((ForNode)Current).From = new NumericNode(token.Value, CurrentLine, CurrentOffset);
                        else if (((ForNode)Current).To == null)
                            ((ForNode)Current).To = new NumericNode(token.Value, CurrentLine, CurrentOffset);
                    break;
                default:
                    break;
            }
        }

        public void AddToAstNode(TokenType token)
        {
            switch (CurrentAction.Type)
            {
                // case 49:
                // case 50:
                // case 51:
                // case 52:
                // case 53:
                // case 54:
                // case 55:
                // case 56:
                //     var expr = new ExpressionNode(CurrentLine, CurrentOffset);
                //     if (Current.Type == EXPR)
                //         ((ExpressionNode)Current).Expression = expr;
                //     else if (Current.Type == ASSIGNMENT)
                //         ((ExpressionNode)((AssignmentNode)Current).Assignment).Expression = expr;
                //     else if (Current.Type == WHILE)
                //         ((WhileNode)Current).Expression = expr;
                //     Current = expr;
                //     break;
                case 115:
                    if (Current.Type == ASSIGNMENT)
                    {
                        CallNode node = new CallNode(CurrentLine, CurrentOffset);
                        ((AssignmentNode)Current).Assignment = node;
                        Current = node;
                    }
                    else
                    {
                        Current = new CallNode(CurrentLine, CurrentOffset);
                        ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    }
                    break;
                case 129:
                    Current = new WaitNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case 30:
                case 31:
                case 32:
                    Current = new AssignmentNode(CurrentLine, CurrentOffset);
                    ((AssignmentNode)Current).Assignment = new ExpressionNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case 126:
                    Current = new WhileNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case 123:
                    Current = new ForNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case 116:
                    Current = new FuncNode(CurrentLine, CurrentOffset);
                    ((ProgramNode)TopScope()).FunctionDefinitons.Add((FuncNode)Current);
                    Scopes.Push(Current);
                    break;
                case 111:
                    Current = new IfStatementNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case 113:
                    Scopes.Pop();
                    Current = new ElseStatementNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case 114:
                    Scopes.Pop();
                    Current = new ElseifStatementNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case 124:
                case 127:
                case 112:
                case 117:
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
                    ReturnNode retNode = new ReturnNode(CurrentLine, CurrentOffset);
                    ExpressionNode expr120 = new ExpressionNode(CurrentLine, CurrentOffset);
                    retNode.ReturnValue = expr120;
                    ((IScope)Current).Statements.Add(retNode);
                    Current = expr120;
                    break;
                
                default:
                    return;
            }
        }
    }
}