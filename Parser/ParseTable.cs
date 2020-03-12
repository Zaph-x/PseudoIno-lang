using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser
{
    public class ParseTable
    {
        public Stack<Token> Stack = new Stack<Token>();

        public Token TOS()
        {
            if (Stack.TryPop(out Token token))
            {
                return token;
            }
            throw new InvalidSyntaxException("Expected stack not empty but was empty");
        }

        public void Match()
        {
            
        }

        public void Apply()
        {
            
        }
        
        
    }
}