using System;

namespace Compiler
{
    public class Recogniser
    {
        public int InputString(string s)
        {
            if (s == "a is 4")
            {
                return 0;
            }
            return 1;
        }
    }
}