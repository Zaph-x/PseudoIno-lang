﻿using AbstractSyntaxTree.Objects.Nodes;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Lexer.Objects;

namespace Contextual_analysis
{
    public class TypeChecker : Visitor
    {
        private TokenType TermType {get;set;} = TokenType.ERROR;
        private SymbolTable.SymbolTable _symbolTabelGlobal = new SymbolTable.SymbolTable();
        private SymbolTable.SymbolTableBuilder _symbolTableBuilder;
        public override object Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);
        }



        public override object Visit(TimeNode timeNode) { }

        public override object Visit(DeclParametersNode declParametersNode)
        {
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        public override object Visit(TimesNode timesNode) { }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            if (((VarNode)assignmentNode.Var).SymbolType == TokenType.ERROR)
            {
                assignmentNode.Var.Accept(this);

                _symbolTableBuilder.AddSymbol(assignmentNode);
                //hvis id == assigmenttype så ok ellers error.
                AstNode typeid = ((AstNode)assignmentNode.Var);
                AstNode typeassigment = ((AstNode)assignmentNode.Assignment);
            //if(typeid.)


            }
            //VarNode id = (VarNode)assignmentNode.Var.Accept(this);
            //dette tilføjer venstre side til symboltable, brug addref til at sætte højre side af statement. De skal ske til sidst når alt er kontrolleret.
            _symbolTableBuilder.AddSymbol(assignmentNode);
            //((ExpressionNode)assignmentNode.Assignment).Accept(this);
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public override object Visit(StatementNode statementNode) { }

        public override object Visit(WithNode withNode) { }

        public override object Visit(WaitNode waitNode)
        {
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
        }

        public override object Visit(VarNode varNode) { }

        public override object Visit(ValNode valNode) { }

        public override object Visit(TimeSecondNode timeSecondNode) { }

        public override object Visit(TimeMinuteNode timeMinuteNode) { }

        public override object Visit(TimeMillisecondNode timeMillisecondNode) { }

        public override object Visit(TimeHourNode timeHourNode) { }

        public override object Visit(RightParenthesisNode rightParenthesisNode) { }

        public override object Visit(NumericNode numericNode) { }

        public override object Visit(NewlineNode newlineNode) { }

        public override object Visit(LeftParenthesisNode leftParenthesisNode) { }

        public override object Visit(InNode inNode) { }
        public override object Visit(EqualNode equalNode) { }

        public override object Visit(EqualsNode equalsNode) { }

        public override object Visit(EOFNode eOFNode) { }

        public override object Visit(EpsilonNode epsilonNode) { }

        public override object Visit(DoNode doNode) { }

        public override object Visit(ProgramNode programNode)
        {
            _symbolTableBuilder = new SymbolTable.SymbolTableBuilder(_symbolTabelGlobal);
            _symbolTableBuilder.CurrentSymbolTable = _symbolTabelGlobal;

            _symbolTableBuilder.OpenScope(TokenType.PROG, "main");
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Parent = programNode);
                //programNode.FunctionDefinitons.ForEach(node => _symbolTableBuilder.AddNode(node));
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
                programNode.Statements.ForEach(node => node.Parent = programNode);
                //programNode.Statements.ForEach(node => _symbolTableBuilder.AddNode(node));
            }
            programNode.LoopFunction.Accept(this);
            _symbolTableBuilder.CloseScope();

            _symbolTableBuilder.MakeFinalTable();
        }

        public override object Visit(CallNode callNode)
        {
            callNode.Id.Accept(this);
            _symbolTableBuilder.AddRef(callNode);
            callNode.Parameters.ForEach(node => node.Accept(this));
            foreach (var node in callNode.Parameters)
            {
                if (node.Type == TokenType.VAR)
                {
                    _symbolTableBuilder.AddRef(node);
                }
            }
        }

        public override object Visit(EndNode endNode) { }
        public override object Visit(AndNode andNode) { }
        public override object Visit(PinNode pinNode) { }
        public override object Visit(APinNode apinNode) { }
        public override object Visit(DPinNode dpinNode) { }
        public override object Visit(OperatorNode operatorNode) { }
        public override object Visit(BoolOperatorNode boolOperatorNode) { }
        public override object Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
        }
        public override object Visit(DivideNode divideNode) { }
        public override object Visit(ExpressionNode expressionNode)
        {
            expressionNode.Term.Accept(this);

            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
        }
        public override object Visit(ForNode forNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.FOR, "for");
            forNode.CountingVariable.Accept(this);
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
                forNode.Statements.ForEach(node => node.Parent = forNode);
            }
            //forNode.Accept(this);
            _symbolTableBuilder.CloseScope();
        }
        public override object Visit(FuncNode funcNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.FUNC, funcNode.Name.Id);

            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Parent = funcNode);

                funcNode.Statements.ForEach(node => node.Accept(this));
            }

            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            _symbolTableBuilder.CloseScope();

        }
        public override object Visit(GreaterNode greaterNode)
        {
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
        }
        public override object Visit(IfStatementNode ifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.IFSTMNT, "if");
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);

                ifStatementNode.Statements.ForEach(node => node.Accept(this));

            }
            _symbolTableBuilder.CloseScope();
        }
        public override object Visit(LessNode lessNode)
        {
            lessNode.OrEqualNode.Accept(this);
        }
        public override object Visit(LoopNode loopNode) { }
        public override object Visit(MathOperatorNode mathOperatorNode) { }
        public override object Visit(PlusNode plusNode) { }
        public override object Visit(MinusNode minusNode) { }
        public override object Visit(ModuloNode moduloNode) { }
        public override object Visit(OrNode orNode) { }
        public override object Visit(StringNode stringNode) { }
        public override object Visit(WhileNode whileNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.WHILE, "while");

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);

            }
            _symbolTableBuilder.CloseScope();
        }
        public override object Visit(ElseStatementNode elseStatement)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSESTMNT, "else");
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);

            }
            //elseStatement.Accept(this);
            _symbolTableBuilder.CloseScope();
            //symbolTabel.AddNode(elseStatement.ToString(), elseStatement);
        }
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSEIFSTMNT, "elseif");
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);

            }
            //elseifStatementNode.Accept(this);
            _symbolTableBuilder.CloseScope();
            //symbolTabel.AddNode(elseifStatementNode.ToString(), elseifStatementNode);
        }
        public override object Visit(RangeNode rangeNode)
        {
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
        }

        public override object Visit(ReturnNode returnNode)
        {
            returnNode.ReturnValue.Accept(this);
        }

        public TokenType GetExpressionType(ExpressionNode node) 
        {   
            if(TermType == TokenType.ERROR)
                TermType = ((AstNode)node.Term).Type;
            if (node.Expression != null)
            {
                if (((AstNode)node.Expression.Term).Type != TermType || ((AstNode)node.Expression.Term).Type != TokenType.EXPR) {
                    // TODO ERROR code goes here
                } else {
                    return GetExpressionType(node.Expression);
                }
            } else if (((AstNode)node.Term).Type == TokenType.EXPR) {
                return GetExpressionType((ExpressionNode)node.Term);
            }
            TermType = TokenType.ERROR;
            return ((AstNode)node.Term).Type;
        }
    }
}
