namespace RPG_ConsoleGame.Exceptions
{
    using System;

    class IncorrectTypeException : Exception
    {
        public IncorrectTypeException(string message)
            : base(message)
        {
        }
    }
}
