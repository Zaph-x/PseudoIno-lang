using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Lexer.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Exceptions;

namespace AbstractSyntaxTree.Objects
{
    public class ASTHelper
    {
        public static bool HasError { get; set; } = false;
        public LinkedList<ScannerToken> RawTokens { get; set; }
        private List<ScannerToken> tokenBuffer = new List<ScannerToken>();
        public ProgramNode Root { get; private set; }

        public ASTHelper(List<Token> tokens)
        {
            RawTokens = new LinkedList<ScannerToken>(tokens.Cast<ScannerToken>());
            Parse(RawTokens.First, Root);
        }

        public AstNode Parse(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            if (token.Value.Type == TokenType.PROG)
            {
                currentScope = new ProgramNode(token.Value.Line, token.Value.Offset);
            }

            return ParseNext(token.Next, Root);
        }

        public AstNode ParseNext(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            if (token.Next.Value.Type == TokenType.ASSIGN)
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
                    return null;
                }
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
        }

        public AssignmentNode ParseAssignment(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            tokenBuffer = new List<ScannerToken>();
            AssignmentNode node = new AssignmentNode(token.Value.Line, token.Value.Offset);
            node.LeftHand = new VarNode(token.Value.Value, token.Value.Line, token.Value.Offset);
            token = token.Next.Next;
            node.RightHand = ;
            return node;
        }

        public ValNode ParseValNode(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            return new ValNode(token.Value, token.Value.Line, token.Value.Offset);
        }
    }
}