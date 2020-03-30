using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        public Stack<TokenType> Stack = new Stack<TokenType>();
        public TokenStream TokenStream;
        private ParseTable _parseTable;
        private bool accepted = false;
        private int line = 0;
        private List<TokenType> _p;

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
            throw new InvalidSyntaxException("Expected stack not empty but was empty");
        }

        private void Match(TokenStream tokens,TokenType token)
        {
            if (TokenStream.Peek().Type == token)
                TokenStream.Advance();
            else
                throw new InvalidSyntaxException("Expected token but was not token");
        }

        private void Apply(List<TokenType> Tokens)
        {
            Stack.Pop();
            for (int i = Tokens.Count-1; i >= 0; i--)
            {
                Stack.Push(Tokens[i]);
            }
        }

        public void CreateAndFillAST()
        {
            // Create AST and fill with tokens
            Stack.Push(TokenType.STMNT);
            accepted = false;
            while (!accepted)
            {
                if (Enum.IsDefined(typeof(TokenType),TopOfStack()) && (int)TopOfStack() <= 49) // less than 49
                {
                    Match(TokenStream,TopOfStack());
                    if (TopOfStack() == TokenType.NEWLINE)
                    {
                        accepted = true;
                    }
                    Stack.Pop();
                }
                else
                {
                    _p = _parseTable[TopOfStack(),TokenStream.Peek().Type];
                    if (_p.Count == 0)
                    {
                        Stack.Pop();
                        return;
                    }
                    if (_p.First() == TokenType.ERROR)
                    {
                        throw new InvalidSyntaxException( $"ParseTable encountered error state. TOS: {TopOfStack()} TS: {TokenStream.Peek().Type}");
                    }
                    Apply(_p);
                }
            }
        }

        public void TypeCheckAST()
        {
            // Do type checking
        }

        public void CreateAndFillSymbolTable()
        {
            // Create symbol table
        }
    }
}