namespace WindowsFormsApplication1.UI
{
    using System;
    using Interfaces;

    public class ConsoleInputReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadKey()
        {
            string input = null;
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.LeftArrow)
            {
                input = "moveLeft";
            }
            if (keyPressed.Key == ConsoleKey.RightArrow)
            {
                input = "moveRight";
            }
            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                input = "moveDown";
            }
            if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                input = "moveUp";
            }
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                input = "exit";
            }
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                input = "skip";
            }
            if (keyPressed.Key == ConsoleKey.S)
            {
                input = "save";
            }

            return input;
        }
    }
}
