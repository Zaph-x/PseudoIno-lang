using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Lexer.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Exceptions;
using System;

namespace AbstractSyntaxTree.Objects
{
    public class ASTHelper
    {
        public static bool HasError { get; set; } = false;
        public LinkedList<ScannerToken> RawTokens { get; set; }
        private List<ScannerToken> tokenBuffer = new List<ScannerToken>();
        public ProgramNode Root { get; private set; }

        public ASTHelper(List<ScannerToken> tokens)
        {
            RawTokens = new LinkedList<ScannerToken>(tokens);
            Parse(RawTokens.First, Root);
        }
        public ASTHelper(LinkedList<ScannerToken> tokens)
        {
            RawTokens = tokens;
            Parse(RawTokens.First, Root);
        }

        public AstNode Parse(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            if (token.Value.Type == TokenType.PROG)
            {
                currentScope = Root = new ProgramNode(token.Value.Line, token.Value.Offset);
            }

            ParseNext(token.Next, Root, null);
            return Root;
        }

        public void ParseNext(LinkedListNode<ScannerToken> token, IScope currentScope, IScope previousScope)
        {
            if (token.Next?.Value.Type == TokenType.ASSIGN)
            {
                currentScope.Statements.Add(ParseAssignment(token, currentScope));
            }
            else if (token.Value.Type == TokenType.IF)
            {
                currentScope.Statements.Add(ParseIf(token, currentScope));
            }
            else if (token.Value.Type == TokenType.FUNC)
            {
                if (!currentScope.GetType().Equals(typeof(ProgramNode)))
                {
                    new UnexpectedSequenceException("Functions can not be defined inside of functions.");
                    return;
                }
                // We know we are in the global scope because of the above check
                ((ProgramNode)currentScope).FunctionDefinitons.Add(ParseFunciondefinitionNode(token));
            }
            else if (token.Value.Type == TokenType.WAIT)
            {
                currentScope.Statements.Add(ParseWait(token, currentScope));
            }
            else if (token.Value.Type == TokenType.BEGIN)
            {
                currentScope.Statements.Add(ParseBegin(token, currentScope));
            }
            else if (token.Value.Type == TokenType.CALL)
            {
                currentScope.Statements.Add(ParseCall(token, currentScope));
            }
            token = token.Next;
            ParseNext(token, currentScope, previousScope);
        }

        private StatementNode ParseIf(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            throw new NotImplementedException();
        }

        private StatementNode ParseWait(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            throw new NotImplementedException();
        }

        private StatementNode ParseBegin(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            throw new NotImplementedException();
        }

        private StatementNode ParseCall(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            throw new NotImplementedException();
        }

        public FunctionDefinitonNode ParseFunciondefinitionNode(LinkedListNode<ScannerToken> token)
        {
            FunctionDefinitonNode funcDef = new FunctionDefinitonNode(token.Next.Value.Value, token.Value.Line, token.Value.Offset);
            while (token.Value.Type != TokenType.END && token.Next.Value.Value != funcDef.Value) {
                // Parse all statements
            }
            return funcDef;
        }

        public AssignmentNode ParseAssignment(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            tokenBuffer = new List<ScannerToken>();
            AssignmentNode node = new AssignmentNode(token.Value.Line, token.Value.Offset);
            node.LeftHand = new VarNode(token.Value.Value, token.Value.Line, token.Value.Offset);
            token = token.Next.Next;
            node.RightHand = ParseValNode(token, currentScope);
            return node;
        }

        public ValNode ParseValNode(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            switch (token.Value.Type)
            {
                case TokenType.NUMERIC:
                    return new NumericNode(token.Value.Value, token.Value.Line, token.Value.Offset);
                case TokenType.STRING:
                    return new StringNode(token.Value.Value, token.Value.Line, token.Value.Offset);
            }
            return null;
        }
    }
}