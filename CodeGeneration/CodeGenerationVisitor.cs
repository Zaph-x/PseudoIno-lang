using System;
using System.IO;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using Contextual_analysis;
using Contextual_analysis.Exceptions;
using Lexer.Objects;
using SymbolTable;


namespace CodeGeneration
{
    public class CodeGenerationVisitor : Visitor
    {
        private string Header { get; set; }
        private string Global { get; set; }
        private string Prototypes { get; set; }
        private string Setup { get; set; }
        private string Funcs { get; set; }
        private string Loop { get; set; }

        private SymbolTableObject GlobalScope = SymbolTableBuilder.GlobalSymbolTable;
        private SymbolTableObject CurrentScope;
        public void PrintStringToFile(string content)
        {
            using (StreamWriter writer = File.AppendText("Codegen_output.cpp"))
            {
                writer.Write(content);
            }
        }

        public void PrintStringToFile()
        {
            string content = "";
            content += Header + Global + Prototypes + Setup + Funcs + Loop;
            using (StreamWriter writer = File.AppendText("Codegen_output.cpp"))
            {
                writer.Write(content);
            }
        }

        public void PrintToHeader(string input)
        {
            Header += input;
        }

        public void PrintToGlobal(string input)
        {
            Global += input;
        }

        public void PrintToPrototypes(string input)
        {
            Prototypes += input;
        }

        public void PrintToSetup(string input)
        {
            Setup += input;
        }

        public void PrintToFuncs(string input)
        {
            Funcs += input;
        }

