using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;
using CodeGeneration.Exceptions;
using Contextual_analysis;
using Contextual_analysis.Exceptions;
using Lexer.Objects;
using SymbolTable;


namespace CodeGeneration
{
    public class CodeGenerationVisitor : Visitor
    {
        public static bool HasError { get; set; } = false;
        private string Header { get; set; }
        private string Declarations { get; set; }
        private string Global { get; set; }
        private string Prototypes { get; set; }
        private string Setup { get; set; }
        private string Funcs { get; set; }
        private string Loop { get; set; }

        private string FileName { get; set; }

        private List<string> PinDefs = new List<string>();

        private SymbolTableObject GlobalScope = SymbolTableBuilder.GlobalSymbolTable;
        private SymbolTableObject CurrentScope = SymbolTableBuilder.GlobalSymbolTable;

        public CodeGenerationVisitor(string fileName)
        {
            FileName = fileName;
        }
        public void PrintStringToFile(string content)
        {
            using (StreamWriter writer = File.AppendText(FileName))
            {
                writer.Write(content);
            }
        }

        public void PrintStringToFile()
        {
            string content = "";
            List<string> uniqueList = PinDefs.Distinct().ToList();
            foreach (var pinDef in uniqueList)
            {
                Setup += pinDef + "";
            }

            Setup += "}";
            content += Header + Global + Prototypes + Declarations + Setup + Funcs + Loop;
            using (StreamWriter writer = File.AppendText(FileName))
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

        public override object Visit(TimesNode timesNode)
        {
            //PrintStringToFile(" * ");
            return " * ";
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            string assign = "";
            if (assignmentNode.LeftHand.Type == TokenType.DPIN)
            {
                string pin = (string)assignmentNode.LeftHand.Accept(this);
                if (PinDefs.Any(def => def.Contains(pin) && def.Contains("INPUT")))
                    new InvalidCodeException($"Pin {pin} was defined as INPUT but is also used as OUTPUT at {assignmentNode.Line}:{assignmentNode.Offset}");
                string pinDef = "pinMode(" + assignmentNode.LeftHand.Accept(this) + ", OUTPUT);";
                PinDefs.Add(pinDef);

                assign += "digitalWrite(" + assignmentNode.LeftHand.Accept(this) + ", ";
                string boolValue = assignmentNode.RightHand.Accept(this) + ")";
                if (boolValue.StartsWith("digitalRead"))
                    assign += boolValue;
                else
                    assign += boolValue == " 1)" ? "HIGH)" : "LOW)";
            }
            else if (assignmentNode.LeftHand.Type == TokenType.APIN)
            {
                string pin = (string)assignmentNode.LeftHand.Accept(this);
                if (PinDefs.Any(def => def.Contains(pin) && def.Contains("INPUT")))
                    new InvalidCodeException($"Pin {pin} was defined as INPUT but is also used as OUTPUT at {assignmentNode.Line}:{assignmentNode.Offset}");
                string pinDef = "pinMode(" + assignmentNode.LeftHand.Accept(this) + ", OUTPUT);";
                PinDefs.Add(pinDef);

                assign += "analogWrite(" + assignmentNode.LeftHand.Accept(this) + ", ";
                string boolValue = assignmentNode.RightHand.Accept(this) + ")";
                if (assignmentNode.RightHand.SymbolType.Type == TokenType.NUMERIC)
                    assign += boolValue;
                else if (boolValue.StartsWith("analogRead"))
                    assign += boolValue;
                else
                    assign += boolValue == " 1)" ? "255)" : "0)";
            }
            else
            {
                assign += (string)assignmentNode.LeftHand.Accept(this);
                if (assignmentNode.RightHand.IsType(typeof(ArrayNode)))
                {
                    assign += (string)assignmentNode.RightHand.Accept(this);
                    assign += ";";
                    return assign;
                }
                assign += " = ";
                assign += (string)assignmentNode.RightHand.Accept(this);
            }


            assign += ";";
            return assign;
        }

        public override object Visit(WaitNode waitNode)
        {
            string delay = "delay(";
            delay += waitNode.TimeAmount.Accept(this);
            delay += waitNode.TimeModifier.Accept(this);
            delay += ");";
            return delay;
        }

        public override object Visit(VarNode varNode)
        {
            string varNodeId = "";
            if (varNode.Declaration)
            {
                if (varNode.SymbolType.Type == TokenType.NUMERIC)
                {
                    if (varNode.SymbolType.IsFloat)
                    {
                        varNodeId += "float ";
                    }
                    else
                    {
                        varNodeId += "int ";
                    }
                }
                else if (varNode.SymbolType.Type == TokenType.BOOL)
                {
                    varNodeId += "bool ";
                }
                else if (varNode.SymbolType.Type == TokenType.STRING)
                {
                    varNodeId += "String ";
                }
            }
            varNodeId += varNode.Id;
            //PrintStringToFile(varNode.Id);
            return varNodeId;
        }

        public override object Visit(ValNode valNode)
        {
            //PrintStringToFile(valNode.Value);
            return valNode.Value;
        }

        public override object Visit(TimeSecondNode timeSecondNode)
        {
            return "*1000";
        }

        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            return "*60000";
        }

        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            return "";
        }

