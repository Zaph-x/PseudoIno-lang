using System;
using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        public Stack<ParseToken> Stack = new Stack<ParseToken>();
        public TokenStream TokenStream;
        private ParseTable _parseTable;
        private bool accepted = false;
        private int line = 0;
        private ParseToken _p;

        public Parsenizer(List<ScannerToken> tokens)
        {
             TokenStream = new TokenStream(tokens);
             _parseTable = new ParseTable();
        }

        public ParseToken TopOfStack()        
        {
            if (Stack.TryPop(out ParseToken token))
            {
                return token;
            }
            throw new InvalidSyntaxException("Expected stack not empty but was empty");
        }

        public void Match(TokenStream tokens,Token token)
        {
            if (TokenStream.Peek() == token)
                TokenStream.Advance();
            else
                throw new InvalidSyntaxException("Expected token but was not token");
        }

        public void Apply(ParseToken Token)
        {
            Stack.Pop();
            for (int i = Stack.Count; i > 0; i--)
            {
                Stack.Push(Token);
            }
        }

        public void CreateAndFillAST()
        {
            // Create AST and fill with tokens
            Stack.Push(new ParseToken(TokenType.LINEBREAK,1,1));
            accepted = false;
            while (!accepted)
            {
                if (Enum.IsDefined(typeof(ParseToken),TopOfStack()))
                {
                    Match(TokenStream,TopOfStack());
                    if (TopOfStack().Type == TokenType.LINEBREAK)
                    {
                        accepted = true;
                        Stack.Pop();
                    }
                }
                else
                {
                    _p = _parseTable[TopOfStack(),TokenStream.Peek()];
                    if (_p.Type == TokenType.ERROR)
                    {
                        throw new InvalidSyntaxException("ParseTable encountered error state");
                    }
                    else
                    {
                        Apply(_p);
                    }
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