using System.Runtime.Intrinsics.X86;
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
                new InvalidTokenException($"Expected  {token} but was {TokenStream.Peek().Type}");
            }
        }

        private void Apply(List<TokenType> tokens, out List<ScannerToken> scannerTokens)
        {
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
        public void Parse(out string verbosity)
        {
            verbosity = "";
            Stack.Push(TokenStream.PROG);

            while (Stack.Any())
            {
                verbosity += $"TS: {TokenStream.Current()} TSPeek: {TokenStream.Peek()} TOS: {TopOfStack()}\n";
                if (TokenTypeExpressions.IsTerminal(TopOfStack().Type))
                {
                    if (TopOfStack().Type == TokenType.EOF)
                    {
                        break;
                    }
                    Match(TopOfStack().Type);
                    Stack.Pop();
                }
                else
                {
                    ScannerToken top = TopOfStack();
                    ScannerToken next = TokenStream.Peek();
                    _p = ParseTable[top, next].Product;
                    if (_p.Any())
                    {
                        if (_p.First() == TokenType.ERROR)
                        {
                            new InvalidTokenException($"ParseTable encountered error state. TOS: {TopOfStack().Type} TS: {TokenStream.Peek().Type}");
                            continue;
                        }
                        List<ScannerToken> scannerTokens;
                        Apply(_p, out scannerTokens);
                    }
                    else
                    {
                        Stack.Pop();
                    }
                }
            }
        }

        private void ParseNode()
        {
            throw new NotImplementedException();
        }

        // public void Parse()
        // {
        //     // Create AST and fill with tokens
        //     Stack.Push(TokenStream.EOF);
        //     Stack.Push(TokenStream.PROG);
        //     while (Stack.Any())
        //     {
        //         if (TokenTypeExpressions.IsTerminal(TopOfStack().Type)) // less than 50
        //         {
        //             System.Console.WriteLine($"TS: {TokenStream.Current()} TSPeek: {TokenStream.Peek()} TOS: {TopOfStack()}");
        //             if (TopOfStack().Type == TokenType.EOF) break;
        //             Match(TopOfStack().Type);
        //             //InsertTerminal();
        //             Stack.Pop();
        //         }
        //         else
        //         {
        //             ScannerToken top = TopOfStack();
        //             ScannerToken next = TokenStream.Peek();
        //             _p = ParseTable[top, next].Product;
        //             if (_p.Count == 0)
        //             {
        //                 //InsertEpsilon();
        //                 Stack.Pop();
        //                 continue;
        //             }
        //             if (_p.First() == TokenType.ERROR)
        //             {
        //                 new InvalidTokenException($"ParseTable encountered error state. TOS: {TopOfStack().Type} TS: {TokenStream.Peek().Type}");
        //             }
        //             List<ScannerToken> scannerTokens;
        //             Apply(_p, out scannerTokens);
        //         }
        //     }
        // }

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