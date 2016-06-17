using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Interfaces
{
    public interface IRender
    {
        void WriteLine(string message, params object[] paramaters);

        void PrintScreen(StringBuilder screen);

        void Clear();
    }
}
