using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Objects
{
    class Symboltabelbogen
    {
        List<int> scopeDisplay = new List<int>();
        void OpenScope()
        {
            int depth =+ 1;
            scopeDisplay[depth] = 0;
        }
    void CloseScope()
        {
            foreach (var item in scopeDisplay)
            {

            }
        }
    }
}
