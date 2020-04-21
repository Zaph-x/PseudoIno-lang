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
            IfStatementNode ifStatementNode =
                new IfStatementNode(token.Value.Line, token.Value.Offset);
            ifStatementNode.Val = ParseValNode(token.Next,currentScope);
            ifStatementNode.Expression = ParseExpression(token.Next,currentScope);
            return ifStatementNode;
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

        public ExpressionNode ParseExpression(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            switch (token.Value.Type)
            {
                case TokenType.OP_PLUS:
                    ExpressionNode plusExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    plusExpressionNode.Type = TokenType.MATHEXPR;
                    plusExpressionNode.LeftHandSide = new PlusNode(token.Value.Line,token.Value.Offset);
                    plusExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    plusExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return plusExpressionNode;
                case TokenType.OP_MINUS:
                    ExpressionNode minusExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    minusExpressionNode.Type = TokenType.MATHEXPR;
                    minusExpressionNode.LeftHandSide = new MinusNode(token.Value.Line,token.Value.Offset);
                    minusExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    minusExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return minusExpressionNode;
                case TokenType.OP_TIMES:
                    ExpressionNode timesExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    timesExpressionNode.Type = TokenType.MATHEXPR;
                    timesExpressionNode.LeftHandSide = new TimesNode(token.Value.Line,token.Value.Offset);
                    timesExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    timesExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return timesExpressionNode;
                case TokenType.OP_DIVIDE:
                    ExpressionNode divideExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    divideExpressionNode.Type = TokenType.MATHEXPR;
                    divideExpressionNode.LeftHandSide = new DivideNode(token.Value.Line,token.Value.Offset);
                    divideExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    divideExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return divideExpressionNode;
                case TokenType.OP_AND:
                    ExpressionNode andExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    andExpressionNode.Type = TokenType.BOOLEXPR;
                    andExpressionNode.LeftHandSide = new AndNode(token.Value.Line,token.Value.Offset);
                    andExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    andExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return andExpressionNode;
                case TokenType.OP_OR:
                    ExpressionNode orExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    orExpressionNode.Type = TokenType.BOOLEXPR;
                    orExpressionNode.LeftHandSide = new OrNode(token.Value.Line,token.Value.Offset);
                    orExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    orExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return orExpressionNode;
                case TokenType.OP_LESS:
                    ExpressionNode lessExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    lessExpressionNode.Type = TokenType.BOOLEXPR;
                    lessExpressionNode.LeftHandSide = new LessNode(token.Value.Line,token.Value.Offset);
                    lessExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    lessExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return lessExpressionNode;
                case TokenType.OP_GREATER:
                    ExpressionNode greaterExpressionNode =
                        new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    greaterExpressionNode.Type = TokenType.BOOLEXPR;
                    greaterExpressionNode.LeftHandSide = new GreaterNode(token.Value.Line,token.Value.Offset);
                    greaterExpressionNode.Middel = ParseValNode(token.Next, currentScope);
                    greaterExpressionNode.RightHandSide = ParseExpression(token.Next, currentScope);
                    return greaterExpressionNode;
            }
            return null;
        }
    }
}