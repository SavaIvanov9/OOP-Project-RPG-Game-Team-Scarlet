namespace RPG_ConsoleGame.Exceptions
{
    using System;

    public class IncorrectNameException : Exception
    {
        public IncorrectNameException(string message)
            : base(message)
        {
        }
    }
}

