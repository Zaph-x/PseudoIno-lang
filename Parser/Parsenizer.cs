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
        public Stack<ParseToken> Stack = new Stack<ParseToken>();
        public TokenStream TokenStream;
        private ParseTable _parseTable;
        private bool accepted = false;
        private int line = 0;
        private List<ParseToken> _p;

        public Parsenizer(List<ScannerToken> tokens)
        {
             TokenStream = new TokenStream(tokens);
             _parseTable = new ParseTable();
             _parseTable.InitTable();
        }

        private ParseToken TopOfStack()        
        {
            if (Stack.TryPeek(out ParseToken token))
            {
                return token;
            }
            throw new InvalidSyntaxException("Expected stack not empty but was empty");
        }

        private void Match(TokenStream tokens,Token token)
        {
            if (TokenStream.Peek() == token)
                TokenStream.Advance();
            else
                throw new InvalidSyntaxException("Expected token but was not token");
        }

        private void Apply(List<ParseToken> Tokens)
        {
            Stack.Pop();
            for (int i = Tokens.Count; i > 0; i--)
            {
                Stack.Push(Tokens[i]);
            }
        }

        public void CreateAndFillAST()
        {
            // Create AST and fill with tokens
            Stack.Push(new ParseToken(TokenType.STMNT,1,1));
            accepted = false;
            while (!accepted)
            {
                if (Enum.IsDefined(typeof(TokenType),TopOfStack().Type) && (int)TopOfStack().Type <= 43) // less than 43
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
                    Console.WriteLine($"TOS: {TopOfStack().GetHashCode()}");
                    int x = TopOfStack().GetHashCode();
                    TokenType t1 = TopOfStack().Type;
                    TokenType t2 = TokenStream.Current().Type;
                    _p = _parseTable[TopOfStack().Type,TokenStream.Current().Type];
                    if (_p.First().Type == TokenType.ERROR)
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