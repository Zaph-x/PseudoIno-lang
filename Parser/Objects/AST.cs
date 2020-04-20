using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;

namespace Parser.Objects
{
    public class AST
    {
        private List<List<ScannerToken>> _listOfStacks;
        private List<int> PlaceOfStatements = new List<int>();
        private List<List<ScannerToken>> _listOfStatements = new List<List<ScannerToken>>();
        public AST(List<List<ScannerToken>> listOfStacks)
        {
            _listOfStacks = listOfStacks;
        }

        public void FindStatements()
        {
            int i = 0;
            foreach (var stack in _listOfStacks)
            {
                if (stack.First().Type == TokenType.STMNT) //if (stack.First().Type == TokenType.STMNT || stack.First().Type == TokenType.IFSTMNT)
                {
                    PlaceOfStatements.Add(i);
                }
                else if (stack.First().Type == TokenType.FUNCDECL)
                {
                    PlaceOfStatements.Add(i);
                }

                i += 1;
            }
        }

        public void MakeStatements()
        {
            for (int i = 0; i < PlaceOfStatements.Count; i++)
            {
                List<ScannerToken> list = new List<ScannerToken>();
                if (i == PlaceOfStatements.Count - 1)
                {
                    break;
                }
                for (int j = PlaceOfStatements[i]; j < PlaceOfStatements[i+1]; j++)
                {
                    list.Add(_listOfStacks[j].First());
                }
                _listOfStatements.Add(list);
            }
        }

        public void TrimStatements()
        {
            foreach (var statement in _listOfStatements)
            {
                for (int i = statement.Count - 1; i > 0; i--)
                {
                    if (TokenTypeExpressions.IsTerminal(statement[i].Type))
                    {
                        break;
                    }
                    statement.RemoveAt(i);
                }
            }
        }
    }
}