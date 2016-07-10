namespace RPG_ConsoleGame.Exceptions
{
    using System;

    public class OutOfAmountException : Exception
    {
        public OutOfAmountException(string message)
            : base(message)
        {
        }
    }
}