using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Lexer.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Exceptions;
using System;
using Parser.Objects;

namespace AbstractSyntaxTree.Objects
{
    public class ASTHelper
    {
        public static bool HasError { get; set; } = false;
        //public LinkedList<ScannerToken> RawTokens { get; set; }
        public TokenStream RawTokens { get; set; }
        private List<ScannerToken> tokenBuffer = new List<ScannerToken>();
        public ProgramNode Root { get; private set; }

        public ASTHelper(List<ScannerToken> tokens)
        {
            RawTokens = new TokenStream(tokens);
            Parse(RawTokens, Root);
        }
        public ASTHelper(TokenStream tokens)
        {
            RawTokens = tokens;
            Parse(RawTokens, Root);
        }

        public AstNode Parse(TokenStream token, IScope currentScope)
        {
            currentScope = Root = new ProgramNode(token.PROG.Line, token.PROG.Offset);
            token.Advance();
            ParseNext(token, Root);
            return Root;
        }

        public void ParseNext(TokenStream token, IScope currentScope)
        {
            if (token.AtEnd()) return;
            if (token.Peek().Type == TokenType.ASSIGN)
            {
                currentScope.Statements.Add(ParseAssignment(token));
            }
            else if (token.Current().Type == TokenType.IF)
            {
                currentScope.Statements.Add(ParseIf(token, currentScope));
            }
            else if (token.Current().Type == TokenType.FUNC)
            {
                if (!currentScope.GetType().Equals(typeof(ProgramNode)))
                {
                    new UnexpectedSequenceException("Functions can not be defined inside of functions.");
                    return;
                }
                // We know we are in the global scope because of the above check
                FunctionDefinitonNode functionDefinitonNode = ParseFunciondefinitionNode(token, currentScope);
                if (functionDefinitonNode.Value == "loop")
                {
                    ((ProgramNode) currentScope).LoopFunction = functionDefinitonNode;
                }
                else
                {
                    ((ProgramNode)currentScope).FunctionDefinitons.Add(ParseFunciondefinitionNode(token,currentScope));
                }
            }
            else if (token.Current().Type == TokenType.WAIT)
            {
                currentScope.Statements.Add(ParseWait(token, currentScope));
            }
            else if (token.Current().Type == TokenType.BEGIN)
            {
                currentScope.Statements.Add(ParseBegin(token, currentScope));
            }
            else if (token.Current().Type == TokenType.CALL)
            {
                currentScope.Statements.Add(ParseCall(token, currentScope));
            }
            else if (token.Current().Type == TokenType.END)
            {
                return;
            }
            else if (token.Current().Type == TokenType.ELSE)
            {
                return;
            }
            token.Advance();
            ParseNext(token, currentScope);
        }
        
        public StatementNode ParseNext(TokenStream token, IScope currentScope, IScope previousScope)
        {
            if (token.Peek().Type == TokenType.ASSIGN)
            {
                currentScope.Statements.Add(ParseAssignment(token));
                token.Advance();
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Current().Type == TokenType.IF)
            {
                currentScope.Statements.Add(ParseIf(token, currentScope));
                token.Advance();
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Current().Type == TokenType.FUNC)
            {
                if (!currentScope.GetType().Equals(typeof(ProgramNode)))
                {
                    new UnexpectedSequenceException("Functions can not be defined inside of functions.");
                    return null;
                }
                // We know we are in the global scope because of the above check
                FunctionDefinitonNode functionDefinitonNode = ParseFunciondefinitionNode(token, currentScope);
                if (functionDefinitonNode.Value == "loop")
                {
                    ((ProgramNode) currentScope).LoopFunction = functionDefinitonNode;
                }
                else
                {
                    ((ProgramNode)currentScope).FunctionDefinitons.Add(ParseFunciondefinitionNode(token,currentScope));
                }
            }
            else if (token.Current().Type == TokenType.WAIT)
            {
                currentScope.Statements.Add(ParseWait(token, currentScope));
                token.Advance();
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Current().Type == TokenType.BEGIN)
            {
                currentScope.Statements.Add(ParseBegin(token, currentScope));
                token.Advance();
                ParseNext(token, currentScope, previousScope);
            }
            else if (token.Current().Type == TokenType.CALL)
            {
                currentScope.Statements.Add(ParseCall(token, currentScope));
                token.Advance();
                ParseNext(token, currentScope, previousScope);
            }
            return null;
        }

