using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;
using Parser.Objects.Nodes;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        private Stack<TokenType> Stack = new Stack<TokenType>();
        private TokenStream TokenStream;
        private ParseTable _parseTable;
        public ProgramNode Program { get; internal set; } = new ProgramNode(0, 0);
        private bool _accepted;
        private List<TokenType> _p;
        private AstNode _current;

        public Parsenizer(List<ScannerToken> tokens)
        {
            TokenStream = new TokenStream(tokens);
            _parseTable = new ParseTable();
            _parseTable.InitTable();
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

        private void Match(TokenType token)
        {
            if (TokenStream.Peek().Type == token)
                TokenStream.Advance();
            else
                new InvalidTokenException("Expected token but was not token");
        }

        private void Apply(List<TokenType> tokens)
        {
            Stack.Pop();
            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                Stack.Push(tokens[i]);
            }
        }

        public void CreateAndFillAst()
        {
            // Create AST and fill with tokens
            Stack.Push(TokenType.EOF);
            Stack.Push(TokenType.STMNT);
            _accepted = false;
            while (!_accepted)
            {
                if (Enum.IsDefined(typeof(TokenType), TopOfStack()) && (int)TopOfStack() <= 50) // less than 50
                {
                    Match(TopOfStack());
                    if (TopOfStack() == TokenType.EOF)
                    {
                        _accepted = true;
                    }
                    InsertTerminal();
                    Stack.Pop();
                }
                else
                {
                    _p = _parseTable[TopOfStack(), TokenStream.Peek().Type];
                    if (_p.Count == 0)
                    {
                        InsertEpsilon();
                        Stack.Pop();
                        return;
                    }
                    if (_p.First() == TokenType.ERROR)
                    {
                        new InvalidTokenException($"ParseTable encountered error state. TOS: {TopOfStack()} TS: {TokenStream.Peek().Type}");
                    }
                    Apply(_p);
                    InsertInAST(_p);
                }
            }
        }

        private void InsertTerminal()
        {
            _current.Children.Add(GenerateNodeFromTokenType(TokenStream.Peek()));
            _current = _current.Parent;
        }
        private void InsertEpsilon()
        {
            _current.Children.Add(new EpsilonNode(0, 0));
            _current = _current.Parent;
        }

        /*private void Insert(List<TokenType> list)
        {
        }*/
        private void InsertInAST(List<TokenType> list)
        {
            _current = _current.Parent;
            _current = _current.Children.Find(x => x.Type == TopOfStack());

            foreach (var tokenType in list)
            {
                switch (tokenType)
                {
                    case TokenType.ASSIGNMENT:
                        Program.Statements.Add(new AssignmentNode(1, 1));
                        break;
                    case TokenType.STMNT:
                        //_astNode.AddChild(new AstNode(new ParseToken(tokenType,"",0,0),"",0,0 ));
                        break;
                    case TokenType.VAR:

                        //_astNode.AddChild();
                        break;
                    default:
                        break;
                }
            }
        }
        //TODO: make switch case
        private AstNode GenerateNodeFromTokenType(ScannerToken token)
        {
            switch (token.Type)
            {
                case TokenType.VAL:
                    return new ValNode(token.Line, token.Offset);
                case TokenType.VAR:
                    return new VarNode(token.Line, token.Offset);
                default:
                    throw new InvalidTokenException("Invalid Token value in token ");
            }
        }

        public void TypeCheckAst()
        {
            // Do type checking
        }

        public void CreateAndFillSymbolTable()
        {
            // Create symbol table
        }
    }
}