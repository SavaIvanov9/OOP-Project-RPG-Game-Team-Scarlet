namespace SuperRpgGame.Exceptions
{
    using System;

    public class IncorrectRace : Exception
    {
        public IncorrectRace(string message)
            : base(message)
        {
        }
    }
}

