using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        public Stack<Token> Stack = new Stack<Token>();
        public TokenStream TokenStream;
        private ParseTable _parseTable;
        private bool accepted = false;
        private int line = 0;
        private TokenType p;

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
            Stack.Push(new Token(TokenType.LINEBREAK,1,1));
            accepted = false;
            while (!accepted)
            {
                if (IsTokenType(TopOfStack()))
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
                    p = _parseTable.Get(TopOfStack(),TokenStream.Peek());
                    if (p == TokenType.ERROR)
                    {
                        throw new InvalidSyntaxException("ParseTable encountered error state");
                    }
                    else
                    {
                        //Apply(p);
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
                case TokenType.SIZE_OF:
                case TokenType.STRING:
                case TokenType.TIME_HR:
                case TokenType.TIME_MS:
                case TokenType.COMMENT:
                case TokenType.OP_EQUAL:
                case TokenType.OP_MINUS:
                case TokenType.OP_TIMES:
                case TokenType.TIME_MIN:
                case TokenType.TIME_SEC:
                case TokenType.OP_DIVIDE:
                case TokenType.OP_LPAREN:
                case TokenType.OP_MODULO:
                case TokenType.OP_RPAREN:
                case TokenType.ARRAYLEFT:
                case TokenType.LINEBREAK:
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