namespace WindowsFormsApplication1.UI
{
    using System;
    using System.Text;
    using Interfaces;

    public class ConsoleRender : IRender
    {
        public void WriteLine(string message, params object[] paramaters)
        {
            Console.WriteLine(message, paramaters);
        }

        public void PrintScreen(StringBuilder screen)
        {
            Console.WriteLine(screen);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
