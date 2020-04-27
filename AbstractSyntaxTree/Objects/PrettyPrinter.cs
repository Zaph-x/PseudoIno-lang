using System;
using AbstractSyntaxTree.Objects.Nodes;

namespace AbstractSyntaxTree.Objects
{
    public class PrettyPrinter : Visitor
    {
        private int Indent {get;set;} = 0;
        private void Print(string input)  {
            string line = "";
            for (int i = 0; i < Indent; i++)
            {
                line += "|---";
            }
            Console.WriteLine(line + input);
        }

        public new void Visit(ProgramNode node) {
            Print("Program");
            Indent++;
            base.Visit(node);
            Indent--;
        }
        public new void Visit(IfStatementNode node)
        {
            Print("If");
            Indent++;
            base.Visit(node);
            Indent--;
            //ifStatementNode.Expression.Accept(this);
            //if (ifStatementNode.Statements.Any())
            //{
            //    ifStatementNode.Statements.ForEach(node => node.Accept(this));
            //}

        }
    }
}