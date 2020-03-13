using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class ParseTable
    {
        public Stack<Token> Stack = new Stack<Token>();

        public Token TopOfStack()        {
        
            if (Stack.TryPop(out Token token))
            {
                return token;
            }
            throw new InvalidSyntaxException("Expected stack not empty but was empty");
        }

        public void Match(StreamToken tokens,Token token)
        {
            List<Token> alltokens = new List<Token>();
            StreamToken TokenStream = new StreamToken(alltokens);
            if (TokenStream.Peek() == token){
                TokenStream.Advance();
            }
            else
            {
                throw new InvalidSyntaxException("Expected token but was not token");
            }
        }

        public void Apply(Token Token)
        {
            Stack.Pop();
            for (int i = Stack.Count; i > 0; i--)
            {
                Stack.Push(Token);
            }
        }
    }
}