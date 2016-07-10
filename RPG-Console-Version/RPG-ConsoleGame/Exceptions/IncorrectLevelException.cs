namespace RPG_ConsoleGame.Exceptions
{
    using System;

    public class IncorrectLevelException : Exception
    {
        public IncorrectLevelException(string message)
            : base(message)
        {
        }
    }
}
