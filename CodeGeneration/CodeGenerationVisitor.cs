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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(StatementNode statementNode)
        {
            throw new NotImplementedException();
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
                programNode.Statements.ForEach(node => node.Accept(this));
                programNode.Statements.ForEach(node => node.Parent = programNode);
                //programNode.Statements.ForEach(node => _symbolTableBuilder.AddNode(node));
            }
            programNode.LoopFunction.Accept(this);
        
           
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override object Visit(FuncNode funcNode)
        {
            //TODO function type Return er en expression bruge noget f.eks. funcNode.Statements.Any(statment => statment.Type == Lexer.Objects.TokenType.RETURN);
            PrintStringToFile(" ");
            funcNode.Name.Accept(this);
            PrintStringToFile("(");
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            PrintStringToFile(")\n");
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Parent = funcNode);
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
          
            return null;
        }

        public override object Visit(GreaterNode greaterNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(IfStatementNode ifStatementNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(LessNode lessNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(LoopNode loopNode)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(RangeNode rangeNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ReturnNode returnNode)
        {
            throw new NotImplementedException();
        }
    }
}
