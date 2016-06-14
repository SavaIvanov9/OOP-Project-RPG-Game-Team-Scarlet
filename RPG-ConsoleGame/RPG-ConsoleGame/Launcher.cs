using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Engine;
using RPG_ConsoleGame.Interfaces;
using RPG_ConsoleGame.UserInterface;

namespace RPG_ConsoleGame
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            //IRender render = new ConsoleRender();
            //IInputReader reader = new ConsoleInputReader();

            //GameEngine engine = new GameEngine(reader, render);

            //engine.Run();

            GameEngine.Instance.Run();
        }
    }
}
