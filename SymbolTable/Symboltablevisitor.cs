using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;

namespace SymbolTable
{
   public class Symboltablevisitor : Visitor
    {
        private int Depth { get; set; } = 0;
        private SymbolTable _symbolTabelGlobal = new SymbolTable();
        public SymbolTableBuilder _symbolTableBuilder;
        private void Print(string input)
        {
            string line = "";
            for (int i = 0; i < Depth; i++)
            {
                line += "|---";
            }
            Console.WriteLine(line + input);
        }
        public override void Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);
        }



        public override void Visit(TimeNode timeNode) { }

        public override void Visit(DeclParametersNode declParametersNode)
        {
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        public override void Visit(TimesNode timesNode) { }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        public override void Visit(AssignmentNode assignmentNode)
        {
            _symbolTableBuilder.AddSymbol(assignmentNode);
            //((ExpressionNode)assignmentNode.Assignment).Accept(this);
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public override void Visit(StatementNode statementNode) { }

        public override void Visit(WithNode withNode) { }

        public override void Visit(WaitNode waitNode)
        {
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
        }

        public override void Visit(VarNode varNode) { }

        public override void Visit(ValNode valNode) { }

        public override void Visit(TimeSecondNode timeSecondNode) { }

        public override void Visit(TimeMinuteNode timeMinuteNode) { }

        public override void Visit(TimeMillisecondNode timeMillisecondNode) { }

        public override void Visit(TimeHourNode timeHourNode) { }

        public override void Visit(RightParenthesisNode rightParenthesisNode) { }

        public override void Visit(NumericNode numericNode) { }

        public override void Visit(NewlineNode newlineNode) { }

        public override void Visit(LeftParenthesisNode leftParenthesisNode) { }

        public override void Visit(InNode inNode) { }
        public override void Visit(EqualNode equalNode) { }

        public override void Visit(EqualsNode equalsNode) { }

        public override void Visit(EOFNode eOFNode) { }

        public override void Visit(EpsilonNode epsilonNode) { }

        public override void Visit(DoNode doNode) { }

        public override void Visit(ProgramNode programNode)
        {
            _symbolTableBuilder = new SymbolTableBuilder(_symbolTabelGlobal);
            _symbolTableBuilder.CurrentSymbolTable = _symbolTabelGlobal;

            _symbolTableBuilder.OpenScope(TokenType.PROG,"main");
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

        public override void Visit(CallNode callNode)
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

        public override void Visit(EndNode endNode) { }
        public override void Visit(AndNode andNode) { }
        public override void Visit(PinNode pinNode) { }
        public override void Visit(APinNode apinNode) { }
        public override void Visit(DPinNode dpinNode) { }
        public override void Visit(OperatorNode operatorNode) { }
        public override void Visit(BoolOperatorNode boolOperatorNode) { }
        public override void Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
        }
        public override void Visit(DivideNode divideNode) { }
        public override void Visit(ExpressionNode expressionNode)
        {
            expressionNode.Term.Accept(this);
            
            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
        }
        public override void Visit(ForNode forNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.FOR,"for");
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
        public override void Visit(FuncNode funcNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.FUNC,funcNode.Name.Id);

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
        public override void Visit(GreaterNode greaterNode)
        {
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
        }
        public override void Visit(IfStatementNode ifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.IFSTMNT,"if");
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);
              
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
                
            }
            _symbolTableBuilder.CloseScope();
        }
        public override void Visit(LessNode lessNode)
        {
            lessNode.OrEqualNode.Accept(this);
        }
        public override void Visit(LoopNode loopNode) { }
        public override void Visit(MathOperatorNode mathOperatorNode) { }
        public override void Visit(PlusNode plusNode) { }
        public override void Visit(MinusNode minusNode) { }
        public override void Visit(ModuloNode moduloNode) { }
        public override void Visit(OrNode orNode) { }
        public override void Visit(StringNode stringNode) { }
        public override void Visit(WhileNode whileNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.WHILE,"while");

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
                
            }
            _symbolTableBuilder.CloseScope();
        }
        public override void Visit(ElseStatementNode elseStatement)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSESTMNT,"else");
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                
            }
            //elseStatement.Accept(this);
            _symbolTableBuilder.CloseScope();
            //symbolTabel.AddNode(elseStatement.ToString(), elseStatement);
        }
        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSEIFSTMNT,"elseif");
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
        public override void Visit(RangeNode rangeNode)
        {
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
        }

        public override void Visit(ReturnNode returnNode)
        {
            returnNode.ReturnValue.Accept(this);
        }
    }
}
