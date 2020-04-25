//using AbstractSyntaxTree.Objects;
//using AbstractSyntaxTree.Objects.Nodes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace AbstractSyntaxTree.Tests
//{
//   public class TestPrittyPrinter:Visitor
//    {
//         void prettyprint (AstNode input)
//        {
//            Console.WriteLine("parent " + (input.Parent == null ? "null" : input.Parent.ToString()));
//            Console.WriteLine(" type " + (input.Type == null ? "null" : input.Type.ToString()));
//            Console.WriteLine(" Value " + (input.Value == null ? "null" : input.Value.ToString()));
//                //+ " type " + input.Type == null ? "null": input.Type.ToString() + " Value " + input.Value==null?"null": input.Value.ToString()) ;
//        }
//        public override void Visit(BeginNode beginNode)
//        {
           
//               beginNode.LoopNode.Accept(this);
//            prettyprint(beginNode);
//        }

//        public override void Visit(TimeNode timeNode)
//        {
//            prettyprint(timeNode);
//            //timeNode.Accept(this);
//        }
//        public override void Visit(TimesNode timesNode)
//        {
//            prettyprint(timesNode);
//        }

//        public override void Visit(FunctionLoopNode loopFnNode)
//        {
            
//            if (loopFnNode.Statements.Any())
//            {
//                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
//            }
//            prettyprint(loopFnNode);
//        }

//        public override void Visit(AssignmentNode assignmentNode)
//        {
            
//            assignmentNode.LeftHand.Accept(this);
//            assignmentNode.RightHand.Accept(this);
//            if (assignmentNode.ExpressionHand != null)
//                assignmentNode.ExpressionHand.Accept(this);
//            prettyprint(assignmentNode);
//        }

//        public override void Visit(FunctionDefinitonNode functionDefinitonNode)
//        {
//            functionDefinitonNode.LeftHand?.Accept(this);
//            functionDefinitonNode.RightHand?.Accept(this);
//            if (functionDefinitonNode.Statements.Any())
//            {
//                functionDefinitonNode.Statements.ForEach(node => node.Accept(this));
//            }
//            prettyprint(functionDefinitonNode);

//            //functionDefinitonNode.Accept(this);
//        }

//        public override void Visit(StatementNode statementNode)
//        {
//            prettyprint(statementNode);
//            //statementNode.Accept(this);
//        }

//        public override void Visit(WithNode withNode)
//        {
//            prettyprint(withNode);
//            //withNode.Accept(this);
//        }

//        public override void Visit(WaitNode waitNode)
//        {

//            waitNode.TimeAmount.Accept(this);
//            waitNode.TimeModifier.Accept(this);
//            prettyprint(waitNode);
//        }

//        public override void Visit(VarNode varNode)
//        {
//            prettyprint(varNode);
//        }

//        public override void Visit(ValNode valNode)
//        {
//            prettyprint(valNode);
//            //valNode.Accept(this);
//        }

//        public override void Visit(TimeSecondNode timeSecondNode)
//        {
//            prettyprint(timeSecondNode);
//            //timeSecondNode.Accept(this);
//        }

//        public override void Visit(TimeMinuteNode timeMinuteNode)
//        {
//            prettyprint(timeMinuteNode);
//            //timeMinuteNode.Accept(this);
//        }

//        public override void Visit(TimeMillisecondNode timeMillisecondNode)
//        {
//            prettyprint(timeMillisecondNode);
//            //timeMillisecondNode.Accept(this);
//        }

//        public override void Visit(TimeHourNode timeHourNode)
//        {
//            prettyprint(timeHourNode);
//            // timeHourNode.Accept(this);

//        }

//        public override void Visit(RightParenthesisNode rightParenthesisNode)
//        {
//            prettyprint(rightParenthesisNode);
//            //rightParenthesisNode.Accept(this);
//        }

//        public override void Visit(NumericNode numericNode)
//        {
//            prettyprint(numericNode);
//            //numericNode.Accept(this);

//        }

//        public override void Visit(NewlineNode newlineNode)
//        {
//            prettyprint(newlineNode);
//            //overridelineNode.Accept(this);
//        }

//        public override void Visit(LeftParenthesisNode leftParenthesisNode)
//        {
//            prettyprint(leftParenthesisNode);
//            //leftParenthesisNode.Accept(this);
//        }

//        public override void Visit(InNode inNode)
//        {
//            prettyprint(inNode);
//            //inNode.Accept(this);
//        }
//        public override void Visit(EqualNode equalNode)
//        {
//            prettyprint(equalNode);
//            //equalNode.Accept(this);
//        }

//        public override void Visit(EqualsNode equalsNode)
//        {
//            prettyprint(equalsNode);
//            //equalsNode.Accept(this);
//        }

//        public override void Visit(EOFNode eOFNode)
//        {
//            prettyprint(eOFNode);
//            //eOFNode.Accept(this);
//        }

//        public override void Visit(EpsilonNode epsilonNode)
//        {
//            prettyprint(epsilonNode);
//            // epsilonNode.Accept(this);
//        }

//        public override void Visit(DoNode doNode)
//        {
//            prettyprint(doNode);
//            //doNode.Accept(this);
//        }

//        public override void Visit(ProgramNode programNode)
//        {
//            if (programNode.FunctionDefinitons.Any())
//            {
//                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
//            }
//            if (programNode.Statements.Any())
//            {
//                programNode.Statements.ForEach(node => node.Accept(this));
//            }

//            programNode.LoopFunction.Accept(this);
//            prettyprint(programNode );
//        }

//        public override void Visit(CallNode callNode)
//        {
//            callNode.VarNode.Accept(this);
//            if (callNode.RightHand != null)
//            {
//                callNode.RightHand.Accept(this);
//            }
//            prettyprint(callNode);
//            //callNode.Accept(this);
//        }

