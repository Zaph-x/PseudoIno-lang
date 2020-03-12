using System.Collections.Generic;

namespace Parser
{
    //TODO gem tokens fra parser i en liste<T> og call AST får at køre preorder tree walk . print tree.
    //TODO gem værdierne i tree structur.
    /// <summary>
    /// This is the AST main class
    /// </summary>
    public class AST
    {
        public List<string> ListAST = new List<string>();
        public AST(List<string> List)
        {
            this.ListAST = List;
            System.Console.WriteLine("Preorder tree walk of the AST:\n");
            Preorder(1);
        }

        private int GetRightChild(int index)
        {
            int i = ((2 * index) + 1);

            if (i <= ListAST[index].Length)
            {
                return i;
            }
            else
            {
                throw new System.NotImplementedException("No right child found.");

            }
        }
        private int Getleftchild(int index)
        {
            int i = (2 * index);
            if (i <= ListAST[index].Length)
            {
                return i;
            }
            else
            {
                throw new System.NotImplementedException("No left child found.");

            }

        }
        public void Preorder(int index)
        {
           
            if (index > 0)
            {
                System.Console.WriteLine(ListAST[index]); // visiting root
                Preorder(Getleftchild(index)); //visiting left subtree
                Preorder(GetRightChild(index)); //visiting right subtree
            }
        }
    }
}

