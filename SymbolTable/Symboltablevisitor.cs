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
        private SymbolTableBuilder _symbolTableBuilder;
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
            Print("BeginNode");
            Depth++;
            Depth--;
            beginNode.LoopNode.Accept(this);

        }



        public override void Visit(TimeNode timeNode)
        {
            Print("TimeNode");
            Depth++;
            Depth--;
        }

        public override void Visit(DeclParametersNode declParametersNode)
        {
            Print("DeclParametersNode");
            Depth++;
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
            Depth--;
        }

        public override void Visit(TimesNode timesNode)
        {
            Print("TimesNode");
            Depth++;

        }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            Print("FunctionLoopNode");
            Depth++;
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            Depth--;
        }

        public override void Visit(AssignmentNode assignmentNode)
        {
            Print("AssignmentNode");
            _symbolTableBuilder.AddSymbol(assignmentNode);
            //((ExpressionNode)assignmentNode.Assignment).Accept(this);
            Depth++;
            Depth--;
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public override void Visit(StatementNode statementNode)
        {
            Print("StatementNode");
            Depth++;
            Depth--;

        }

        public override void Visit(WithNode withNode)
        {
            Print("WithNode");
            Depth++;
            Depth--;

        }

        public override void Visit(WaitNode waitNode)
        {
            Print("WaitNode");
            Depth++;
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            Depth--;
        }

        public override void Visit(VarNode varNode)
        {
            Print("VarNode");
            Depth++;
            Depth--;

        }

        public override void Visit(ValNode valNode)
        {
            Print("ValNode");
            Depth++;
            Depth--;
        }

        public override void Visit(TimeSecondNode timeSecondNode)
        {
            Print("TimeSecondNode");
            Depth++;
            Depth--;

        }

        public override void Visit(TimeMinuteNode timeMinuteNode)
        {
            Print("TimeMinuteNode");
            Depth++;
            Depth--;

        }

        public override void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            Print("TimeMillisecondNode");
            Depth++;
            Depth--;

        }

        public override void Visit(TimeHourNode timeHourNode)
        {
            Print("TimeHourNode");
            Depth++;
            Depth--;


        }

        public override void Visit(RightParenthesisNode rightParenthesisNode)
        {
            Print("TimeMillisecondNode");
            Depth++;
            Depth--;

        }

        public override void Visit(NumericNode numericNode)
        {
            Print("NumericNode");
            Depth++;
            Depth--;


        }

        public override void Visit(NewlineNode newlineNode)
        {
            Print("NewlineNode");
            Depth++;
            Depth--;
        }

        public override void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            Print("LeftParenthesisNode");
            Depth++;
            Depth--;
        }

        public override void Visit(InNode inNode)
        {
            Print("InNode");
            Depth++;
            Depth--;

        }
        public override void Visit(EqualNode equalNode)
        {
            Print("EqualNode");
            Depth++;
            Depth--;

        }

        public override void Visit(EqualsNode equalsNode)
        {
            Print("EqualsNode");
            Depth++;
            Depth--;

        }

        public override void Visit(EOFNode eOFNode)
        {
            Print("EOFNode");
            Depth++;
            Depth--;

        }

        public override void Visit(EpsilonNode epsilonNode)
        {
            Print("EpsilonNode");
            Depth++;
            Depth--;

        }

        public override void Visit(DoNode doNode)
        {
            Print("DoNode");
            Depth++;
            Depth--;

        }

        public override void Visit(ProgramNode programNode)
        {
            _symbolTableBuilder = new SymbolTableBuilder(_symbolTabelGlobal);
            _symbolTableBuilder.CurrentSymbolTable = _symbolTabelGlobal;
            Print("Program");
            _symbolTableBuilder.OpenScope(TokenType.PROG,"main");
            Depth++;
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
            Depth--;
            _symbolTableBuilder.MakeFinalTable();
        }

        public override void Visit(CallNode callNode)
        {
            Print("CallNode");
            Depth++;
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
            Depth--;
        }

        public override void Visit(EndNode endNode)
        {
            Print("EndNode");
            Depth++;
            Depth--;
        }
        public override void Visit(AndNode andNode)
        {
            Print("AndNode");
            Depth++;
            Depth--;
        }
        public override void Visit(PinNode pinNode)
        {
            Print("PinNode");
            Depth++;
            Depth--;
        }
        public override void Visit(APinNode apinNode)
        {
            Print("APinNode");
            Depth++;
            Depth--;
        }
        public override void Visit(DPinNode dpinNode)
        {
            Print("DPinNode");
            Depth++;
            Depth--;
        }
        public override void Visit(OperatorNode operatorNode)
        {
            Print("OperatorNode");
            Depth++;
            Depth--;
        }
        public override void Visit(BoolOperatorNode boolOperatorNode)
        {
            Print("BoolOperatorNode");
            Depth++;
            Depth--;
        }
        public override void Visit(CallParametersNode callParametersNode)
        {
            Print("CallParametersNode");
            Depth++;
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
            Depth--;
        }
        public override void Visit(DivideNode divideNode)
        {
            Print("DivideNode");
            Depth++;
            Depth--;
        }
        public override void Visit(ExpressionNode expressionNode)
        {
            Print("ExpressionNode");
            Depth++;
            expressionNode.Term.Accept(this);
            
            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
            Depth--;
        }
        public override void Visit(ForNode forNode)
        {
            Print("ForNode");
            Depth++;
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
            Depth--;
        }
        public override void Visit(FuncNode funcNode)
        {
            Print("FuncNode");
            Depth++;
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
            Depth--;
           
        }
        public override void Visit(GreaterNode greaterNode)
        {
            Print("GreaterNode");
            Depth++;
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
            Depth--;
        }
        public override void Visit(IfStatementNode ifStatementNode)
        {
            Print("IfstatementNode");
            Depth++;
            _symbolTableBuilder.OpenScope(TokenType.IFSTMNT,"if");
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);
              
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
                
            }
            _symbolTableBuilder.CloseScope();
            Depth--;
        }
        public override void Visit(LessNode lessNode)
        {
            Print("LessNode");
            Depth++;
            lessNode.OrEqualNode.Accept(this);
            Depth--;
        }
        public override void Visit(LoopNode loopNode)
        {
            Print("LoopNode");
            Depth++;
            Depth--;
        }
        public override void Visit(MathOperatorNode mathOperatorNode)
        {
            Print("MathOperatorNode");
            Depth++;
            Depth--;
        }
        public override void Visit(PlusNode plusNode)
        {
            Print("PlusNode");
            Depth++;
            Depth--;
        }
        public override void Visit(MinusNode minusNode)
        {
            Print("MinusNode");
            Depth++;
            Depth--;
        }
        public override void Visit(ModuloNode moduloNode)
        {
            Print("ModuloNode");
            Depth++;
            Depth--;
        }
        public override void Visit(OrNode orNode)
        {
            Print("OrNode");
            Depth++;
            Depth--;
        }
        public override void Visit(StringNode stringNode)
        {
            Print("StringNode");
            Depth++;
            Depth--;
        }
        public override void Visit(WhileNode whileNode)
        {
            Print("WhileNode");
            Depth++;
            _symbolTableBuilder.OpenScope(TokenType.WHILE,"while");

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
                
            }
            _symbolTableBuilder.CloseScope();
            Depth--;
           
        }
        public override void Visit(ElseStatementNode elseStatement)
        {
            Print("ElseStatementNode");
            Depth++;
            _symbolTableBuilder.OpenScope(TokenType.ELSESTMNT,"else");
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                
            }
            //elseStatement.Accept(this);
            _symbolTableBuilder.CloseScope();
            Depth--;
            //symbolTabel.AddNode(elseStatement.ToString(), elseStatement);
        }
        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSEIFSTMNT,"elseif");
            Print("ElseifStatementNode");
            Depth++;
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);
                
            }
            //elseifStatementNode.Accept(this);
            _symbolTableBuilder.CloseScope();
            Depth--;
            //symbolTabel.AddNode(elseifStatementNode.ToString(), elseifStatementNode);
        }
        public override void Visit(RangeNode rangeNode)
        {
            Print("RangeNode");
            Depth++;
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
            Depth--;
        }

        public override void Visit(ReturnNode returnNode)
        {
            Print("ReturnNode");
            Depth++;
            returnNode.ReturnValue.Accept(this);
            Depth--;
        }

    }
}
