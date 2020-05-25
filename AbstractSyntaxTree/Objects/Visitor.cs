using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;
/// <summary>
/// The namespace means that objects of the Abstract syntax tree is used
/// </summary>
namespace AbstractSyntaxTree.Objects
{
    public abstract class Visitor
    {
        /// <summary>
        /// This method visits a Time node
        /// </summary>
        /// <param name="timesNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(TimesNode timesNode);
        /// <summary>
        /// This vitits a Assignment node
        /// </summary>
        /// <param name="assignmentNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(AssignmentNode assignmentNode);
        /// <summary>
        /// This visits a Wait node
        /// </summary>
        /// <param name="waitNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(WaitNode waitNode);
        /// <summary>
        /// This visits a Var node
        /// </summary>
        /// <param name="varNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(VarNode varNode);
        /// <summary>
        /// This visits a TimeSecond node
        /// </summary>
        /// <param name="timeSecondNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(TimeSecondNode timeSecondNode);
        /// <summary>
        /// This visits a TimeMinute node
        /// </summary>
        /// <param name="timeMinuteNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(TimeMinuteNode timeMinuteNode);
        /// <summary>
        /// This visits TimeMilliSecond node
        /// </summary>
        /// <param name="timeMillisecondNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(TimeMillisecondNode timeMillisecondNode);
        /// <summary>
        /// This visits a TimeHour node
        /// </summary>
        /// <param name="timeHourNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(TimeHourNode timeHourNode);
        /// <summary>
        /// This visits a Numeric node
        /// </summary>
        /// <param name="numericNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(NumericNode numericNode);
        /// <summary>
        /// This visits an Equal node
        /// </summary>
        /// <param name="equalNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(EqualNode equalNode);
        /// <summary>
        /// This visits a Program node
        /// </summary>
        /// <param name="programNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ProgramNode programNode);
        /// <summary>
        /// This visits a Call node
        /// </summary>
        /// <param name="callNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(CallNode callNode);
        /// <summary>
        /// This visits an And node
        /// </summary>
        /// <param name="andNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(AndNode andNode);
        /// <summary>
        /// This visits an APin node
        /// </summary>
        /// <param name="apinNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(APinNode apinNode);
        /// <summary>
        /// This visits a DPin node
        /// </summary>
        /// <param name="dpinNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(DPinNode dpinNode);
        /// <summary>
        /// This visits a Divide node
        /// </summary>
        /// <param name="divideNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(DivideNode divideNode);
        /// <summary>
        /// This visits a For node
        /// </summary>
        /// <param name="forNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ForNode forNode);
        /// <summary>
        /// This visits a Func node
        /// </summary>
        /// <param name="funcNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(FuncNode funcNode);
        /// <summary>
        /// This visits a Greater node
        /// </summary>
        /// <param name="greaterNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(GreaterNode greaterNode);
        /// <summary>
        /// This visits a Greater or equal node
        /// </summary>
        /// <param name="greaterNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(GreaterOrEqualNode greaterNode);
        /// <summary>
        /// This visits an If statement node
        /// </summary>
        /// <param name="ifStatementNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(IfStatementNode ifStatementNode);
        /// <summary>
        /// This visits a less node
        /// </summary>
        /// <param name="lessNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(LessNode lessNode);
        /// <summary>
        /// This visits a less or equal node
        /// </summary>
        /// <param name="lessNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(LessOrEqualNode lessNode);
        /// <summary>
        /// This visits a plus node
        /// </summary>
        /// <param name="plusNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(PlusNode plusNode);
        /// <summary>
        /// This visits a minus node
        /// </summary>
        /// <param name="minusNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(MinusNode minusNode);
        /// <summary>
        /// This visits a Modulo node
        /// </summary>
        /// <param name="moduloNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ModuloNode moduloNode);
        /// <summary>
        /// This visits an Or node
        /// </summary>
        /// <param name="orNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(OrNode orNode);
        /// <summary>
        /// This visits a string node
        /// </summary>
        /// <param name="stringNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(StringNode stringNode);
        /// <summary>
        /// This visits a while node
        /// </summary>
        /// <param name="whileNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(WhileNode whileNode);
        /// <summary>
        /// This visits an Else statement
        /// </summary>
        /// <param name="elseStatement">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ElseStatementNode elseStatement);
        /// <summary>
        /// This visits an ElseIf statement
        /// </summary>
        /// <param name="elseifStatementNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ElseifStatementNode elseifStatementNode);
        /// <summary>
        /// This visits a Return node
        /// </summary>
        /// <param name="returnNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ReturnNode returnNode);
        /// <summary>
        /// This visits an ExpressionTerm node
        /// </summary>
        /// <param name="expressionTermNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ExpressionTerm expressionTermNode);
        /// <summary>
        /// This visits an no Paranthesis Expression node
        /// </summary>
        /// <param name="noParenExpression">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(BinaryExpression noParenExpression);
        /// <summary>
        /// This visits a Paranthesis Expression node
        /// </summary>
        /// <param name="parenthesisExpression">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ParenthesisExpression parenthesisExpression);
        /// <summary>
        /// This visits a Boolean node
        /// </summary>
        /// <param name="boolNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(BoolNode boolNode);
        /// <summary>
        /// This visits an Array node
        /// </summary>
        /// <param name="arrayNode">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ArrayNode arrayNode);
        /// <summary>
        /// This visits an Array Access node 
        /// </summary>
        /// <param name="arrayAccess">The visited node</param>
        /// <returns></returns>
        public abstract object Visit(ArrayAccessNode arrayAccess);
    }
}