using AbstractSyntaxTree.Objects.Nodes;
using System.Linq;
using AbstractSyntaxTree.Objects;
using Lexer.Objects;
using SymbolTable;

namespace Contextual_analysis
{
    public class TypeChecker : Visitor
    {
        private TypeContext TermType { get; set; }
        private SymbolTableObject _symbolTabelGlobal = new SymbolTableObject();
        private SymbolTableBuilder _symbolTableBuilder;
        public override object Visit(BeginNode beginNode)
        {
            beginNode.LoopNode.Accept(this);
            return null;
        }



        public override object Visit(TimeNode timeNode) { 
            return null;

        }

        public override object Visit(DeclParametersNode declParametersNode)
        {
            if (declParametersNode.Parameters.Any())
            {
                declParametersNode.Parameters.ForEach(stmnt => stmnt.Accept(this));
            }
            return null;
        }

        public override object Visit(TimesNode timesNode) { 
            return null;

        }

        public override object Visit(FunctionLoopNode loopFnNode)
        {
            if (loopFnNode.Statements.Any())
            {
                loopFnNode.Statements.ForEach(stmnt => stmnt.Accept(this));
            }
            return null;
        }

        public override object Visit(AssignmentNode assignmentNode)
        {
            ((IAssginable)assignmentNode.LeftHand).Accept(this);
            // if (((VarNode)assignmentNode.LeftHand).SymbolType == null)
            // {
            //     assignmentNode.LeftHand.Accept(this);

            //     _symbolTableBuilder.AddSymbol(assignmentNode);
            //     //hvis id == assigmenttype så ok ellers error.
            //     AstNode typeid = ((AstNode)assignmentNode.LeftHand);
            //     AstNode typeassigment = ((AstNode)assignmentNode.Assignment);
            //     //if(typeid.)


            // }
            //VarNode id = (VarNode)assignmentNode.LeftHand.Accept(this);
            //dette tilføjer venstre side til symboltable, brug addref til at sætte højre side af statement. De skal ske til sidst når alt er kontrolleret.
            _symbolTableBuilder.AddSymbol(assignmentNode);
            //((ExpressionNode)assignmentNode.Assignment).Accept(this);
            //TODO der er interface med IAssginable LeftHand { get; set; } og public IAssignment Assignment { get; set; } de har ikke accept metode.
            return null;
        }

        public override object Visit(StatementNode statementNode) { 
            return null;

        }

        public override object Visit(WithNode withNode) { 
            return null;

        }

        public override object Visit(WaitNode waitNode)
        {
            waitNode.TimeAmount.Accept(this);
            waitNode.TimeModifier.Accept(this);
            return null;
        }

        public override object Visit(VarNode varNode)
        {
            return varNode.SymbolType.Type == TokenType.VAR ? null /* Look up in symboltable */ : varNode.SymbolType;
        }

        public override object Visit(ValNode valNode) {
            return null;

         }

        public override object Visit(TimeSecondNode timeSecondNode) { 
            return null;

        }

        public override object Visit(TimeMinuteNode timeMinuteNode) { 
            return null;

        }

        public override object Visit(TimeMillisecondNode timeMillisecondNode) {
            return null;

         }

        public override object Visit(TimeHourNode timeHourNode) {
            return null;

         }

        public override object Visit(RightParenthesisNode rightParenthesisNode) { 
            return null;

        }

        public override object Visit(NumericNode numericNode) { 
            return numericNode.SymbolType;

        }

        public override object Visit(NewlineNode newlineNode) { 
            return null;

        }

        public override object Visit(LeftParenthesisNode leftParenthesisNode) { 
            return null;

        }

        public override object Visit(InNode inNode) { 
            return null;

        }
        public override object Visit(EqualNode equalNode) { 
            return null;

        }

        public override object Visit(EqualsNode equalsNode) { 
            return null;

        }

        public override object Visit(EOFNode eOFNode) {
            return null;
            
        }

        public override object Visit(EpsilonNode epsilonNode) { 
            return null;
            
        }

        public override object Visit(DoNode doNode) { 
            return null;

        }

        public override object Visit(ProgramNode programNode)
        {
            _symbolTableBuilder = new SymbolTableBuilder(_symbolTabelGlobal);
            _symbolTableBuilder.CurrentSymbolTable = _symbolTabelGlobal;

            _symbolTableBuilder.OpenScope(TokenType.PROG, "main");
            if (programNode.FunctionDefinitons.Any())
            {
                programNode.FunctionDefinitons.ForEach(node => node.Parent = programNode);
                //programNode.FunctionDefinitons.ForEach(node => _symbolTableBuilder.AddNode(node));
                programNode.FunctionDefinitons.ForEach(node => node.Accept(this));
            }
            if (programNode.Statements.Any())
            {
                programNode.Statements.ForEach(node => node.Accept(this));
                programNode.Statements.ForEach(node => node.Parent = programNode);
                //programNode.Statements.ForEach(node => _symbolTableBuilder.AddNode(node));
            }
            programNode.LoopFunction.Accept(this);
            _symbolTableBuilder.CloseScope();

            _symbolTableBuilder.MakeFinalTable();
            return null;
        }

        public override object Visit(CallNode callNode)
        {
            callNode.Id.Accept(this);
            _symbolTableBuilder.AddRef(callNode);
            callNode.Parameters.ForEach(node => node.Accept(this));
            foreach (var node in callNode.Parameters)
            {
                if (node.Type == TokenType.VAR)
                {
                    _symbolTableBuilder.AddRef(node);
                }
            }
            return null;
        }

