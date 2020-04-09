using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        private Stack<TokenType> Stack = new Stack<TokenType>();
        private TokenStream TokenStream;
        private ParseTable _parseTable;
        //private AstNode _astNode = new AstNode(new ParseToken(TokenType.START,"",0,0),"",0,0 );
        private bool _accepted;
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

        private void Match(TokenType token)
        {
            if (TokenStream.Peek().Type == token)
                TokenStream.Advance();
            else
                throw new InvalidSyntaxException("Expected token but was not token");
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
                if (Enum.IsDefined(typeof(TokenType),TopOfStack()) && (int)TopOfStack() <= 50) // less than 50
                {
                    Match(TopOfStack());
                    if (TopOfStack() == TokenType.EOF)
                    {
                        _accepted = true;
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
                    InsertInAST(_p);
                }
            }
        }

        private void InsertInAST(List<TokenType> list)
        {
            foreach (var tokenType in list)
            {
                switch (tokenType)
                {
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