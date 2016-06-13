using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.UserInterface
{
    using Interfaces;
    public class ConsoleRender : IRender
    {
        public void WriteLine(string message, params object[] paramaters)
        {
            Console.WriteLine(message, paramaters);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
