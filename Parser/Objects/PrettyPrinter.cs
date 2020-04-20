using System;
using Parser.Objects.Nodes;

namespace Parser.Objects
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
    }
}