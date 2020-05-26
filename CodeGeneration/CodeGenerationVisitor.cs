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

/// <summary>
/// This name space indicated that the codegeneration is being used
/// </summary>
namespace CodeGeneration
{
    /// <summary>
    /// This is the class for the codegeneration visitor
    /// It inherits from the visitor class
    /// </summary>
    public class CodeGenerationVisitor : Visitor
    {
        /// <summary>
        /// This sets and return the boolean value for has error
        /// </summary>
        public static bool HasError { get; set; } = false;
        /// <summary>
        /// This set and returns the value of the string for the header file
        /// </summary>
        private string Header { get; set; }
        /// <summary>
        /// This set and returns the value for the string of declaration
        /// </summary>
        private string Declarations { get; set; }
        /// <summary>
        /// This set and returns the value of the string of global variables
        /// </summary>
        private string Global { get; set; }
        /// <summary>
        /// This set and returns the value of the string of prototypes for all functions(C and C++ feature)
        /// </summary>
        private string Prototypes { get; set; }
        /// <summary>
        /// This sets and returns the value of the strig for setup function
        /// </summary>
        private string Setup { get; set; }
        /// <summary>
        /// This sets and returns the value of the string for functions 
        /// </summary>
        private string Funcs { get; set; }
        /// <summary>
        /// This sets and returns the value of the string for loops
        /// </summary>
        private string Loop { get; set; }
        /// <summary>
        /// This sets and returns the value of the list for PWM pins
        /// </summary>
        private List<string> PWM { get; set; }
        /// <summary>
        /// This set and returns the value of the string for file names
        /// </summary>
        private string FileName { get; set; }
        /// <summary>
        /// This assignes the list of pin definitions to a list of new list of strings
        /// </summary>
        private List<string> PinDefs = new List<string>();
        /// <summary>
        /// This assignes global scope to the global symboltable 
        /// </summary>
        private SymbolTableObject GlobalScope = SymbolTableBuilder.GlobalSymbolTable;
        /// <summary>
        /// This assigns the current scope to the global symbol table
        /// </summary>
        private SymbolTableObject CurrentScope = SymbolTableBuilder.GlobalSymbolTable;
        /// <summary>
        /// This is the constructor for the code generation visitor
        /// </summary>
        /// <param name="fileName">This is the filename</param>
        /// <param name="pwm">This is the PWM pin</param>
        public CodeGenerationVisitor(string fileName, List<string> pwm)
        {
            FileName = fileName;
            PWM = pwm;
        }
        /// <summary>
        /// This method writes header, global, prototypes, declarations, setup, functions and loops in a ".cpp" file
        /// It creats a list of unique pin definitions
        /// </summary>
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
        /// <summary>
        /// This method visits the times node
        /// </summary>
        /// <param name="timesNode">This is the name of the node</param>
        /// <returns>It returns *</returns>
        public override object Visit(TimesNode timesNode)
        {
            return " * ";
        }
        /// <summary>
        /// This method first checks if the left side of the assignment is a Dpin, and Apin has the same input as output
        /// Create the Apin or Dpin as the lefthand side is accepted
        /// If it's a PWM pin, its an Apin otherwise its a Dpin
        /// It then accepts the righthand side and if its numeric, the value is accepted
        /// It can also be assigned to true, high or low
        /// The method can also accept the righthand side is an array
        /// </summary>
        /// <param name="assignmentNode">The name of the node</param>
        /// <returns>It returns an assignment</returns>
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
                if (PWM.Contains(pin))
                {

                    assign += "analogWrite(" + assignmentNode.LeftHand.Accept(this) + ", ";
                    string value = assignmentNode.RightHand.Accept(this) + ")";
                    if (assignmentNode.RightHand.SymbolType.Type == TokenType.NUMERIC)
                        assign += value;
                    else if (value.StartsWith("digitalRead"))
                        assign += value;
                    else
                        assign += value == " true)" ? "HIGH)" : "LOW)";

                }
                else
                {

                    assign += "digitalWrite(" + assignmentNode.LeftHand.Accept(this) + ", ";
                    string boolValue = assignmentNode.RightHand.Accept(this) + ")";
                    if (boolValue.StartsWith("digitalRead"))
                        assign += boolValue;
                    else
                        assign += boolValue == " true)" ? "HIGH)" : "LOW)";
                }
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
                    assign += boolValue == " true)" ? "HIGH)" : "LOW)";
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
        /// <summary>
        /// This method visits a wait node
        /// First assigns a string that prints delay, and then accepts the amount of time and the time modifier
        /// Then closes the parenthesis
        /// </summary>
        /// <param name="waitNode">The name of the node</param>
        /// <returns>It returns delay(Time amount and modifier)</returns>
        public override object Visit(WaitNode waitNode)
        {
            string delay = "delay(";
            delay += waitNode.TimeAmount.Accept(this);
            delay += waitNode.TimeModifier.Accept(this);
            delay += ");";
            return delay;
        }
        /// <summary>
        /// This methods visits a var node
        /// If the var is a numeric it's checked if it is a float or integer
        /// Then it's checked if it's a bool or string var
        /// The var is assigned to an ID
        /// </summary>
        /// <param name="varNode">The name of the token</param>
        /// <returns>It returns an ID for the var</returns>
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
            return varNodeId;
        }
        /// <summary>
        /// This method visits a time second node 
        /// the result is multiplied by 1000 to convert it to milliseconds
        /// </summary>
        /// <param name="timeSecondNode">The name of the node</param>
        /// <returns>It returns the second in milliseconds</returns>
        public override object Visit(TimeSecondNode timeSecondNode)
        {
            return "*1000";
        }
        /// <summary>
        /// This method visits a time minute node 
        /// the result is multiplied by 60000 to convert it to milliseconds
        /// </summary>
        /// <param name="timeMinuteNode">The name of the node</param>
        /// <returns>It returns the minute in milliseconds</returns>
        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            return "*60000";
        }
        /// <summary>
        /// This method visits a time milleseconds node
        /// </summary>
        /// <param name="timeMillisecondNode">The name of the node</param>
        /// <returns>Returns the milliseconds</returns>
        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            return "";
        }
        /// <summary>
        /// This method visits a time minute node
        /// the result is multiplied by 3600000 to convert it to milliseconds
        /// </summary>
        /// <param name="timeHourNode">The name of the node</param>
        /// <returns>Returns is result in milliseconds</returns>
        public override object Visit(TimeHourNode timeHourNode)
        {
            return "*3600000";
        }
        /// <summary>
        /// This method visits a numeric node
        /// If the value is modulo 1 is not 0 then the float or integer value is converted to a string
        /// </summary>
        /// <param name="numericNode">The name of the node</param>
        /// <returns>Returns the numeric value, either int or float</returns>
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
        /// <summary>
        /// This method visits the equal node
        /// </summary>
        /// <param name="equalNode">The name of the node</param>
        /// <returns>Returns "=="</returns>
        public override object Visit(EqualNode equalNode)
        {
            return " == ";
        }
        /// <summary>
        /// This method visits a program node
        /// The headers files are assigned to Header
        /// It checks that the function definitions and function calls Id and parameter count corresponds to the given Id and parameter count
        /// The Id and parameters are retreived from the symbol table
        /// The void setup is set in the global scope
        /// It then accepts statements, assignments and declerations
        /// It then makes the setup function
        /// Then it accepts loop functions and create loop function
        /// Finally it calls the method to write to the .cpp file
        /// </summary>
        /// <param name="programNode">The name of the node</param>
        /// <returns>It returns null</returns>
        public override object Visit(ProgramNode programNode)
        {
            Header += "#include<Arduino.h>\n";
            Header += "/* code generated by the PseudoIno compiler */\n";

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
                Funcs += funcs;
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
            }
            Setup += setupString;
            string loopString = (string)programNode.LoopFunction.Accept(this);
            Loop += loopString;
            PrintStringToFile();
            return null;
        }
        /// <summary>
        /// This method visits the call node
        /// It loops through the number of input parameters and prints them to the callString
        /// The callString can be called by setup or loop
        /// </summary>
        /// <param name="callNode">The name of the node</param>
        /// <returns>It return a call node string</returns>
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
        /// <summary>
        /// This method visits an and node
        /// </summary>
        /// <param name="andNode">The name of the node</param>
        /// <returns>Returns "&&"</returns>
        public override object Visit(AndNode andNode)
        {
            return " && ";
        }
        /// <summary>
        /// This method visits an Apin node
        /// It checks if the analog pin is defined or is a part of an expression
        /// If its a part of an expression it prints the Apin and the ID
        /// </summary>
        /// <param name="apinNode">The name of the node</param>
        /// <returns>Returns the Apin ID or "AnalogueRead(Apin and ID)"</returns>
        public override object Visit(APinNode apinNode)
        {
            return apinNode.Parent == null ? apinNode.Id : $"analogueRead({apinNode.Id})";
        }
        /// <summary>
        /// This method visits an Apin node
        /// It checks if the digital pin is defined or is a part of an expression
        /// If its a part of an expression it prints the Dpin and the ID
        /// </summary>
        /// <param name="dpinNode">The name of the node</param>
        /// <returns>Returns the Dpin ID or "digitalRead(Dpin and ID)"</returns>
        public override object Visit(DPinNode dpinNode)
        {
            return dpinNode.Parent == null ? dpinNode.Id : $"digitalRead({dpinNode.Id})";
        }
        /// <summary>
        /// This method visits a divide node
        /// </summary>
        /// <param name="divideNode">The name of the node</param>
        /// <returns>It returns a "/"</returns>
        public override object Visit(DivideNode divideNode)
        {
            return " / ";
        }
        /// <summary>
        /// This method visits a for loop node
        /// It first checks count value start is smaller then the amount of loop required and then inserts the symbols
        /// It either increments or decrements
        /// The second if statements accepts the statements in the forloop
        /// </summary>
        /// <param name="forNode">The name of the node</param>
        /// <returns>It returns the for loop</returns>
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
        /// <summary>
        /// This method visits a function node
        /// The switch case checks for the function type
        /// Then the input parameters are accepted
        /// Then the function is assigned to a new scope and the statements are accepted
        /// </summary>
        /// <param name="funcNode">The name of the node</param>
        /// <returns>Returns a function</returns>
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

            func += funcNode.Name.Id + "(";

            funcNode.FunctionParameters.ForEach(node => func += findFuncInputparam(node, funcNode) + node.Accept(this) + (funcNode.FunctionParameters.IndexOf(node) < funcNode.FunctionParameters.Count - 1 ? ", " : " "));

            func += ")";
            Global += func + ";";
            Global += "";
            func += "{";
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => func += node.Accept(this));
            }
            func += "}";
            return func;
        }
        /// <summary>
        /// This method checks what type the parameters of a function is
        /// </summary>
        /// <param name="functionsParam">Is a variable, either float, bool, int or string</param>
        /// <param name="function">Is the function the parameter is given to</param>
        /// <returns></returns>
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
        /// <summary>
        /// This method visits a greater node
        /// </summary>
        /// <param name="greaterNode">The name of the node</param>
        /// <returns>It returns ">"</returns>
        public override object Visit(GreaterNode greaterNode)
        {
            return " > ";
        }
        /// <summary>
        /// This method visits a greater or equal node
        /// </summary>
        /// <param name="greaterNode">The name of the node</param>
        /// <returns>It returns ">="</returns>
        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            return " >= ";
        }
        /// <summary>
        /// This method visits an if statement node
        /// It first write "if"
        /// Then it checks if there is an espression to accepts
        /// Then it accepts the statements
        /// </summary>
        /// <param name="ifStatementNode">The name of the node</param>
        /// <returns>It returns an if node</returns>
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
        /// <summary>
        /// This method visits a less node
        /// </summary>
        /// <param name="lessNode">The name of the node</param>
        /// <returns>It returns "<"</returns>
        public override object Visit(LessNode lessNode)
        {
            return " < ";
        }
        /// <summary>
        /// This method visits a less or equal node
        /// </summary>
        /// <param name="lessNode">The name of the node</param>
        /// <returns>It returns "<=" </returns>
        public override object Visit(LessOrEqualNode lessNode)
        {
            return " <= ";
        }
        /// <summary>
        /// This method visits a plus node
        /// </summary>
        /// <param name="plusNode">The name of the node</param>
        /// <returns>It returns a "+" </returns>
        public override object Visit(PlusNode plusNode)
        {
            return " + ";
        }
        /// <summary>
        /// This method visits a minus node
        /// </summary>
        /// <param name="minusNode">The name of the node</param>
        /// <returns>It returns "-" </returns>
        public override object Visit(MinusNode minusNode)
        {
            return " - ";
        }
        /// <summary>
        /// This method visits a modulo node
        /// </summary>
        /// <param name="moduloNode">The name of the node</param>
        /// <returns>It returns the "&" </returns>
        public override object Visit(ModuloNode moduloNode)
        {
            return " % ";
        }
        /// <summary>
        /// This method visits an or node
        /// </summary>
        /// <param name="orNode">The name of the node</param>
        /// <returns>It returns "||" </returns>
        public override object Visit(OrNode orNode)
        {
            return " || ";
        }
        /// <summary>
        /// This method visits a string node
        /// </summary>
        /// <param name="stringNode">The name of the node</param>
        /// <returns>It returns the value of the string node</returns>
        public override object Visit(StringNode stringNode)
        {
            return $" {stringNode.Value} ";
        }
        /// <summary>
        /// This method visits a while node
        /// It first writes "while and accepts the expression
        /// Then it accepts the any statements
        /// </summary>
        /// <param name="whileNode">The name of the node</param>
        /// <returns>It returns a while string </returns>
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
        /// <summary>
        /// This method visits an else statement node
        /// If accepts any statements 
        /// </summary>
        /// <param name="elseStatement">The name of the node</param>
        /// <returns>It returns an else string or nothing, if there is not a else string</returns>
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
        /// <summary>
        /// This method visits an elseif statement node
        /// It checks if there is an expression to accepts
        /// Then it accepts any statement
        /// </summary>
        /// <param name="elseifStatementNode">The name of the node</param>
        /// <returns>It returns an else if</returns>
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            string elseif = "else if (";
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
        /// <summary>
        /// This node visits a return node
        /// It accetps the return value and write "return, the value and add a semicolom 
        /// </summary>
        /// <param name="returnNode"></param>
        /// <returns></returns>
        public override object Visit(ReturnNode returnNode)
        {
            string ret = "return ";
            ret += returnNode.ReturnValue.Accept(this);
            ret += ";";
            return ret;
        }
        /// <summary>
        /// This method visits an expressionterm node
        /// First it checks the type on the lefthand side of the expression is either Apin or Dpin
        /// If also checks if an input is attempted to be uses as an output 
        /// The input for Apin or Dpin is then accepted and written to the file
        /// Lastly the lefthand side of the expression is accepted
        /// </summary>
        /// <param name="expressionTermNode">The name of the node</param>
        /// <returns>It returns the left side of an expression</returns>
        public override object Visit(ExpressionTerm expressionTermNode)
        {
            string exp = "";
            if (expressionTermNode.LeftHand.IsType(typeof(APinNode)) || expressionTermNode.LeftHand.IsType(typeof(DPinNode)))
            {
                string pin = ((PinNode)expressionTermNode.LeftHand).Value;
                if (PinDefs.Any(def => def.Contains(pin) && def.Contains("OUTPUT")))
                    new InvalidCodeException($"Pin {pin} was defined as OUTPUT but is also used as INPUT at {expressionTermNode.Line}:{expressionTermNode.Offset}");
                if (expressionTermNode.LeftHand.Type == TokenType.APIN)
                {
                    PinDefs.Add($"pinMode({pin}, INPUT);");
                    exp += $"analogRead({pin})";
                }
                else
                {
                    PinDefs.Add($"pinMode({pin}, INPUT);");
                    if (PWM.Contains(pin))
                        exp += $"analogRead({pin})";
                    else
                        exp += $"digitalRead({pin})";
                }
                return exp;
            }
            exp += expressionTermNode.LeftHand.Accept(this);
            return exp;
        }
        /// <summary>
        /// This method visits a binary expression with no parenthesis
        /// It accepts the left and right hand side and checks if there is an operator to accept also
        /// </summary>
        /// <param name="noParenExpression"> The name of the node</param>
        /// <returns>It returns an expression</returns>
        public override object Visit(BinaryExpression noParenExpression)
        {
            string exp = "";
            exp += noParenExpression.LeftHand.Accept(this);
            exp += noParenExpression.Operator?.Accept(this);
            exp += noParenExpression.RightHand?.Accept(this);
            return exp;
        }
        /// <summary>
        /// This method visits a parenthesis espression
        /// It accepts the left and right hand side and checks if there is an operator to accept also
        /// </summary>
        /// <param name="parenthesisExpression">The name of the node</param>
        /// <returns>It returns an expression</returns>
        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            string exp = "(";
            exp += parenthesisExpression.LeftHand.Accept(this);
            exp += parenthesisExpression.Operator?.Accept(this);
            exp += parenthesisExpression.RightHand?.Accept(this);
            exp += ")";
            return exp;
        }
        /// <summary>
        /// This visits a bool node
        /// It checks if the value is either true or false
        /// </summary>
        /// <param name="boolNode">The name of the node</param>
        /// <returns>Returns the bool value</returns>
        public override object Visit(BoolNode boolNode)
        {
            string boolVal = "";
            if (boolNode.Value)
                boolVal += " true";
            else
                boolVal += " false";

            return boolVal;
        }
        /// <summary>
        /// This method visit an array node
        /// It checks for the dementions in the array and assign the value to the dementions
        /// </summary>
        /// <param name="arrayNode">The name of the node</param>
        /// <returns>It returns an array</returns>
        public override object Visit(ArrayNode arrayNode)
        {
            string arr = "";
            foreach (NumericNode dimension in arrayNode.Dimensions)
            {
                arr += $"[{dimension.Value}]";
            }
            return arr;
        }
        /// <summary>
        /// This method visits an array access node
        /// 
        /// </summary>
        /// <param name="arrayAccess"></param>
        /// <returns></returns>
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