        public void PrintToLoop(string input)
        {
            Loop += input;
        }
        public override object Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);
            return null;
        }

        public override object Visit(TimeNode timeNode)
        {
            return null;
        }

        public override object Visit(DeclParametersNode declParametersNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(TimesNode timesNode)
        {
            //PrintStringToFile(" * ");
            return " * ";
        }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            string funcloop = "";
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => funcloop += stmnt.Accept(this));
            }
            return funcloop;
        }

        public override object Visit(FollowTermNode followTermNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            string assign = (string)assignmentNode.LeftHand.Accept(this);
            assign += " = ";
            // assignmentNode.Operator.Accept(this);
            assign += (string)assignmentNode.RightHand.Accept(this);
            assign += ";\n";
            return assign;
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
            string delay = "delay(";
            waitNode.TimeAmount.Accept(this);
            //            waitNode.TimeModifier.Accept(this);
            switch (waitNode.TimeModifier.Type)
            {
                case TokenType.TIME_HR:
                    delay += "*3600000";
                    break;
                case TokenType.TIME_MIN:
                    delay += "*60000";
                    break;
                case TokenType.TIME_SEC:
                    delay += "*1000";
                    break;
                case TokenType.TIME_MS:
                    break;
                default:
                    throw new InvalidTypeException($"Invalid timemodifier exception at{waitNode.TimeModifier.Line}:{waitNode.TimeModifier.Offset}. Time parameter not specified.");
            }
            delay += ")\n";
            return delay;
        }

        public override object Visit(VarNode varNode)
        {
            //PrintStringToFile(varNode.Id);
            return varNode.Id;
        }

        public override object Visit(ValNode valNode)
        {
            //PrintStringToFile(valNode.Value);
            return valNode.Value;
        }

        public override object Visit(TimeSecondNode timeSecondNode)
        {
            return null;
        }

        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            return null;
        }

        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            return null;
        }

        public override object Visit(TimeHourNode timeHourNode)
        {
            return null;
        }

        public override object Visit(RightParenthesisNode rightParenthesisNode)
        {
            //PrintStringToFile(")");
            return ")";
        }

        public override object Visit(NumericNode numericNode)
        {
            string numeric = "";
            if (numericNode.FValue % 1 != 0)
            {
                numeric += numericNode.FValue.ToString();

            }
            else
            {
                numeric += numericNode.IValue.ToString();

            }
            return numeric;
        }

        public override object Visit(NewlineNode newlineNode)
        {
            //PrintStringToFile("\n");
            return null;
        }

        public override object Visit(LeftParenthesisNode leftParenthesisNode)
        {
            //PrintStringToFile("(");
            return "(";
        }

        public override object Visit(InNode inNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(EqualNode equalNode)
        {
            //PrintStringToFile(" = ");
            return " = ";
        }

        public override object Visit(EqualsNode equalsNode)
        {
            //PrintStringToFile(" == ");
            return " == ";
        }

        public override object Visit(EOFNode eOFNode)
        {
            return null;
        }

        public override object Visit(EpsilonNode epsilonNode)
        {
            return null;
        }

        public override object Visit(DoNode doNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ProgramNode programNode)
        {
            PrintToHeader("#include<Arduino.h>\n");
            PrintToHeader("//code generated by the PseudoIno\n");

            if (programNode.FunctionDefinitons.Any())
            {
                foreach (var functionDefiniton in programNode.FunctionDefinitons)
                {
                    //PrintToPrototypes(functionDefiniton.Name.Id + "(");
                }

                string funcs = "";
                programNode.FunctionDefinitons.ForEach(node => node.Parent = programNode);
                programNode.FunctionDefinitons.ForEach(node => funcs += node.Accept(this));
                PrintToFuncs(funcs);
            }
            string setupString = "void setup()\n{\n";
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Parent = programNode);
                programNode.Statements.ForEach(node => setupString += node.Accept(this));
            }
            setupString += "}\n";
            PrintToSetup(setupString);
            //PrintStringToFile("void loop(){\n");
            string loopString = (string)programNode.LoopFunction.Accept(this);
            PrintToLoop(loopString);
            //PrintStringToFile("}\n");
            PrintStringToFile();
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            string callString = callNode.Id.Id + "(";
            for (int i = 0; i < callNode.Parameters.Count - 1; i++)
            {
                if (i > 0)
                {
                    callString += ", ";
                }
                callString += callNode.Parameters[i].Value;
            }
            callString += ");\n";
            return callString;
        }

        public override object Visit(EndNode endNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(AndNode andNode)
        {
            //PrintStringToFile(" && ");
            return " && ";
        }

        public override object Visit(PinNode pinNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(APinNode apinNode)
        {
            //TODO Write to a thing thats does the thing - ask sejbas :)
            return null;
        }

        public override object Visit(DPinNode dpinNode)
        {
            //TODO Write to a thing thats does the thing - ask sejbas :)
            return null;
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
            //TODO Make this
            throw new NotImplementedException();
        }

        public override object Visit(DivideNode divideNode)
        {
            //PrintStringToFile(" / ");
            return " / ";
        }

        public override object Visit(ExpressionNode expressionNode)
        {
            expressionNode.LeftHand.Accept(this);
            expressionNode.Operator.Accept(this);
            expressionNode.RightHand.Accept(this);
            return null;
        }

        public override object Visit(ForNode forNode)
        {
            string forLoop = "";
            if (forNode.From.IValue < forNode.To.IValue)
            {
                forLoop += "for(int " + forNode.CountingVariable.Id + " = " +
                                  forNode.From.IValue + ";" + " " + forNode.CountingVariable.Id + " < " +
                                  forNode.To.IValue + "; " +
                                  forNode.CountingVariable.Id + "++;){\n";
            }
            else
            {
                forLoop += "for(int " + forNode.CountingVariable.Id + " = " +
                                  forNode.From.IValue + ";" + " " + forNode.CountingVariable.Id + " < " +
                                  forNode.To.IValue + "; " +
                                  forNode.CountingVariable.Id + "--;){\n";
            }
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Parent = forNode);
                forNode.Statements.ForEach(node => forLoop += ((string)node.Accept(this)));
            }
            forLoop += "}\n";
            return forLoop;
        }

        public override object Visit(FuncNode funcNode)
        {
            string func = "";
            switch ((funcNode.SymbolType?.Type.ToString() ?? "void"))
            {
                case "void":
                    func += "void ";
                    break;
                case "NUMERIC":
                    if (funcNode.SymbolType.IsFloat)
                    {
                        func += "float ";
                    }
                    else
                    {
                        func += "int ";
                    }
                    break;
                case "BOOL":
                    func += "bool ";
                    break;
                case "STRING":
                    func += "string ";
                    break;
                default:
                    throw new InvalidTypeException($"Invalid return type in function {funcNode.Name.Id} at {funcNode.Line}:{funcNode.Offset}");
            }

            //funcNode.Name.Accept(this);
            func += funcNode.Name.Id + "(";

            //TODO lav functions paramenter med type symboltable
            funcNode.FunctionParameters.ForEach(node => func += node.Accept(this));
            func += ")\n{\n";
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Parent = funcNode);
                funcNode.Statements.ForEach(node => func += node.Accept(this));
            }
            func += "\n}\n";
            return func;
        }

        public override object Visit(GreaterNode greaterNode)
        {
            //PrintStringToFile(" > ");
            return " > ";
        }

        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            //PrintStringToFile(" >= ");
            return " >= ";
        }

        public override object Visit(IfStatementNode ifStatementNode)
        {
            string ifNode = "";
            ifNode += "if(";
            ifNode += ifStatementNode.Expression?.Accept(this);
            ifNode += "){\n";
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);

                ifStatementNode.Statements.ForEach(node => ifNode += node.Accept(this));

            }
            ifNode += "\n}\n";
            return ifNode;
        }

        public override object Visit(LessNode lessNode)
        {
            //PrintStringToFile(" < ");
            return " < ";
        }

        public override object Visit(LessOrEqualNode lessNode)
        {
            //PrintStringToFile(" <= ");
            return " <= ";
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
            //PrintStringToFile(" + ");
            return " + ";
        }

        public override object Visit(MinusNode minusNode)
        {
            //PrintStringToFile(" - ");
            return " - ";
        }

        public override object Visit(ModuloNode moduloNode)
        {
            //PrintStringToFile(" % ");
            return " % ";
        }

        public override object Visit(OrNode orNode)
        {
            //PrintStringToFile(" || ");
            return " || ";
        }

        public override object Visit(StringNode stringNode)
        {
            //PrintStringToFile($" {stringNode.Value} ");
            return $" {stringNode.Value} ";
        }

        public override object Visit(WhileNode whileNode)
        {
            string whileString = "";
            whileString += "while(";
            whileString += whileNode.Expression.Accept(this);
            whileString += "){\n";
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
                whileNode.Statements.ForEach(node => whileString += node.Accept(this));
            }
            whileString += "}\n";
            return whileString;
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            string elseString = "else{\n";
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                elseStatement.Statements.ForEach(node => elseString += node.Accept(this));
            }

            elseString += "\n}\n";
            return elseString;
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            string elseif = "else if (";
            //elseifStatementNode.Val?.Accept(this);
            elseif += elseifStatementNode.Expression?.Accept(this);
            elseif += "){\n";
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);
                elseifStatementNode.Statements.ForEach(node => elseif += node.Accept(this));
            }
            elseif += ("\n}\n");

            return elseif;
        }

        public override object Visit(RangeNode rangeNode)
        {
            throw new NotImplementedException();
        }

        public override object Visit(ReturnNode returnNode)
        {
            string ret = "return ";
            ret += returnNode.ReturnValue.Accept(this);
            ret += ";";
            return ret;
        }

        public override object Visit(ExpressionTerm expressionTermNode)
        {
            string exp = "";
            exp += expressionTermNode.LeftHand?.Accept(this);
            //expressionTermNode.Operator.Accept(this);
            exp += expressionTermNode.RightHand?.Accept(this);
            return exp;
        }

        public override object Visit(NoParenExpression noParenExpression)
        {
            string exp = "";
            exp += noParenExpression.LeftHand.Accept(this);
            //noParenExpression.Operator.Accept(this);
            exp += noParenExpression.RightHand?.Accept(this);
            return exp;
        }

        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            string exp = "(";
            exp += parenthesisExpression.LeftHand.Accept(this);
            exp += parenthesisExpression.Operator.Accept(this);
            exp += parenthesisExpression.RightHand.Accept(this);
            exp += ")";
            return exp;
        }

        public override object Visit(BoolNode boolNode)
        {
            string boolVal = "";
            if (boolNode.Value)
                boolVal += " 1";
            else
                boolVal += " 0";

            return boolVal;
        }
    }
}