        private StatementNode ParseIf(TokenStream token, IScope currentScope)
        {
            IfStatementNode ifStatementNode =
                new IfStatementNode(token.Current().Line, token.Current().Offset);
            token.Advance();
            ifStatementNode.Val = ParseValNode(token);
            //token.Advance();
            ifStatementNode.Expression = ParseExpression(token);
            //Parse Statements
            token.Advance();
            token.Advance();
            ParseNext(token, ifStatementNode);
            if (token.Current().Type == TokenType.ELSE)
            {
                if (token.Peek().Type == TokenType.IF)
                {
                    token.Advance();
                    token.Advance();
                    ifStatementNode.ElseifStatementNode.Add(ParseElseIf(token, currentScope));
                    if (token.Current().Type == TokenType.ELSE)
                    {
                        ElseStatementNode elseStatementNode = new ElseStatementNode(token.Current().Line, token.Current().Offset);
                        token.Advance();
                        ParseNext(token, elseStatementNode);
                        ifStatementNode.ElseStatementNode = elseStatementNode;
                    }
                }
                else
                {
                    ElseStatementNode elseStatementNode = new ElseStatementNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    ParseNext(token, elseStatementNode);
                    ifStatementNode.ElseStatementNode = elseStatementNode;
                }
            }
            token.Advance();
            return ifStatementNode;
        }
        
        public ElseifStatementNode ParseElseIf(TokenStream token, IScope currentScope)
        {
            ElseifStatementNode elseifStatementNode =
                new ElseifStatementNode(token.Current().Line, token.Current().Offset);
            elseifStatementNode.Val = ParseValNode(token);
            //token.Advance();
            elseifStatementNode.Expression = ParseExpression(token);
            //Parse Statements
            token.Advance();
            token.Advance();
            //Parse Statements
            ParseNext(token, elseifStatementNode);
            return elseifStatementNode;
        }

        private StatementNode ParseWait(TokenStream token, IScope currentScope)
        {
            WaitNode node = new WaitNode(token.Current().Line, token.Current().Offset);
            token.Advance();
            node.TimeAmount = new NumericNode(token.Current().Value,token.Current().Line,token.Current().Offset);
            token.Advance();
            node.TimeModifier = ParseTimeNode(token);
            return node;
        }

        private StatementNode ParseBegin(TokenStream token, IScope currentScope)
        {
            BeginNode beginNode = new BeginNode(token.Current().Line,token.Current().Offset);
            token.Advance();
            beginNode.LoopNode = ParseLoop(token, currentScope);
            token.Prev();
            return beginNode;
        }

        public LoopNode ParseLoop(TokenStream token, IScope currentScope)
        {
            switch (token.Current().Type)
            {
                case TokenType.WHILE:
                    WhileNode whileNode = new WhileNode(token.Current().Line,token.Current().Offset);
                    token.Advance();
                    whileNode.ValNode = ParseValNode(token);
                    token.Advance();
                    whileNode.ExpressionNode = ParseExpression(token);
                    token.Advance();
                    ParseNext(token, whileNode);
                    return whileNode;
                case TokenType.FOR:
                    ForNode forNode = new ForNode(token.Current().Line,token.Current().Offset);
                    token.Advance();
                    forNode.ValNode = ParseValNode(token);
                    token.Advance();
                    token.Advance();
                    forNode.RangeNode = ParseRange(token,currentScope);
                    token.Advance();
                    ParseNext(token, forNode);
                    return forNode;
                
            }

            return null;
        }

        private RangeNode ParseRange(TokenStream token, IScope currentScope)
        {
            RangeNode rangeNode = new RangeNode(token.Current().Line,token.Current().Offset);
            rangeNode.LeftHand = ParseValNode(token);
            token.Advance();
            token.Advance();
            rangeNode.RightHand = ParseValNode(token);
            return rangeNode;
        }
        private StatementNode ParseCall(TokenStream token, IScope currentScope)
        {
            CallNode callNode = new CallNode(token.Current().Line,token.Current().Offset);
            token.Advance();
            callNode.VarNode = new VarNode(token.Current().Value,token.Current().Line,token.Current().Offset);
            token.Advance();
            token.Advance();
            callNode.RightHand = ParseCallParameters(token,currentScope);
            token.Prev();
            return callNode;
        }

        public CallParametersNode ParseCallParameters(TokenStream token, IScope currentScope)
        {
            CallParametersNode callParametersNode = new CallParametersNode(token.Current().Line,token.Current().Offset);
            callParametersNode.ValNode = ParseValNode(token);
            token.Advance();
            token.Advance();
            if (token.Current().Type == TokenType.VAR ||
                token.Current().Type == TokenType.NUMERIC || 
                token.Current().Type == TokenType.STRING || 
                token.Current().Type == TokenType.PIN)
            {
                callParametersNode.RightHand = ParseCallParameters(token, currentScope);
            }
            token.Prev();
            return callParametersNode;
        }
        public FunctionDefinitonNode ParseFunciondefinitionNode(TokenStream token, IScope currentScope)
        {
            token.Advance();
            FunctionDefinitonNode funcDef = new FunctionDefinitonNode(token.Current().Value, token.Current().Line, token.Current().Offset);
            token.Advance();
            if (token.Current().Type == TokenType.WITH)
            {
                token.Advance();
                funcDef.LeftHand = ParseValNode(token);
                if (token.Peek().Type == TokenType.SEPARATOR)
                {
                    token.Advance();
                    token.Advance();
                    funcDef.RightHand = ParseCallParameters(token, currentScope);
                }
            }
            
            ParseNext(token, funcDef);
            /*while (token.Value.Type != TokenType.END && token.Next.Value.Value != funcDef.Value)
            {
                // Parse all statements
            }*/
            return funcDef;
        }

