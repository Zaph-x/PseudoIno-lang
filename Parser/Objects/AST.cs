using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;

namespace Parser.Objects
{
    public class AST
    {
        private List<List<ScannerToken>> _listOfStacks;
        private List<int> PlaceOfStatements = new List<int>();
        public AST(List<List<ScannerToken>> listOfStacks)
        {
            _listOfStacks = listOfStacks;
        }

        public void FindStatements()
        {
            int i = 0;
            foreach (var stack in _listOfStacks)
            {
                if (stack.First().Type == TokenType.STMNT || stack.First().Type == TokenType.IFSTMNT)
                {
                    PlaceOfStatements.Add(i);
                }

                i += 1;
            }
        }
        
        
    }
}