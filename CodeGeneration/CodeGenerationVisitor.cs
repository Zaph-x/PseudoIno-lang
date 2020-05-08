using System;
using System.IO;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using Lexer.Objects;


namespace CodeGeneration
{
    public class CodeGenerationVisitor : Visitor
    {
        public void PrintStringToFile(string content)
        {
            using (StreamWriter writer = File.AppendText("Codegen_output.cpp"))
            {
                writer.Write(content);
            }
        }
        public override object Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);
            return null;
        }

        public override object Visit(TimeNode timeNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(DeclParametersNode declParametersNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimesNode timesNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            return null;
        }

        public override object Visit(FollowTermNode followTermNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(StatementNode statementNode)
        {
            return null;
        }

        public override object Visit(WithNode withNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(WaitNode waitNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(VarNode varNode)
        {
            PrintStringToFile(varNode.Id);
            return null;
        }

        public override object Visit(ValNode valNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimeSecondNode timeSecondNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimeHourNode timeHourNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(RightParenthesisNode rightParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(NumericNode numericNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(NewlineNode newlineNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(LeftParenthesisNode leftParenthesisNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(InNode inNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(EqualNode equalNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(EqualsNode equalsNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(EOFNode eOFNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(EpsilonNode epsilonNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(DoNode doNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ProgramNode programNode)
        {
           
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Parent = programNode);
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Parent = programNode);
                programNode.Statements.ForEach(node => node.Accept(this));
            }
            programNode.LoopFunction.Accept(this);
        
           
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            PrintStringToFile(callNode.Id.Id);
            PrintStringToFile("(");
            for (int i = 0; i < callNode.Parameters.Count - 1; i++)
            {
                if (i > 0)
                {
                    PrintStringToFile(", ");
                }
                PrintStringToFile(callNode.Parameters[i].Value);
            }
            PrintStringToFile(");\n");
            return null;
        }

        public override object Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(AndNode andNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(PinNode pinNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(APinNode apinNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(DPinNode dpinNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(OperatorNode operatorNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(BoolOperatorNode boolOperatorNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(CallParametersNode callParametersNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(DivideNode divideNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ExpressionNode expressionNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ForNode forNode)
        {
            if (forNode.From.IValue < forNode.To.IValue)
            {
                PrintStringToFile("for(int " + forNode.CountingVariable.Id + " = " +  
                                  forNode.From.IValue + ";" + " " + forNode.CountingVariable.Id + " < " + 
                                  forNode.To.IValue + "; " + 
                                  forNode.CountingVariable.Id + "++;){\n");
            }
            else
            {
                PrintStringToFile("for(int " + forNode.CountingVariable.Id + " = " +  
                                  forNode.From.IValue + ";" + " "+ forNode.CountingVariable.Id + " < " + 
                                  forNode.To.IValue + "; " + 
                                  forNode.CountingVariable.Id + "--;){\n");
            }
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Parent = forNode);
                forNode.Statements.ForEach(node => node.Accept(this));
            }
            PrintStringToFile("}");
            return null;
        }

        public override object Visit(FuncNode funcNode)
        {
            //TODO function type Return er en expression bruge noget f.eks. funcNode.Statements.Any(statment => statment.Type == Lexer.Objects.TokenType.RETURN);
            PrintStringToFile(" ");
            funcNode.Name.Accept(this);
            PrintStringToFile("(");
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            PrintStringToFile(")\n{");
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Parent = funcNode);
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
            PrintStringToFile("\n}\n");
            return null;
        }

        public override object Visit(GreaterNode greaterNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(IfStatementNode ifStatementNode)
        {
            PrintStringToFile("if(");
            ifStatementNode.Expression?.Accept(this);
            PrintStringToFile("){\n");
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);

                ifStatementNode.Statements.ForEach(node => node.Accept(this));

            }
            PrintStringToFile("\n}\n");
            return null;
        }

        public override object Visit(LessNode lessNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(LessOrEqualNode lessNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(LoopNode loopNode)
        {
            // Node not used
            /*if (loopNode.Type == TokenType.FOR)
            {
                
            }
            else if (loopNode.Type == TokenType.WHILE)
            {
                
            }*/

            return null;
        }

        public override object Visit(MathOperatorNode mathOperatorNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(PlusNode plusNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(MinusNode minusNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ModuloNode moduloNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(OrNode orNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(StringNode stringNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(WhileNode whileNode)
        {
            //whileNode.Expression.Accept(this); yeet
            
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
            }

            return null;
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            PrintStringToFile("else{\n");
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }

            PrintStringToFile("\n}\n");
            return null;
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            PrintStringToFile("else if (");
            //elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            PrintStringToFile("){\n");
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            PrintStringToFile("\n}\n");

            return null;
        }

        public override object Visit(RangeNode rangeNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ReturnNode returnNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ExpressionTerm expressionTermNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(NoParenExpression noParenExpression)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            throw new NotImplementedException();
        }
    }
}