        public AssignmentNode ParseAssignment(TokenStream token)
        {
            tokenBuffer = new List<ScannerToken>();
            AssignmentNode node = new AssignmentNode(token.Current().Line, token.Current().Offset);
            node.LeftHand = new VarNode(token.Current().Value, token.Current().Line, token.Current().Offset);
            token.Advance();
            token.Advance();
            node.RightHand = ParseValNode(token);
            node.ExpressionHand = ParseExpression(token);
            return node;
        }

        public ValNode ParseValNode(TokenStream token)
        {
            switch (token.Current().Type)
            {
                case TokenType.VAR:
                    return new VarNode(token.Current().Value, token.Current().Line, token.Current().Offset);
                case TokenType.NUMERIC:
                    return new NumericNode(token.Current().Value, token.Current().Line, token.Current().Offset);
                case TokenType.STRING:
                    return new StringNode(token.Current().Value, token.Current().Line, token.Current().Offset);
                case TokenType.DPIN:
                    return new DPinNode(token.Current().Value, token.Current().Line, token.Current().Offset);
                case TokenType.APIN:
                    return new DPinNode(token.Current().Value, token.Current().Line, token.Current().Offset);
            }
            return null;
        }

        public TimeNode ParseTimeNode(TokenStream token)
        {
            switch (token.Current().Type)
            {
                case TokenType.TIME_HR:
                    return new TimeHourNode(token.Current().Line, token.Current().Offset);
                case TokenType.TIME_MIN:
                    return new TimeMinuteNode(token.Current().Line, token.Current().Offset);
                case TokenType.TIME_SEC:
                    return new TimeSecondNode(token.Current().Line, token.Current().Offset);
                case TokenType.TIME_MS:
                    return new TimeMillisecondNode(token.Current().Line, token.Current().Offset);
                default:
                    return null;
            }
        }

        public ExpressionNode ParseExpression(TokenStream token)
        {
            token.Advance();
            switch (token.Current().Type)
            {
                case TokenType.OP_PLUS:
                    ExpressionNode plusExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    plusExpressionNode.Type = TokenType.MATHEXPR;
                    plusExpressionNode.Operator = new PlusNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    plusExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    plusExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return plusExpressionNode;
                case TokenType.OP_MINUS:
                    ExpressionNode minusExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    minusExpressionNode.Type = TokenType.MATHEXPR;
                    minusExpressionNode.Operator = new MinusNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    minusExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    minusExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return minusExpressionNode;
                case TokenType.OP_TIMES:
                    ExpressionNode timesExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    timesExpressionNode.Type = TokenType.MATHEXPR;
                    timesExpressionNode.Operator = new TimesNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    timesExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    timesExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return timesExpressionNode;
                case TokenType.OP_DIVIDE:
                    ExpressionNode divideExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    divideExpressionNode.Type = TokenType.MATHEXPR;
                    divideExpressionNode.Operator = new DivideNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    divideExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    divideExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return divideExpressionNode;
                case TokenType.OP_AND:
                    ExpressionNode andExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    andExpressionNode.Type = TokenType.BOOLEXPR;
                    andExpressionNode.Operator = new AndNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    andExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    andExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return andExpressionNode;
                case TokenType.OP_OR:
                    ExpressionNode orExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    orExpressionNode.Type = TokenType.BOOLEXPR;
                    orExpressionNode.Operator = new OrNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    orExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    orExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return orExpressionNode;
                case TokenType.OP_LESS:
                    ExpressionNode lessExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    lessExpressionNode.Type = TokenType.BOOLEXPR;
                    lessExpressionNode.Operator = new LessNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    lessExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    lessExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return lessExpressionNode;
                case TokenType.OP_GREATER:
                    ExpressionNode greaterExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    greaterExpressionNode.Type = TokenType.BOOLEXPR;
                    greaterExpressionNode.Operator = new GreaterNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    greaterExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    greaterExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return greaterExpressionNode;
                case TokenType.OP_OREQUAL:
                    ExpressionNode orEqualExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    orEqualExpressionNode.Type = TokenType.BOOLEXPR;
                    orEqualExpressionNode.Operator = new OrEqualNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    orEqualExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    orEqualExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return orEqualExpressionNode;
                case TokenType.OP_EQUAL:
                    ExpressionNode equalExpressionNode = new ExpressionNode(TokenType.EXPR, token.Current().Line, token.Current().Offset);
                    equalExpressionNode.Type = TokenType.BOOLEXPR;
                    equalExpressionNode.Operator = new EqualNode(token.Current().Line, token.Current().Offset);
                    token.Advance();
                    equalExpressionNode.Value = ParseValNode(token);
                    token.Advance();
                    equalExpressionNode.Expression = ParseExpression(token);
                    token.Prev();
                    return equalExpressionNode;
                case TokenType.DO:
                    return null;
                default:
                    token.Prev();
                    return null;
            }
        }
    }
}