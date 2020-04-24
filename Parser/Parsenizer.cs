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
        public static Stack<AstNode> Scopes = new Stack<AstNode>();
        public AstNode Current;
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
            Scopes.Push(new ProgramNode(0, 0));

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

                    var rule = ParseTable[top, token].Product;
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
        }

        private AstNode TopScope()
        {
            if (Scopes.TryPeek(out AstNode node))
            {
                return node;
            }
            // FIXME Skal ikke smide en exception da dette dræber compileren
            throw new InvalidTokenException("Expected stack not empty but was empty");
        }


        public void AddToAstNode(TokenType token)
        {
            switch (token)
            {
                case WAITSTMNT:
                    Current = new WaitNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case ASSIGNSTMNT:
                    Current = new AssignmentNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case LOOPW:
                    Current = new WhileNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case LOOPF:
                    Current = new ForNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;
                case FUNCCALL:
                    Current = new FuncNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    break;
                case IFSTMNT:
                    Current = new IfStatementNode(CurrentLine, CurrentOffset);
                    ((IScope)TopScope()).Statements.Add((StatementNode)Current);
                    Scopes.Push(Current);
                    break;


                default:
                    return;
            }
        }
    }
}