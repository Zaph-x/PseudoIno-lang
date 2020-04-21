using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        private Stack<ScannerToken> Stack = new Stack<ScannerToken>();
        private List<List<ScannerToken>> _listOfStacks = new List<List<ScannerToken>>();
        private TokenStream TokenStream;
        public ParseTable ParseTable { get; private set; }
        private bool _accepted;
        private List<TokenType> _p;
        public static bool HasError { get; set; } = false;
        public Parsenizer(List<ScannerToken> tokens)
        {
            TokenStream = new TokenStream(tokens.Where(tok => tok.Type != TokenType.COMMENT && tok.Type != TokenType.MULT_COMNT));
            ParseTable = new ParseTable();
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
        public List<ScannerToken> Parse(out string verbosity)
        {
            List<ScannerToken> tokenList = new List<ScannerToken>();
            verbosity = "";
            Stack.Push(TokenStream.PROG);
            while (Stack.Any())
            {
                verbosity += $"TS: {TokenStream.Current()} TSPeek: {TokenStream.Peek()} TOS: {TopOfStack()}\n";
                tokenList.Add(TopOfStack().Copy());
                CopyStackToList();
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
            return tokenList;
        }

        private void CopyStackToList()
        {
            List<ScannerToken> list = new List<ScannerToken>();
            foreach (var token in Stack)
            {
                list.Add(token);
            }
            _listOfStacks.Add(list);
        }

        private void ParseNode()
        {
            throw new NotImplementedException();
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