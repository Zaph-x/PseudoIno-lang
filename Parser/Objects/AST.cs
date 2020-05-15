using System;
using System.Collections.Generic;
using System.Linq;
using Lexer.Objects;
// using Parser.Objects.Nodes;


namespace Parser.Objects
{
    public class AST
    {
        private List<List<ScannerToken>> _listOfStacks;
        private List<int> PlaceOfStatements = new List<int>();
        private List<List<ScannerToken>> _listOfStatements = new List<List<ScannerToken>>();
        private TokenStream _tokenStream;
        public AST(List<List<ScannerToken>> listOfStacks)
        {
            _listOfStacks = listOfStacks;
        }

        public void FindStatements()
        {
            int i = 0;
            foreach (var stack in _listOfStacks)
            {
                switch (stack.First().Type)
                {
                    case TokenType.STMNT:
                    case TokenType.FUNC:
                    case TokenType.END:
                        PlaceOfStatements.Add(i);
                        break;
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
                for (int j = PlaceOfStatements[i]; j < PlaceOfStatements[i + 1]; j++)
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

            foreach (var statement in _listOfStatements.Select(list => new TokenStream(list)))
            {
                if (statement.Current().Type == TokenType.STMNT)
                {
                    if (statement.Peek().Type == TokenType.VAR)
                    {
                        // ParseAssign(statement);
                    }
                }
                else if (statement.Current().Type == TokenType.IFSTMNT)
                {
                    ParseIfStatement(statement);
                }
                else if (statement.Current().Type == TokenType.FUNC)
                {
                    ParseFunctionDeclaration(statement);
                }
                else if (IsType(statement, TokenType.CALL))
                {

                }
            }
        }

        private bool IsType(TokenStream statement, TokenType expectedType)
        {
            return statement.Current().Type == expectedType;
        }

        public void ParseIfStatement(TokenStream tokens)
        {
            if (true)
            {
                //IfStmnt -> 'if' Val Expr 'do' Stmts ElseStmnt 'end' 'if' .
            }
        }

        // public void ParseAssign(TokenStream tokens)
        // {
        //     AssignmentNode assignmentNode = new AssignmentNode(tokens.Current().Line, tokens.Current().Offset);
        //     assignmentNode.LeftHand = new VarNode(tokens.Current().Line, tokens.Current().Offset);
        //     assignmentNode.LeftHand.Value = tokens.Current().Value;
        //     tokens.Advance();
        //     assignmentNode.RightHand = ReturnExpressionNode(tokens.Current().Type, tokens.Current().Line, tokens.Current().Offset);
        // }

        // public ExpressionNode ReturnExpressionNode(TokenType tokenType, int line, int offset)
        // {
        //     if (tokenType == TokenType.MATH_OP)
        //     {
        //         return new ExpressionNode(TokenType.MATH_OP, line, offset);
        //     }
        //     else if (tokenType == TokenType.BOOL_OP)
        //     {
        //         return new ExpressionNode(TokenType.BOOL_OP, line, offset);
        //     }
        //     throw new Exception();
        // }

        public void ParseFunctionDeclaration(TokenStream tokens)
        {
            //Make func subtree
        }
    }
}