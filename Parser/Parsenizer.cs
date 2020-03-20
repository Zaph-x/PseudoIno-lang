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
        /// <summary>
        /// A stack of tokens to build AstNodes from.
        /// </summary>
        /// <typeparam name="ParseToken"></typeparam>
        /// <returns>A stack of tokens</returns>
        public Stack<Token> Stack = new Stack<Token>();
        /// <summary>
        /// A stream of tokens.
        /// </summary>
        public TokenStream TokenStream;
        
        private ParseTable _parseTable;
        private bool accepted = false;
        private int line = 0;
        private Token p;

        public Parsenizer(List<Token> tokens)
        {
             TokenStream = new TokenStream(tokens);
             _parseTable = new ParseTable();
        }

        public Token TopOfStack()        
        {
            if (Stack.TryPop(out Token token))
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

        public void Apply(Token Token)
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
            Stack.Push(new Token(TokenType.BEGIN,"",1,1));
            accepted = false;
            while (!accepted)
            {
                if (Enum.IsDefined(typeof(Token),TopOfStack()))
                {
                    Match(TokenStream,TopOfStack());
                    if (TopOfStack() == new Token(TokenType.BEGIN,"",1,1))
                    {
                        accepted = true;
                        Stack.Pop();
                    }
                }
                else
                {
                    p = _parseTable[TopOfStack(),TokenStream.Peek()];
                    if (p == new Token(TokenType.BEGIN,"",1,1))
                    {
                        new InvalidSyntaxException("ParseTable encountered error state");
                    }
                    else
                    {
                        Apply(p);
                    }
                }
            }
        }

        public bool IsTokenType(Token token)
        {
            switch (token.Type)
            {
                case TokenType.IF:
                case TokenType.END:
                case TokenType.FOR:
                case TokenType.VAL:
                case TokenType.VAR:
                case TokenType.APIN:
                case TokenType.BOOL:
                case TokenType.CALL:
                case TokenType.DPIN:
                case TokenType.ELSE:
                case TokenType.FUNC:
                case TokenType.OP_OR:
                case TokenType.WAIT:
                case TokenType.ERROR:
                case TokenType.OP_AND:
                case TokenType.OP_NOT:
                case TokenType.RANGE:
                case TokenType.WHILE:
                case TokenType.ASSIGN:
                case TokenType.LOOP_FN:
                case TokenType.OP_LESS:
                case TokenType.OP_PLUS:
                case TokenType.STRING:
                case TokenType.COMMENT:
                case TokenType.OP_EQUAL:
                case TokenType.OP_MINUS:
                case TokenType.OP_TIMES:
                case TokenType.OP_DIVIDE:
                case TokenType.OP_LPAREN:
                case TokenType.OP_MODULO:
                case TokenType.OP_RPAREN:
                case TokenType.ARRAYLEFT:
                case TokenType.MULT_COMNT:
                case TokenType.OP_GREATER:
                case TokenType.ARRAYINDEX:
                case TokenType.ARRAYRIGHT:
                case TokenType.NUMERIC_INT:
                case TokenType.NUMERIC_FLOAT:
                case TokenType.OP_QUESTIONMARK:
                    return true;
                default:
                    return false;
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