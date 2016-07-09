namespace WindowsFormsApplication1
{
    using System;
    using Core.Engines;

    static class Launcher
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CoreGameEngine.Instance.Run();
           
        }
    }
}
