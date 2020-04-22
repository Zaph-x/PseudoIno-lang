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

            ParseNext(token.Next, Root);
            return Root;
        }

        public void ParseNext(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            if (token == null) return;
            if (token.Next?.Value?.Type == TokenType.ASSIGN)
            {
                currentScope.Statements.Add(ParseAssignment(token));
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
                ((ProgramNode)currentScope).FunctionDefinitons.Add(ParseFunciondefinitionNode(token,currentScope));
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
            ParseNext(token, currentScope);
        }
        
        public StatementNode ParseNext(LinkedListNode<ScannerToken> token, IScope currentScope, IScope previousScope)
        {
            if (token.Next?.Value.Type == TokenType.ASSIGN)
            {
                currentScope.Statements.Add(ParseAssignment(token));
                token = token.Next;
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Value.Type == TokenType.IF)
            {
                currentScope.Statements.Add(ParseIf(token, currentScope));
                token = token.Next;
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Value.Type == TokenType.FUNC)
            {
                if (!currentScope.GetType().Equals(typeof(ProgramNode)))
                {
                    new UnexpectedSequenceException("Functions can not be defined inside of functions.");
                    return null;
                }
                // We know we are in the global scope because of the above check
                ((ProgramNode)currentScope).FunctionDefinitons.Add(ParseFunciondefinitionNode(token,currentScope));
            }
            else if (token.Value.Type == TokenType.WAIT)
            {
                currentScope.Statements.Add(ParseWait(token, currentScope));
                token = token.Next;
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Value.Type == TokenType.BEGIN)
            {
                currentScope.Statements.Add(ParseBegin(token, currentScope));
                token = token.Next;
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Value.Type == TokenType.CALL)
            {
                currentScope.Statements.Add(ParseCall(token, currentScope));
                token = token.Next;
                ParseNext(token, currentScope, previousScope);
            }
            return null;
        }

        private StatementNode ParseIf(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            IfStatementNode ifStatementNode =
                new IfStatementNode(token.Value.Line, token.Value.Offset);
            ifStatementNode.Val = ParseValNode(token.Next);
            ifStatementNode.Expression = ParseExpression(token.Next);
            //Parse Statements
            ParseNext(token, ifStatementNode, currentScope);
            return ifStatementNode;
        }

        private StatementNode ParseWait(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            WaitNode node = new WaitNode(token.Value.Line, token.Value.Offset);
            node.TimeModifier = ParseTimeNode(token);
            return node;
        }

        private StatementNode ParseBegin(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            BeginNode beginNode = new BeginNode(token.Value.Line,token.Value.Offset);
            beginNode.LoopNode = ParseLoop(token.Next, currentScope);
            return beginNode;
        }

        public LoopNode ParseLoop(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            switch (token.Value.Type)
            {
                case TokenType.WHILE:
                    WhileNode whileNode = new WhileNode(token.Value.Line,token.Value.Offset);
                    whileNode.ValNode = ParseValNode(token.Next);
                    whileNode.ExpressionNode = ParseExpression(token.Next.Next);
                    ParseNext(token.Next.Next.Next, whileNode, currentScope);
                    break;
                case TokenType.FOR:
                    ForNode forNode = new ForNode(token.Value.Line,token.Value.Offset);
                    forNode.ValNode = ParseValNode(token.Next);
                    forNode.ExpressionNode = ParseExpression(token.Next.Next);
                    ParseNext(token.Next.Next.Next, forNode, currentScope);
                    break;
                
            }

            return null;
        }
        private StatementNode ParseCall(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            CallNode callNode = new CallNode(token.Value.Line,token.Value.Offset);
            callNode.VarNode = new VarNode(token.Next.Value.Value,token.Next.Value.Line,token.Next.Value.Offset);
            callNode.RightHand = ParseCallParameters(token.Next.Next,currentScope);
            return callNode;
        }

        public CallParametersNode ParseCallParameters(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            CallParametersNode callParametersNode = new CallParametersNode(token.Value.Line,token.Value.Offset);
            callParametersNode.ValNode = ParseValNode(token);
            if (token.Next.Value.Type == TokenType.VAR ||
                token.Next.Value.Type == TokenType.NUMERIC || 
                token.Next.Value.Type == TokenType.STRING || 
                token.Next.Value.Type == TokenType.PIN)
            {
                callParametersNode.RightHand = ParseCallParameters(token.Next, currentScope);
            }
            
            return null;
        }
        public FunctionDefinitonNode ParseFunciondefinitionNode(LinkedListNode<ScannerToken> token, IScope currentScope)
        {
            FunctionDefinitonNode funcDef = new FunctionDefinitonNode(token.Next.Value.Value, token.Value.Line, token.Value.Offset);
            ParseNext(token.Next, funcDef);
            /*while (token.Value.Type != TokenType.END && token.Next.Value.Value != funcDef.Value)
            {
                // Parse all statements
            }*/
            return funcDef;
        }

        public AssignmentNode ParseAssignment(LinkedListNode<ScannerToken> token)
        {
            tokenBuffer = new List<ScannerToken>();
            AssignmentNode node = new AssignmentNode(token.Value.Line, token.Value.Offset);
            node.LeftHand = new VarNode(token.Value.Value, token.Value.Line, token.Value.Offset);
            token = token.Next.Next;
            node.RightHand = ParseValNode(token);
            token = token.Next;
            node.ExpressionHand = ParseExpression(token);
            return node;
        }

        public ValNode ParseValNode(LinkedListNode<ScannerToken> token)
        {
            switch (token.Value.Type)
            {
                case TokenType.VAR:
                    return new VarNode(token.Value.Value, token.Value.Line, token.Value.Offset);
                case TokenType.NUMERIC:
                    return new NumericNode(token.Value.Value, token.Value.Line, token.Value.Offset);
                case TokenType.STRING:
                    return new StringNode(token.Value.Value, token.Value.Line, token.Value.Offset);
                case TokenType.DPIN:
                    return new DPinNode(token.Value.Value, token.Value.Line, token.Value.Offset);
                case TokenType.APIN:
                    return new DPinNode(token.Value.Value, token.Value.Line, token.Value.Offset);
            }
            return null;
        }

        public TimeNode ParseTimeNode(LinkedListNode<ScannerToken> token)
        {
            switch (token.Value.Type)
            {
                case TokenType.TIME_HR:
                    return new TimeHourNode(token.Value.Line, token.Value.Offset);
                case TokenType.TIME_MIN:
                    return new TimeHourNode(token.Value.Line, token.Value.Offset);
                case TokenType.TIME_SEC:
                    return new TimeHourNode(token.Value.Line, token.Value.Offset);
                case TokenType.TIME_MS:
                    return new TimeHourNode(token.Value.Line, token.Value.Offset);
                default:
                    return null;
            }
        }

        public ExpressionNode ParseExpression(LinkedListNode<ScannerToken> token)
        {
            switch (token.Value.Type)
            {
                case TokenType.OP_PLUS:
                    ExpressionNode plusExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    plusExpressionNode.Type = TokenType.MATHEXPR;
                    plusExpressionNode.Operator = new PlusNode(token.Value.Line, token.Value.Offset);
                    plusExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    plusExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return plusExpressionNode;
                case TokenType.OP_MINUS:
                    ExpressionNode minusExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    minusExpressionNode.Type = TokenType.MATHEXPR;
                    minusExpressionNode.Operator = new MinusNode(token.Value.Line, token.Value.Offset);
                    minusExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    minusExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return minusExpressionNode;
                case TokenType.OP_TIMES:
                    ExpressionNode timesExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    timesExpressionNode.Type = TokenType.MATHEXPR;
                    timesExpressionNode.Operator = new TimesNode(token.Value.Line, token.Value.Offset);
                    timesExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    timesExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return timesExpressionNode;
                case TokenType.OP_DIVIDE:
                    ExpressionNode divideExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    divideExpressionNode.Type = TokenType.MATHEXPR;
                    divideExpressionNode.Operator = new DivideNode(token.Value.Line, token.Value.Offset);
                    divideExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    divideExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return divideExpressionNode;
                case TokenType.OP_AND:
                    ExpressionNode andExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    andExpressionNode.Type = TokenType.BOOLEXPR;
                    andExpressionNode.Operator = new AndNode(token.Value.Line, token.Value.Offset);
                    andExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    andExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return andExpressionNode;
                case TokenType.OP_OR:
                    ExpressionNode orExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    orExpressionNode.Type = TokenType.BOOLEXPR;
                    orExpressionNode.Operator = new OrNode(token.Value.Line, token.Value.Offset);
                    orExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    orExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return orExpressionNode;
                case TokenType.OP_LESS:
                    ExpressionNode lessExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    lessExpressionNode.Type = TokenType.BOOLEXPR;
                    lessExpressionNode.Operator = new LessNode(token.Value.Line, token.Value.Offset);
                    lessExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    lessExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return lessExpressionNode;
                case TokenType.OP_GREATER:
                    ExpressionNode greaterExpressionNode = new ExpressionNode(TokenType.EXPR, token.Value.Line, token.Value.Offset);
                    greaterExpressionNode.Type = TokenType.BOOLEXPR;
                    greaterExpressionNode.Operator = new GreaterNode(token.Value.Line, token.Value.Offset);
                    greaterExpressionNode.Value = ParseValNode(token.Next);
                    token = token.Next;
                    greaterExpressionNode.Expression = ParseExpression(token.Next);
                    token = token.Next;
                    return greaterExpressionNode;
            }
            return null;
        }
    }
}