//        public override void Visit(EndNode endNode)
//        {
//            prettyprint(endNode);
//            //endNode.Accept(this);
//        }
//        public override void Visit(AndNode andNode)
//        {
//            prettyprint(andNode);   //andNode.Accept(this);
//        }
//        public override void Visit(PinNode pinNode)
//        {
//            prettyprint(pinNode);
//            //pinNode.Accept(this);
//        }
//        public override void Visit(APinNode apinNode)
//        {
//            prettyprint(apinNode);
//        }
//        public override void Visit(DPinNode dpinNode)
//        {
//            prettyprint(dpinNode);
//        }
//        public override void Visit(OperatorNode operatorNode)
//        {
//            prettyprint(operatorNode);
//            //operatorNode.Accept(this);
//        }
//        public override void Visit(BoolOperatorNode boolOperatorNode)
//        {
//            //boolOperatorNode.Accept(this);
//            prettyprint(boolOperatorNode);
//        }
//        public override void Visit(CallParametersNode callParametersNode)
//        {
//            callParametersNode.ValNode.Accept(this);
//            if (callParametersNode.RightHand != null)
//            {
//                callParametersNode.RightHand.Accept(this);
//            }
//            prettyprint(callParametersNode);
//            //callParametersNode.Accept(this);
//        }
//        public override void Visit(DivideNode divideNode)
//        {
//            prettyprint(divideNode);
//        }
//        public override void Visit(ExpressionNode expressionNode)
//        {
//            expressionNode.Value.Accept(this);
//            expressionNode.Operator.Accept(this);
//            if (expressionNode.Expression != null)
//            {
//                expressionNode.Expression.Accept(this);
//            }
//            prettyprint(expressionNode);

//        }
//        public override void Visit(ForNode forNode)
//        {
//            forNode.ValNode.Accept(this);
//            forNode.RangeNode.Accept(this);
//            if (forNode.Statements.Any())
//            {
//                forNode.Statements.ForEach(node => node.Accept(this));
//            }
//            //forNode.Accept(this);
//            prettyprint(forNode);
//        }
//        public override void Visit(FuncNode funcNode)
//        {
//            //funcNode.Accept(this);
//            if (funcNode.Statements.Any())
//            {
//                funcNode.Statements.ForEach(node => node.Accept(this));
//            }
//            funcNode.LeftHand.Accept(this);
//            funcNode.RightHand.Accept(this);
//            prettyprint(funcNode);
//        }
//        public override void Visit(GreaterNode greaterNode)
//        {
//            greaterNode.OrEqualNode.Accept(this);
//            //greaterNode.Accept(this);
//            prettyprint(greaterNode);
//        }
//        public override void Visit(IfStatementNode ifStatementNode)
//        {
//            ifStatementNode.Val.Accept(this);
//            ifStatementNode.Expression.Accept(this);
//            if (ifStatementNode.Statements.Any())
//            {
//                ifStatementNode.Statements.ForEach(node => node.Accept(this));
//            }
//            if (ifStatementNode.ElseifStatementNode.Any())
//            {
//                ifStatementNode.ElseifStatementNode.ForEach(node => node.Accept(this));
//            }
//            ifStatementNode.ElseStatementNode.Accept(this);
//            //ifStatementNode.Accept(this);
//            prettyprint(ifStatementNode);
//        }
//        public override void Visit(LessNode lessNode)
//        {
//            lessNode.OrEqualNode?.Accept(this);
//            prettyprint(lessNode);
//        }
//        public override void Visit(LoopNode loopNode)
//        {
//            //loopNode.Accept(this);
//            prettyprint(loopNode);
//        }
//        public override void Visit(MathOperatorNode mathOperatorNode)
//        {
//            prettyprint(mathOperatorNode);
//            //mathOperatorNode.Accept(this);
//        }
//        public override void Visit(PlusNode plusNode)
//        {
//            prettyprint(plusNode);
//        }
//        public override void Visit(MinusNode minusNode)
//        {
//            prettyprint(minusNode);
//        }
//        public override void Visit(ModuloNode moduloNode)
//        {
//            prettyprint(moduloNode);
//        }
//        public override void Visit(OrNode orNode)
//        {
//            prettyprint(orNode);
//            //orNode.Accept(this);
//        }
//        public override void Visit(StringNode stringNode)
//        {
//            prettyprint(stringNode);
//            //stringNode.Accept(this);
//        }
//        public override void Visit(WhileNode whileNode)
//        {
//            whileNode.ValNode.Accept(this);
//            whileNode.ExpressionNode.Accept(this);
//            if (whileNode.Statements.Any())
//            {
//                whileNode.Statements.ForEach(node => node.Accept(this));
//            }
//            prettyprint(whileNode);
//        }
//        public override void Visit(ElseStatementNode elseStatement)
//        {
//            if (elseStatement.Statements.Any())
//            {
//                elseStatement.Statements.ForEach(node => node.Accept(this));
//            }
//            prettyprint(elseStatement);
//            //elseStatement.Accept(this);
//        }
//        public override void Visit(ElseifStatementNode elseifStatementNode)
//        {
//            elseifStatementNode.Val.Accept(this);
//            elseifStatementNode.Expression.Accept(this);
//            if (elseifStatementNode.Statements.Any())
//            {
//                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
//            }
//            prettyprint(elseifStatementNode);
//            // elseifStatementNode.Accept(this);
//        }
//        public override void Visit(RangeNode rangeNode)
//        {
//            rangeNode.LeftHand.Accept(this);
//            rangeNode.RightHand.Accept(this);
//            // rangeNode.Accept(this);
//            prettyprint(rangeNode);
//        }
//    }
//}
