using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
using Parser.Objects;

namespace Parser
{
    public class Parsenizer
    {
        // public AST Ast = new AST();
        int Index = 1;
        private Stack<TokenType> Stack = new Stack<TokenType>();
        // private TokenStream TokenStream;
        List<ScannerToken> Tokens = new List<ScannerToken>();
        public ParseTable ParseTable { get; private set; }
        private bool _accepted;
        private List<TokenType> _p;
        public static bool HasError { get; set; } = false;

        public Parsenizer(List<ScannerToken> tokens)
        {
            Tokens = tokens.Where(tok => tok.Type != TokenType.COMMENT && tok.Type != TokenType.MULT_COMNT).ToList();
            ParseTable = new ParseTable();
            ParseTable.InitTable();
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

        // private void Match(TokenType token)
        // {
        //     if (Tokens[Index].Type == token)
        //     {
        //         System.Console.WriteLine("Advancing");
        //         Index++;
        //         Stack.Pop();
        //     }
        //     else
        //     {
        //         new InvalidTokenException($"Expected  {token} but was {Tokens[Index + 1].Type}");
        //     }
        // }

        // private void Apply(List<TokenType> tokens)
        // {
        //     Stack.Pop();
        //     tokens.Reverse();
        //     foreach (var token in tokens)
        //     {
        //         Stack.Push(token);
        //     }
        // }
        public void Parse(out string verbosity)
        {
            verbosity = "";
            Stack.Push(TokenType.EOF);
            Stack.Push(TokenType.PROG);

            while (Stack.Any())
            {
                // verbosity += $"TS: {Tokens[Index]} TSPeek: {Tokens[Index+1]} TOS: {TopOfStack()}\n";
                System.Console.WriteLine($"TS: {Tokens[Index]} TSPeek: {Tokens[Index + 1]} TOS: {TopOfStack()}");
                while (Stack.Any())
                {
                    TokenType top = Stack.Pop();
                    TokenType token = Tokens[Index].Type;
                    if (TokenTypeExpressions.IsTerminal(top))
                    {
                        if (top == token)
                        {
                            Index++;
                            System.Console.WriteLine("POP {0}", top);
                            if (token == TokenType.EOF)
                                System.Console.WriteLine("Input accepted");
                        }
                        else
                        {
                            System.Console.WriteLine("Bad input {0}", token);
                            break;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Value {0} token {1}", top, token);
                        var rule = ParseTable[top, token].Product;
                        if (rule.Count > 0 && rule.First() == TokenType.ERROR)
                        {
                            System.Console.WriteLine("Bad input {0}", token);
                            break;
                        }
                        rule.Reverse();
                        foreach (var r in rule)
                            Stack.Push(r);
                    }
                    System.Console.WriteLine(Stack.ToString());
                }

                // CopyStackToList();
                // if (TokenTypeExpressions.IsTerminal(TopOfStack()))
                // {
                //     if (TopOfStack() == TokenType.EOF)
                //     {
                //         break;
                //     }
                //     Match(TopOfStack());
                //     Stack.Pop();
                // }
                // else
                // {
                //     TokenType top = TopOfStack();
                //     TokenType next = TokenStream.Peek().Type;
                //     _p = ParseTable[top, next].Product;
                //     if (_p.Any())
                //     {
                //         if (_p.First() == TokenType.ERROR)
                //         {
                //             new InvalidTokenException($"ParseTable encountered error state. TOS: {TopOfStack()} TS: {TokenStream.Peek().Type}");
                //             break;
                //         }
                //         List<ScannerToken> scannerTokens;
                //         Apply(_p);
                //     }
                //     else
                //     {
                //         Stack.Pop();
                //     }
                // }
                if (HasError) return;
            }
        }
    }
}