        public override object Visit(TimeHourNode timeHourNode)
        {
            return "*3600000";
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

        public override object Visit(EqualNode equalNode)
        {
            //PrintStringToFile(" == ");
            return " == ";
        }

        public override object Visit(ProgramNode programNode)
        {
            PrintToHeader("#include<Arduino.h>\n");
            PrintToHeader("/* code generated by the PseudoIno compiler */\n");

            if (programNode.FunctionDefinitons.Any())
            {
                string funcs = "";
                programNode.FunctionDefinitons.ForEach(node =>
                {
                    if (SymbolTableObject.FunctionCalls.Any(cn => cn.Id.Id == node.Name.Id && cn.Parameters.Count == node.FunctionParameters.Count))
                        node.Parent = programNode;
                });
                programNode.FunctionDefinitons.ForEach(node =>
                {
                    if (SymbolTableObject.FunctionCalls.Any(cn => cn.Id.Id == node.Name.Id && cn.Parameters.Count == node.FunctionParameters.Count))
                        funcs += node.Accept(this);
                });
                PrintToFuncs(funcs);
            }

            Global += "void setup();";
            string setupString = "void setup(){";
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Parent = programNode);
                foreach (var node in programNode.Statements)
                {
                    if (node.Type == TokenType.ASSIGNMENT)
                    {
                        if (((AstNode)((AssignmentNode)node).LeftHand).IsType(typeof(VarNode)) && ((VarNode)((AssignmentNode)node).LeftHand).Declaration)
                        {
                            Declarations += node.Accept(this);
                            continue;
                        }
                    }
                    setupString += node.Accept(this);
                }
                //programNode.Statements.ForEach(node => setupString += node.Accept(this));
            }
            //setupString += "}";
            PrintToSetup(setupString);
            //PrintStringToFile("void loop(){");
            string loopString = (string)programNode.LoopFunction.Accept(this);
            PrintToLoop(loopString);
            //PrintStringToFile("}");
            PrintStringToFile();
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            string callString = callNode.Id.Id + "(";
            for (int i = 0; i < callNode.Parameters.Count; i++)
            {
                if (i > 0)
                {
                    callString += ", ";
                }
                callString += callNode.Parameters[i].Value;
            }
            callString += $"){((callNode.Parent?.Type == TokenType.EXPR) ? "" : ";")}";
            return callString;
        }

        public override object Visit(AndNode andNode)
        {
            //PrintStringToFile(" && ");
            return " && ";
        }

        public override object Visit(APinNode apinNode)
        {
            return apinNode.Parent == null ? apinNode.Id : $"analogueRead({apinNode.Id})";
        }

        public override object Visit(DPinNode dpinNode)
        {
            return dpinNode.Parent == null ? dpinNode.Id : $"digitalRead({dpinNode.Id})";
        }

        public override object Visit(DivideNode divideNode)
        {
            //PrintStringToFile(" / ");
            return " / ";
        }

        public override object Visit(ForNode forNode)
        {
            string forLoop = "";
            if (forNode.From.IValue < forNode.To.IValue)
            {
                forLoop += "for(int " + forNode.CountingVariable.Id + "=" +
                                  forNode.From.IValue + ";" + forNode.CountingVariable.Id + "<" +
                                  forNode.To.IValue + ";" +
                                  forNode.CountingVariable.Id + "++){";
            }
            else
            {
                forLoop += "for(int " + forNode.CountingVariable.Id + "=" +
                                  forNode.From.IValue + ";" + forNode.CountingVariable.Id + "<" +
                                  forNode.To.IValue + ";" +
                                  forNode.CountingVariable.Id + "--){";
            }
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Parent = forNode);
                forNode.Statements.ForEach(node => forLoop += ((string)node.Accept(this)));
            }
            forLoop += "}";
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
                    new InvalidCodeException($"Invalid return type in function {funcNode.Name.Id} at {funcNode.Line}:{funcNode.Offset}");
                    break;
            }

            //funcNode.Name.Accept(this);
            func += funcNode.Name.Id + "(";

            funcNode.FunctionParameters.ForEach(node => func += findFuncInputparam(node, funcNode) + node.Accept(this) + (funcNode.FunctionParameters.IndexOf(node) < funcNode.FunctionParameters.Count - 1 ? ", " : " "));

            func += ")";
            Global += func + ";";
            Global += "";
            func += "{";
            if (funcNode.Statements.Any())
            {
                // funcNode.Statements.ForEach(node => node.Parent = funcNode);
                funcNode.Statements.ForEach(node => func += node.Accept(this));
            }
            func += "}";
            return func;
        }

        private string findFuncInputparam(VarNode functionsParam, FuncNode function)
        {
            VarNode param = function.FunctionParameters.Find(x => x.Id == functionsParam.Id);

            if (param.SymbolType.IsFloat)
            {
                return "float ";
            }
            else if (param.SymbolType.Type == TokenType.BOOL)
            {
                return "bool ";
            }
            else if (!param.SymbolType.IsFloat)
            {
                return "int ";
            }
            else if (param.SymbolType.Type == TokenType.STRING)
            {
                return "String ";
            }
            else
            {
                new InvalidCodeException($"The function{function.Name} input parameter {functionsParam.Id} at {functionsParam.Line}:{functionsParam.Offset} is not used.");
                return "";
            }


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
            ifNode += "){";
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);

                ifStatementNode.Statements.ForEach(node => ifNode += node.Accept(this));

            }
            ifNode += "}";
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
            whileString += "){";
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Parent = whileNode);
                whileNode.Statements.ForEach(node => whileString += node.Accept(this));
            }
            whileString += "}";
            return whileString;
        }

        public override object Visit(ElseStatementNode elseStatement)
        {
            if (elseStatement.Statements.Any())
            {
                string elseString = "else{";
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);
                elseStatement.Statements.ForEach(node => elseString += node.Accept(this));
                elseString += "}";
                return elseString;
            }

            return "";
        }

        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            string elseif = "else if (";
            //elseifStatementNode.Val?.Accept(this);
            elseif += elseifStatementNode.Expression?.Accept(this);
            elseif += "){";
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);
                elseifStatementNode.Statements.ForEach(node => elseif += node.Accept(this));
            }
            elseif += ("}");

            return elseif;
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
            if (expressionTermNode.LeftHand.IsType(typeof(APinNode))||expressionTermNode.LeftHand.IsType(typeof(DPinNode)))
            {
                string pin = ((PinNode)expressionTermNode.LeftHand).Value;
                if (PinDefs.Any(def => def.Contains(pin) && def.Contains("OUTPUT")))
                    new InvalidCodeException($"Pin {pin} was defined as OUTPUT but is also used as INPUT at {expressionTermNode.Line}:{expressionTermNode.Offset}");
                if (expressionTermNode.LeftHand.Type == TokenType.APIN)
                {
                    PinDefs.Add($"pinMode({pin}, INPUT);");
                    exp += $"analogRead({pin})";
                } else {
                    PinDefs.Add($"pinMode({pin}, INPUT);");
                    exp += $"digitalRead({pin})";
                }
                return exp;
            }
            exp += expressionTermNode.LeftHand.Accept(this);
            return exp;
        }

        public override object Visit(BinaryExpression noParenExpression)
        {
            string exp = "";
            exp += noParenExpression.LeftHand.Accept(this);
            exp += noParenExpression.Operator?.Accept(this);
            exp += noParenExpression.RightHand?.Accept(this);
            return exp;
        }

        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            string exp = "(";
            exp += parenthesisExpression.LeftHand.Accept(this);
            exp += parenthesisExpression.Operator?.Accept(this);
            exp += parenthesisExpression.RightHand?.Accept(this);
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

        public override object Visit(ArrayNode arrayNode)
        {
            string arr = "";
            foreach (NumericNode dimension in arrayNode.Dimensions)
            {
                arr += $"[{dimension.Value}]";
            }
            return arr;
        }

        public override object Visit(ArrayAccessNode arrayAccess)
        {
            string arrAccess = "";
            arrAccess += arrayAccess.Actual.ActualId.Id;
            foreach (string access in arrayAccess.Accesses.Select(node => node.IsType(typeof(VarNode)) ? ((VarNode)node).Id : ((NumericNode)node).Value))
                arrAccess += $"[{access}]";
            return arrAccess;
        }
    }
}
