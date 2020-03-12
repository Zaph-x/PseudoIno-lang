using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class Node
    {
        public string Value;
        public Node left;
        public Node right;

        public Node(string value)
        {
            this.Value = value;
           
        }
  
    }
}
