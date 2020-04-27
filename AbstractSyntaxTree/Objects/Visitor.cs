using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public abstract class Visitor
    {
        public void Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);

        }

        internal void Visit(TimeNode timeNode)
        {

        }

        internal void Visit(DeclParametersNode declParametersNode)
        {
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }

        }

        public void Visit(TimesNode timesNode)
        {

        }

        public void Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
        }

        internal void Visit(AssignmentNode assignmentNode)
        {
            //TODO der er interface med IAssginable Var { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
        }

        public void Visit(StatementNode statementNode)
        {
            
        }

        public void Visit(WithNode withNode)
        {
        }

        public void Visit(WaitNode waitNode)
        {

            waitNode.TimeAmount?.Accept(this);
            waitNode.TimeModifier?.Accept(this);
        }

        public void Visit(VarNode varNode)
        {

        }

        public void Visit(ValNode valNode)
        {
          
        }

        public void Visit(TimeSecondNode timeSecondNode)
        {
        
        }

        public void Visit(TimeMinuteNode timeMinuteNode)
        {
           
        }

        public void Visit(TimeMillisecondNode timeMillisecondNode)
        {
            
        }

        public void Visit(TimeHourNode timeHourNode)
        {
          

        }

        public void Visit(RightParenthesisNode rightParenthesisNode)
        {
            
        }

        public void Visit(NumericNode numericNode)
        {


        }

        public void Visit(NewlineNode newlineNode)
        {
           
        }

        public void Visit(LeftParenthesisNode leftParenthesisNode)
        {
        }

        public void Visit(InNode inNode)
        {
        
        }
        public void Visit(EqualNode equalNode)
        {
        
        }

        public void Visit(EqualsNode equalsNode)
        {
            
        }

        public void Visit(EOFNode eOFNode)
        {
            
        }

        public void Visit(EpsilonNode epsilonNode)
        {
            
        }

        public void Visit(DoNode doNode)
        {
            
        }

        public void Visit(ProgramNode programNode)
        {
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
            }
            programNode.LoopFunction.Accept(this);
        }

        public void Visit(CallNode callNode)
        {
            callNode.Id.Accept(this);
            callNode.Parameters.ForEach(node => node.Accept(this));
        }

        public void Visit(EndNode endNode)
        {
            
        }
        public void Visit(AndNode andNode)
        {
         
        }
        public void Visit(PinNode pinNode)
        {
            
        }
        public void Visit(APinNode apinNode)
        {

        }
        public void Visit(DPinNode dpinNode)
        {

        }
        public void Visit(OperatorNode operatorNode)
        {
        
        }
        public void Visit(BoolOperatorNode boolOperatorNode)
        {
            
        }
        public void Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
        }
        public void Visit(DivideNode divideNode)
        {

        }
        public void Visit(ExpressionNode expressionNode)
        {
            expressionNode.Term.Accept(this);
            expressionNode.Operator.Accept(this);
            expressionNode.Expression.Accept(this);
            
        }
        public void Visit(ForNode forNode)
        {
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
            }
            //forNode.Accept(this);
        }
        public void Visit(FuncNode funcNode)
        {
            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));

        }
        public void Visit(GreaterNode greaterNode)
        {
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
        }
        public void Visit(IfStatementNode ifStatementNode)
        {
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
            }

        }
        public void Visit(LessNode lessNode)
        {
            lessNode.OrEqualNode.Accept(this);
            
        }
        public void Visit(LoopNode loopNode)
        {
            
        }
        public void Visit(MathOperatorNode mathOperatorNode)
        {
            
        }
        public void Visit(PlusNode plusNode)
        {

        }
        public void Visit(MinusNode minusNode)
        {

        }
        public void Visit(ModuloNode moduloNode)
        {

        }
        public void Visit(OrNode orNode)
        {
        }
        public void Visit(StringNode stringNode)
        {
            
        }
        public void Visit(WhileNode whileNode)
        {
            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
            }
        }
        public void Visit(ElseStatementNode elseStatement)
        {
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }
            //elseStatement.Accept(this);
        }
        public void Visit(ElseifStatementNode elseifStatementNode)
        {
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            //elseifStatementNode.Accept(this);
        }
        public void Visit(RangeNode rangeNode)
        {
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
        }
    }
}