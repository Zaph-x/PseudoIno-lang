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
        private int Indent { get; set; } = 0;
        private NodeSymbolTab _symbolTabelGlobal = new NodeSymbolTab();
        private void Print(string input)
        {
            string line = "";
            for (int i = 0; i < Indent; i++)
            {
                line += "|---";
            }
            Console.WriteLine(line + input);
        }
        public override void Visit(BeginNode beginNode)
        {
            Print("BeginNode");
            Indent++;
            Indent--;
            beginNode.LoopNode.Accept(this);

        }



        public override void Visit(TimeNode timeNode)
        {
            Print("TimeNode");
            Indent++;
            Indent--;
        }

        public override void Visit(DeclParametersNode declParametersNode)
        {
            Print("DeclParametersNode");
            Indent++;
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
            Indent--;
        }

        public override void Visit(TimesNode timesNode)
        {
            Print("TimesNode");
            Indent++;

        }

        public override void Visit(FunctionLoopNode loopFnNode)
        {
            Print("FunctionLoopNode");
            Indent++;
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            Indent--;
        }

        public override void Visit(AssignmentNode assignmentNode)
        {
            Print("AssignmentNode");
            Indent++;
            Indent--;
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public override void Visit(StatementNode statementNode)
        {
            Print("StatementNode");
            Indent++;
            Indent--;

        }

        public override void Visit(WithNode withNode)
        {
            Print("WithNode");
            Indent++;
            Indent--;

        }

        public override void Visit(WaitNode waitNode)
        {
            Print("WaitNode");
            Indent++;
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            Indent--;
        }

        public override void Visit(VarNode varNode)
        {
            Print("VarNode");
            Indent++;
            Indent--;

        }

        public override void Visit(ValNode valNode)
        {
            Print("ValNode");
            Indent++;
            Indent--;
        }

        public override void Visit(TimeSecondNode timeSecondNode)
        {
            Print("TimeSecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeMinuteNode timeMinuteNode)
        {
            Print("TimeMinuteNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            Print("TimeMillisecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(TimeHourNode timeHourNode)
        {
            Print("TimeHourNode");
            Indent++;
            Indent--;


        }

        public override void Visit(RightParenthesisNode rightParenthesisNode)
        {
            Print("TimeMillisecondNode");
            Indent++;
            Indent--;

        }

        public override void Visit(NumericNode numericNode)
        {
            Print("NumericNode");
            Indent++;
            Indent--;


        }

        public override void Visit(NewlineNode newlineNode)
        {
            Print("NewlineNode");
            Indent++;
            Indent--;
        }

        public override void Visit(LeftParenthesisNode leftParenthesisNode)
        {
            Print("LeftParenthesisNode");
            Indent++;
            Indent--;
        }

        public override void Visit(InNode inNode)
        {
            Print("InNode");
            Indent++;
            Indent--;

        }
        public override void Visit(EqualNode equalNode)
        {
            Print("EqualNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EqualsNode equalsNode)
        {
            Print("EqualsNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EOFNode eOFNode)
        {
            Print("EOFNode");
            Indent++;
            Indent--;

        }

        public override void Visit(EpsilonNode epsilonNode)
        {
            Print("EpsilonNode");
            Indent++;
            Indent--;

        }

        public override void Visit(DoNode doNode)
        {
            Print("DoNode");
            Indent++;
            Indent--;

        }

        public override void Visit(ProgramNode programNode)
        {
            _symbolTabelGlobal.Type = TokenType.PROG;
            Print("Program");
            Indent++;
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
                programNode.FunctionDefinitons.ForEach(node => node.Parent = programNode);
                programNode.FunctionDefinitons.ForEach(node => _symbolTabelGlobal.AddNode(node));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
                programNode.Statements.ForEach(node => node.Parent = programNode);
                programNode.Statements.ForEach(node => _symbolTabelGlobal.AddNode(node));
            }
            programNode.LoopFunction.Accept(this);
            
            Indent--;
        }

        public override void Visit(CallNode callNode)
        {
            Print("CallNode");
            Indent++;
            callNode.Id.Accept(this);
            callNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
        }

        public override void Visit(EndNode endNode)
        {
            Print("EndNode");
            Indent++;
            Indent--;
        }
        public override void Visit(AndNode andNode)
        {
            Print("AndNode");
            Indent++;
            Indent--;
        }
        public override void Visit(PinNode pinNode)
        {
            Print("PinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(APinNode apinNode)
        {
            Print("APinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(DPinNode dpinNode)
        {
            Print("DPinNode");
            Indent++;
            Indent--;
        }
        public override void Visit(OperatorNode operatorNode)
        {
            Print("OperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(BoolOperatorNode boolOperatorNode)
        {
            Print("BoolOperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(CallParametersNode callParametersNode)
        {
            Print("CallParametersNode");
            Indent++;
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
        }
        public override void Visit(DivideNode divideNode)
        {
            Print("DivideNode");
            Indent++;
            Indent--;
        }
        public override void Visit(ExpressionNode expressionNode)
        {
            Print("ExpressionNode");
            Indent++;
            expressionNode.Term.Accept(this);
            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
            Indent--;
        }
        public override void Visit(ForNode forNode)
        {
            Print("ForNode");
            Indent++;
            forNode.CountingVariable.Accept(this);
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
                forNode.Statements.ForEach(node => node.Parent = forNode);
            }
            //forNode.Accept(this);
            Indent--;
        }
        public override void Visit(FuncNode funcNode)
        {
         NodeSymbolTab symbolTabel = new NodeSymbolTab();
    //symbolTabel.AddNode(funcNode.Name.Id, funcNode);
    Print("FuncNode");
            Indent++;

            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Accept(this));
                funcNode.Statements.ForEach(node => node.Parent = funcNode);
                funcNode.Statements.ForEach(node => symbolTabel.AddNode(node));
            }

            symbolTabel.Parent = _symbolTabelGlobal;
            symbolTabel.Type = TokenType.FUNC;
            symbolTabel.Line = funcNode.Line;
            symbolTabel.Offset = funcNode.Offset;
            _symbolTabelGlobal.ChildrenList.Add(symbolTabel);
            
            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            Indent--;
           
        }
        public override void Visit(GreaterNode greaterNode)
        {
            Print("GreaterNode");
            Indent++;
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
            Indent--;
        }
        public override void Visit(IfStatementNode ifStatementNode)
        {
            NodeSymbolTab symbolTab = new NodeSymbolTab();
            symbolTab.Type = TokenType.IF;
            symbolTab.Line = ifStatementNode.Line;
            symbolTab.Offset = ifStatementNode.Offset;
            Print("IfstatementNode");
            Indent++;
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);
                ifStatementNode.Statements.ForEach(node => symbolTab.AddNode(node));
            }
            Indent--;
        }
        public override void Visit(LessNode lessNode)
        {
            Print("LessNode");
            Indent++;
            lessNode.OrEqualNode.Accept(this);
            Indent--;
        }
        public override void Visit(LoopNode loopNode)
        {
            Print("LoopNode");
            Indent++;
            Indent--;
        }
        public override void Visit(MathOperatorNode mathOperatorNode)
        {
            Print("MathOperatorNode");
            Indent++;
            Indent--;
        }
        public override void Visit(PlusNode plusNode)
        {
            Print("PlusNode");
            Indent++;
            Indent--;
        }
        public override void Visit(MinusNode minusNode)
        {
            Print("MinusNode");
            Indent++;
            Indent--;
        }
        public override void Visit(ModuloNode moduloNode)
        {
            Print("ModuloNode");
            Indent++;
            Indent--;
        }
        public override void Visit(OrNode orNode)
        {
            Print("OrNode");
            Indent++;
            Indent--;
        }
        public override void Visit(StringNode stringNode)
        {
            Print("StringNode");
            Indent++;
            Indent--;
        }
        public override void Visit(WhileNode whileNode)
        {
            NodeSymbolTab symbolTab = new NodeSymbolTab();
            symbolTab.Type = TokenType.WHILE;
            symbolTab.Line = whileNode.Line;
            symbolTab.Offset = whileNode.Offset;
            
            Print("WhileNode");
            Indent++;

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
                whileNode.Statements.ForEach(node => symbolTab.AddNode(node));
            }
            
            Indent--;
           
        }
        public override void Visit(ElseStatementNode elseStatement)
        {
            NodeSymbolTab symbolTab = new NodeSymbolTab();
            symbolTab.Type = TokenType.ELSE;
            symbolTab.Line = elseStatement.Line;
            symbolTab.Offset = elseStatement.Offset;
            Print("ElseStatementNode");
            Indent++;
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                elseStatement.Statements.ForEach(node => symbolTab.AddNode(node));
            }
            //elseStatement.Accept(this);
            Indent--;
            //symbolTabel.AddNode(elseStatement.ToString(), elseStatement);
        }
        public override void Visit(ElseifStatementNode elseifStatementNode)
        {
            NodeSymbolTab symbolTab = new NodeSymbolTab();
            symbolTab.Type = TokenType.ELSEIFSTMNT;
            symbolTab.Line = elseifStatementNode.Line;
            symbolTab.Offset = elseifStatementNode.Offset;
            Print("ElseifStatementNode");
            Indent++;
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);
                elseifStatementNode.Statements.ForEach(node => symbolTab.AddNode(node));
            }
            //elseifStatementNode.Accept(this);
            Indent--;
            //symbolTabel.AddNode(elseifStatementNode.ToString(), elseifStatementNode);
        }
        public override void Visit(RangeNode rangeNode)
        {
            Print("RangeNode");
            Indent++;
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
            Indent--;
        }

        public override void Visit(ReturnNode returnNode)
        {
            Print("ReturnNode");
            Indent++;
            returnNode.ReturnValue.Accept(this);
            Indent--;
        }

    }
}
