namespace RPG_ConsoleGame
{
    using Core.Engines;

    public class Launcher
    {
        // No comments for you! It was hard to write so it should be hard to read.
        static void Main()
        {
           CoreGameEngine.Instance.Run();
        }
    }
}
