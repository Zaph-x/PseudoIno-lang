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
        private int CurrentLine;
        private int CurrentOffset;
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
        public void Parse(List<ScannerToken> Tokens, out string verbosity)
        {
            Stack = new Stack<TokenType>();
            verbosity = "";
            Stack.Push(TokenType.EOF);
            Stack.Push(TokenType.PROG);
            Index = 0;
            System.Console.WriteLine();

            // verbosity += $"TS: {Tokens[Index]} TSPeek: {Tokens[Index+1]} TOS: {TopOfStack()}\n";
            while (Stack.Any() && Index < Tokens.Count)
            {
                TokenType top = Stack.Pop();
                TokenType token = Tokens[Index].Type;
                CurrentLine = Tokens[Index].Line;
                CurrentOffset = Tokens[Index].Offset;
                verbosity += $"Token: {token} Top: {TopOfStack()}".PadRight(35, ' ');
                if (TokenTypeExpressions.IsTerminal(top))
                {
                    if (top == token)
                    {
                        Index++;
                        // System.Console.WriteLine("POP {0}", top);

                    }
                    else
                    {
                        new InvalidTokenException($"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})");
                        verbosity += $"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})";
                        break;
                    }
                }
                else
                {
                    // System.Console.WriteLine("Value {0} token {1}", top, token);
                    var rule = ParseTable[top, token].Product;
                    if (rule.Count > 0 && rule.First() == TokenType.ERROR)
                    {
                        new InvalidTokenException($"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})");
                        verbosity += $"Bad input {token} top {top} ({CurrentLine}:{CurrentOffset})";
                        break;
                    }
                    for (int i = rule.Count - 1; i >= 0; i--)
                        Stack.Push(rule[i]);
                }
                // foreach (var val in Stack)
                //     verbosity += $"{val}".PadRight(13, ' ');
                verbosity += $"\n";
                if (token == TokenType.EOF || Index == Tokens.Count)
                { verbosity += "Input Accepted."; }
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