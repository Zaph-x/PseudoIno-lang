using System;
using System.Collections.Generic;
using System.IO;

namespace Parser.Objects
{
    public class CfgReader
    {
        private bool flag = false;
        List<GrammarRule> GrammarRules = new List<GrammarRule>();
        
        public void GrammarScanner()
        {
            int counter = 0;  
            string line;  
              
            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(@"cfg");  
            while((line = file.ReadLine()) != null)  
            {  
                LineScanner(line);
                Console.WriteLine(line);  
                counter++;  
            }  
              
            file.Close();  
            Console.WriteLine("There were {0} lines.", counter);  
            // Suspend the screen.  
            Console.ReadLine();  
        }

        public void LineScanner(string line)
        {
            if (line.StartsWith(' ') || line.StartsWith('\n'))
            {
                flag = true;
                return;
            }
            else if (line.StartsWith('|'))
            {
                
            }
            else
            {
                
            }
        }
    }

    public class GrammarRule
    {
        public NotTerminal Name;
        List<Production> productions = new List<Production>();

        public GrammarRule(string name)
        {
            Name = new NotTerminal(name);
        }
    }
    
    public class Production
    {
        public int Type;
        public int RuleNumber;
        public Terminal Terminals;
        public NotTerminal NonTerminals;

        public Production(string production, int ruleNumber)
        {
            RuleNumber = ruleNumber;
            if (production.StartsWith('\''))
            {
                Type = 0;
                Terminals = new Terminal(production);
            }
            else
            {
                Type = 1;
                NonTerminals = new NotTerminal(production);
            }
        }
    }

    public class Terminal
    {
        public string Name { get; set; }

        public Terminal(string name)
        {
            Name = name;
        }
    }
    
    public class NotTerminal
    {
        public string Name { get; set; }
        
        public NotTerminal(string name)
        {
            Name = name;
        }
    }
}