        public override object Visit(EndNode endNode) { 
            return null;

        }
        public override object Visit(AndNode andNode) { 
            return null;

        }
        public override object Visit(PinNode pinNode) { 
            return null;

        }
        public override object Visit(APinNode apinNode) {
            return null;
            
         }
        public override object Visit(DPinNode dpinNode) { 
            return null;

        }
        public override object Visit(OperatorNode operatorNode) {
            return null;
            
         }
        public override object Visit(BoolOperatorNode boolOperatorNode) {
            return null;

         }
        public override object Visit(CallParametersNode callParametersNode)
        {
            callParametersNode.Parameters.ForEach(node => node.Accept(this));
            return null;
        }
        public override object Visit(DivideNode divideNode) { 
            return null;

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
            _symbolTableBuilder.OpenScope(TokenType.FOR, "for");
            forNode.CountingVariable.Accept(this);
            forNode.From.Accept(this);
            forNode.To.Accept(this);
            if (forNode.Statements.Any())
            {
                forNode.Statements.ForEach(node => node.Accept(this));
                forNode.Statements.ForEach(node => node.Parent = forNode);
            }
            //forNode.Accept(this);
            _symbolTableBuilder.CloseScope();
            return null;
        }
        public override object Visit(FuncNode funcNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.FUNC, funcNode.Name.Id);

            //funcNode.Accept(this);
            if (funcNode.Statements.Any())
            {
                funcNode.Statements.ForEach(node => node.Parent = funcNode);

                funcNode.Statements.ForEach(node => node.Accept(this));
            }

            funcNode.Name.Accept(this);
            funcNode.FunctionParameters.ForEach(node => node.Accept(this));
            _symbolTableBuilder.CloseScope();
            return null;

        }
        public override object Visit(GreaterNode greaterNode)
        {
            greaterNode.OrEqualNode.Accept(this);
            //greaterNode.Accept(this);
            return null;
        }
        public override object Visit(IfStatementNode ifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.IFSTMNT, "if");
            ifStatementNode.Expression?.Accept(this);
            if (ifStatementNode.Statements.Any())
            {
                ifStatementNode.Statements.ForEach(node => node.Parent = ifStatementNode);

                ifStatementNode.Statements.ForEach(node => node.Accept(this));

            }
            _symbolTableBuilder.CloseScope();
            return null;
        }
        public override object Visit(LessNode lessNode)
        {
            lessNode.OrEqualNode.Accept(this);
            return null;
        }
        public override object Visit(LoopNode loopNode) { return null; }
        public override object Visit(MathOperatorNode mathOperatorNode) { return null; }
        public override object Visit(PlusNode plusNode) { return null; }
        public override object Visit(MinusNode minusNode) { return null; }
        public override object Visit(ModuloNode moduloNode) {
            return null;
        }
        public override object Visit(OrNode orNode) {
            return null;
        }
        public override object Visit(StringNode stringNode) {
            return null;
        }
        public override object Visit(WhileNode whileNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.WHILE, "while");

            whileNode.Expression.Accept(this);
            if (whileNode.Statements.Any())
            {
                whileNode.Statements.ForEach(node => node.Accept(this));
                whileNode.Statements.ForEach(node => node.Parent = whileNode);

            }
            _symbolTableBuilder.CloseScope();
            return null;
        }
        public override object Visit(ElseStatementNode elseStatement)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSESTMNT, "else");
            if (elseStatement.Statements.Any())
            {
                elseStatement.Statements.ForEach(node => node.Accept(this));
                elseStatement.Statements.ForEach(node => node.Parent = elseStatement);

            }
            //elseStatement.Accept(this);
            _symbolTableBuilder.CloseScope();
            //symbolTabel.AddNode(elseStatement.ToString(), elseStatement);
            return null;
        }
        public override object Visit(ElseifStatementNode elseifStatementNode)
        {
            _symbolTableBuilder.OpenScope(TokenType.ELSEIFSTMNT, "elseif");
            elseifStatementNode.Val?.Accept(this);
            elseifStatementNode.Expression?.Accept(this);
            if (elseifStatementNode.Statements.Any())
            {
                elseifStatementNode.Statements.ForEach(node => node.Accept(this));
                elseifStatementNode.Statements.ForEach(node => node.Parent = elseifStatementNode);

            }
            //elseifStatementNode.Accept(this);
            _symbolTableBuilder.CloseScope();
            //symbolTabel.AddNode(elseifStatementNode.ToString(), elseifStatementNode);
            return null;
        }
        public override object Visit(RangeNode rangeNode)
        {
            rangeNode.From.Accept(this);
            rangeNode.To.Accept(this);
            //rangeNode.Accept(this);
            return null;
        }

        public override object Visit(ReturnNode returnNode)
        {
            returnNode.ReturnValue.Accept(this);
            return null;
        }

        public TypeContext GetExpressionType(IExpr node)
        {
            if (TermType == null)
                TermType = ((AstNode)node.LeftHand).SymbolType;
            if (node.RightHand != null)
            {
                if (((AstNode)node.RightHand.LeftHand).SymbolType != TermType || ((AstNode)node.RightHand.LeftHand).SymbolType.Type != TokenType.EXPR)
                {
                    // TODO ERROR code goes here
                }
                else
                {
                    return GetExpressionType(node.RightHand);
                }
            }
            else if (((AstNode)node.LeftHand).SymbolType.Type == TokenType.EXPR)
            {
                return GetExpressionType((ExpressionNode)node.LeftHand);
            }
            TermType = null;
            return ((AstNode)node.LeftHand).SymbolType;
           
        }
    }
}
