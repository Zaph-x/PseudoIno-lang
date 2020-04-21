using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
using Parser.Objects.Nodes;

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
                else if (stack.First().Type == TokenType.FUNC)
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

        public void LogicMainMethod()
        {
            foreach (var statement in _listOfStatements)
            {
                if (statement.First().Type == TokenType.STMNT)
                {
                    if (statement[1].Type == TokenType.VAR)
                    {
                        ParseAssign(statement);
                    }
                }
                else if (statement.First().Type == TokenType.IFSTMNT)
                {
                    ParseIfStatement(statement);
                }
                else if (statement.First().Type == TokenType.FUNC)
                {
                    ParseFunctionDeclaration();
                }
            }
        }

        public void ParseIfStatement(List<ScannerToken> tokens)
        {
            if (true)
            {
                //IfStmnt -> 'if' Val Expr 'do' Stmts ElseStmnt 'end' 'if' .
            }
        }

        public void ParseAssign(List<ScannerToken> tokens)
        {
            AssignmentNode assignmentNode = new AssignmentNode(tokens[1].Line,tokens[1].Offset);
            assignmentNode.LeftHand = new VarNode(tokens[1].Line,tokens[1].Offset);
            assignmentNode.LeftHand.Value = tokens[1].Value;

            assignmentNode.RightHand = ReturnExpressionNode(tokens[2].Type, tokens[2].Line, tokens[2].Offset);
        }

        public ExpressionNode ReturnExpressionNode(TokenType tokenType, int line, int offset)
        {
            if (tokenType == TokenType.MATH_OP)
            {
                return new ExpressionNode(TokenType.MATH_OP, line, offset);
            }
            else if (tokenType == TokenType.BOOL_OP)
            {
                return new ExpressionNode(TokenType.BOOL_OP, line, offset);
            }
            throw new Exception();
        }

        public void ParseFunctionDeclaration()
        {
            //Make func subtree
        }
    }
}