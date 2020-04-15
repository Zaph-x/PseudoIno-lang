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
        private Stack<ScannerToken> Stack = new Stack<ScannerToken>();
        private TokenStream TokenStream;
        public ParseTable ParseTable { get; private set; }
        public ProgramNode Program { get; internal set; } = new ProgramNode(0, 0);
        private bool _accepted;
        private List<TokenType> _p;
        private AstNode _current;
        public static bool HasError { get; set; } = false;

        public Parsenizer(List<ScannerToken> tokens)
        {
            TokenStream = new TokenStream(tokens);
            ParseTable = new ParseTable();
            ParseTable.InitTable();
        }

        private ScannerToken TopOfStack()
        {
            if (Stack.TryPeek(out ScannerToken token))
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
            {
                new InvalidTokenException($"Expected {TokenStream.Peek().Type} but was {token}");
            }
        }

        private void Apply(List<TokenType> tokens, out List<ScannerToken> scannerTokens)
        {
            Stack.Pop();
            scannerTokens = new List<ScannerToken>();
            foreach (var token in tokens)
            {
                if (TokenTypeExpressions.IsTerminal(token))
                {
                    int i = 1;
                    while (token != TokenStream.Peek(i).Type)
                    {
                        i += 1;
                    }
                    scannerTokens.Add(new ScannerToken(TokenStream.Peek(i).Type, TokenStream.Peek(i).Value, TokenStream.Peek(i).Line, TokenStream.Peek(i).Offset));
                }
                else scannerTokens.Add(new ScannerToken(token, "", 0, 0));
            }
            for (int i = scannerTokens.Count - 1; i >= 0; i--)
            {
                Stack.Push(scannerTokens[i]);
            }
        }

        public void CreateAndFillAst()
        {
            // Create AST and fill with tokens
            Stack.Push(TokenStream.EOF);
            Stack.Push(TokenStream.PROG);
            _accepted = false;
            while (!_accepted)
            {
                System.Console.WriteLine($"TS: {TokenStream.Current()} TSPeek: {TokenStream.Peek()} TOS: {TopOfStack()}");
                if (Enum.IsDefined(typeof(TokenType), TopOfStack().Type) && TokenTypeExpressions.IsTerminal(TopOfStack().Type)) // less than 50
                {
                    Match(TopOfStack().Type);
                    if (TopOfStack().Type == TokenType.EOF)
                    {
                        _accepted = true;
                    }
                    //InsertTerminal();
                    Stack.Pop();
                }
                else
                {
                    _p = ParseTable[TopOfStack(), TokenStream.Peek()].Product;
                    if (_p.Count == 0)
                    {
                        //InsertEpsilon();
                        Stack.Pop();
                        continue;
                    }
                    if (_p.First() == TokenType.ERROR)
                    {
                        new InvalidTokenException($"ParseTable encountered error state. TOS: {TopOfStack().Type} TS: {TokenStream.Peek().Type}");
                    }
                    List<ScannerToken> scannerTokens;
                    Apply(_p, out scannerTokens);
                    InsertInAST(scannerTokens);
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
        private void InsertInAST(List<ScannerToken> list)
        {
            _current = _current?.Parent ?? Program;
            _current = _current.Children.Find(x => x.Type == TopOfStack().Type);

            foreach (var token in list)
            {
                switch (token.Type)
                {
                    case TokenType.ASSIGNMENT:
                        Program.Statements.Add(new AssignmentNode(token.Line, token.Offset));
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
                    new InvalidTokenException("Invalid token type value in token ");
                    return null;
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