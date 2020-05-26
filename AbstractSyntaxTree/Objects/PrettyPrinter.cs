using System;
using System.Linq;
using AbstractSyntaxTree.Objects.Nodes;
using AbstractSyntaxTree.Objects;

/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    /// <summary>
    /// This class prints the AST nodes
    /// It inherits the visitor class to implement the visit method for each node
    /// </summary>
    public class PrettyPrinter : Visitor

    {

        /// <summary>
        /// This creat indentation on the outprinted AST
        /// When a new scope is created, the an indentation is made 
        /// </summary>
        /// <value></value>

        private int Indent { get; set; } = 0;
        /// <summary>
        /// This method prints the tree structure of the pretty printed AST with indentation and newline
        /// </summary>
        /// <param name="input">The specific node is taken as a string as input</param>
        private void Print(string input)
        {
            string line = "";
            for (int i = 0; i < Indent; i++)
            {
                line += "|---";
            }
            Console.WriteLine(line + input);
        }

        /// <summary>
        /// This method prints the time node and make an indentation
        /// </summary>
        /// <param name="timesNode">The name of the node</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimesNode timesNode)
        {
            Print("TimesNode");
            Indent++;
            return null;
        }

        /// <summary>
        /// This method prints the assignmentNode and make an indentation
        /// It accepts the lefthand side and righthands side of the assignment
        /// Then outdent
        /// </summary>
        /// <param name="assignmentNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(AssignmentNode assignmentNode)
        {
            Print("AssignmentNode");
            Indent++;
            assignmentNode.LeftHand.Accept(this);
            assignmentNode.RightHand.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the waitNode and make an indentation
        /// It accepts a visit of TimeAmount and Timemodifier
        /// Then outdent
        /// </summary>
        /// <param name="waitNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(WaitNode waitNode)
        {
            Print("WaitNode");
            Indent++;
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the varNode 
        /// </summary>
        /// <param name="varNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(VarNode varNode)
        {
            Print("VarNode");

            return null;
        }
        /// <summary>
        /// This method prints the timeSecondNode 
        /// </summary>
        /// <param name="timeSecondNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeSecondNode timeSecondNode)
        {
            Print("TimeSecondNode");

            return null;
        }
        /// <summary>
        /// This method prints the timeMinueNode 
        /// </summary>
        /// <param name="timeMinuteNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeMinuteNode timeMinuteNode)
        {
            Print("TimeMinuteNode");

            return null;
        }
        /// <summary>
        /// This method prints the timeMillisecondNode 
        /// </summary>
        /// <param name="timeMillisecondNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeMillisecondNode timeMillisecondNode)
        {
            Print("TimeMillisecondNode");

            return null;
        }
        /// <summary>
        /// This method prints the timeHourNode 
        /// </summary>
        /// <param name="timeHourNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(TimeHourNode timeHourNode)
        {
            Print("TimeHourNode");

            return null;
        }
        /// <summary>
        /// This method prints the numericNode 
        /// </summary>
        /// <param name="numericNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(NumericNode numericNode)
        {
            Print("NumericNode");

            return null;
        }
        /// <summary>
        /// This method prints the equalNode 
        /// </summary>
        /// <param name="equalNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(EqualNode equalNode)
        {
            Print("EqualNode");

            return null;
        }
        /// <summary>
        /// This method prints the programNode and make an indentation
        /// Then if there exist any function definitions or statements in the program, they will be accepted
        /// Then it make outdent
        /// </summary>
        /// <param name="programNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ProgramNode programNode)
        {
            Print("Program");
            Indent++;
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
            }
            programNode.LoopFunction.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the callNode and make an indentation
        /// It accepts the ID number of the node and all the parameters of the call node
        /// Then outdent
        /// </summary>
        /// <param name="callNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(CallNode callNode)
        {
            Print("CallNode");
            Indent++;
            callNode.Id.Accept(this);
            callNode.Parameters.ForEach(node => node.Accept(this));
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the andNode 
        /// </summary>
        /// <param name="andNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(AndNode andNode)
        {
            Print("AndNode");

            return null;
        }
        /// <summary>
        /// This method prints the apinNode 
        /// </summary>
        /// <param name="apinNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(APinNode apinNode)
        {
            Print("APinNode");

            return null;
        }
        /// <summary>
        /// This method prints the dpinNode 
        /// </summary>
        /// <param name="dpinNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(DPinNode dpinNode)
        {
            Print("DPinNode");

            return null;
        }
        /// <summary>
        /// This method prints the divideNode 
        /// </summary>
        /// <param name="divideNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(DivideNode divideNode)
        {
            Print("DivideNode");

            return null;
        }
        /// <summary>
        /// This method prints the forNode and make an indentation
        /// It accepts the counting variable, the value which it count from and the variable it counts to
        /// Then it accepts all the statements in the for loop node
        /// Last it outdent
        /// </summary>
        /// <param name="forNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ForNode forNode)
        {
            Print("ForNode");
            Indent++;
            forNode.CountingVariable.Accept(this);
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
            }
            //forNode.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the funcNode and make an indentation
        /// It accepts the statements, the name of the function and the parameters
        /// </summary>
        /// <param name="funcNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(FuncNode funcNode)
        {
            Print("FuncNode");
            Indent++;
            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Accept(this));
            }
            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the greaterNode 
        /// </summary>
        /// <param name="greaterNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(GreaterNode greaterNode)
        {
            Print("GreaterNode");
            return null;
        }
        /// <summary>
        /// This method prints the ifStatementNode and make an indentation
        /// It accepts Expresstion if there is any and also accepts all ifStatement nodes
        /// Then outdent
        /// </summary>
        /// <param name="ifStatementNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(IfStatementNode ifStatementNode)
        {
            Print("IfstatementNode");
            Indent++;
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the lessNode 
        /// </summary>
        /// <param name="lessNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(LessNode lessNode)
        {
            Print("LessNode");
            return null;
        }
        /// <summary>
        /// This method prints the plusNode 
        /// </summary>
        /// <param name="plusNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(PlusNode plusNode)
        {
            Print("PlusNode");

            return null;
        }
        /// <summary>
        /// This method prints the minusNode 
        /// </summary>
        /// <param name="minusNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(MinusNode minusNode)
        {
            Print("MinusNode");

            return null;
        }
        /// <summary>
        /// This method prints the moduloNode 
        /// </summary>
        /// <param name="moduloNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ModuloNode moduloNode)
        {
            Print("ModuloNode");

            return null;
        }
        /// <summary>
        /// This method prints the orNode 
        /// </summary>
        /// <param name="orNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(OrNode orNode)
        {
            Print("OrNode");

            return null;
        }
        /// <summary>
        /// This method prints the stringNode 
        /// </summary>
        /// <param name="stringNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(StringNode stringNode)
        {
            Print("StringNode");

            return null;
        }
        /// <summary>
        /// This method prints the whileNode and make an indentation
        /// It accepts expressions and all statements in the while node
        /// Then outdent
        /// </summary>
        /// <param name="whileNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(WhileNode whileNode)
        {
            Print("WhileNode");
            Indent++;
            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the elseStatement and make an indentation
        /// It then accepts all the statement and then outdent
        /// </summary>
        /// <param name="elseStatement">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ElseStatementNode elseStatement)
        {
            Print("ElseStatementNode");
            Indent++;
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the elseifStatementNode and make an indentation
        /// It accepts the Val and Expressions if there exist any
        /// Then it accepts all the statements and then lastly outdent
        /// </summary>
        /// <param name="elseifStatementNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            Print("ElseifStatementNode");
            Indent++;
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the returnNode and make an indentation
        /// Then accepts the value of the return
        /// Then it outdent
        /// </summary>
        /// <param name="returnNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ReturnNode returnNode)
        {
            Print("ReturnNode");
            Indent++;
            returnNode.ReturnValue.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the greaterNode 
        /// </summary>
        /// <param name="greaterNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(GreaterOrEqualNode greaterNode)
        {
            Print("GreaterOrEqualNode");
            return null;
        }
        /// <summary>
        /// This method prints the lessNode 
        /// </summary>
        /// <param name="lessNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(LessOrEqualNode lessNode)
        {
            Print("LessOrEqualNode");
            return null;
        }
        /// <summary>
        /// This method prints the expressionTermNode and make an indentation
        /// Then accepts the lefthand of the expression and then outdent
        /// </summary>
        /// <param name="expressionTermNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ExpressionTerm expressionTermNode)
        {
            Print("ExpressionTerm");
            Indent++;
            expressionTermNode.LeftHand.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the binaryExpresstion and make an indentation
        /// It accepts the lef-and righthandside of the expresstion
        /// It accepts the operator if there exist any
        /// Then it outdent
        /// </summary>
        /// <param name="binaryExpression">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(BinaryExpression binaryExpression)
        {
            Print("BinaryExpression");
            Indent++;
            binaryExpression.LeftHand.Accept(this);
            binaryExpression.Operator?.Accept(this);
            binaryExpression.RightHand?.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the paraenthesisExpression and then make an indentation
        /// It accepts the lef-and righthandside of the expresstion
        /// It accepts the operator if there exist any
        /// Then it outdent
        /// </summary>
        /// <param name="parenthesisExpression">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ParenthesisExpression parenthesisExpression)
        {
            Print("ParenthesisExpression");
            Indent++;
            parenthesisExpression.LeftHand.Accept(this);
            parenthesisExpression.Operator?.Accept(this);
            parenthesisExpression.RightHand?.Accept(this);
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the boolNode 
        /// </summary>
        /// <param name="boolNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(BoolNode boolNode)
        {
            Print("Bool");
            return null;
        }
        /// <summary>
        /// This method prints the arrayNode and make an indentation
        /// Then it accepts all dimensions of the array
        /// Then it outdent
        /// </summary>
        /// <param name="arrayNode">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ArrayNode arrayNode)
        {
            Print("Array");
            Indent++;  
            if (arrayNode.Dimensions.Any())
            {
                arrayNode.Dimensions.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
        /// <summary>
        /// This method prints the arrayAccess and make an indentation
        /// Then it accepts all the array accesses 
        /// Then outdent
        /// </summary>
        /// <param name="arrayAccess">The node to print.</param>
        /// <returns>Returns null</returns>
        public override object Visit(ArrayAccessNode arrayAccess)
        {
            Print("ArrayAccess");
            Indent++;
            if (arrayAccess.Accesses.Any())
            {
                arrayAccess.Accesses.ForEach(node => node.Accept(this));
            }
            Indent--;
            return null;
        }
    }
}
