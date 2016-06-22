using System;
using System.Collections.Generic;
using System.Linq;
namespace RPG_ConsoleGame.Interfaces
{
    using System.Text;

    public interface IRender
    {
        void WriteLine(string message, params object[] paramaters);

        void PrintScreen(StringBuilder screen);

        void Clear();
    }
}
