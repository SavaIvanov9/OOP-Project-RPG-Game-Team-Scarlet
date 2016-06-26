namespace SuperRpgGame.Exceptions
{
    using System;

    public class IncorrectName : Exception
    {
        public IncorrectName(string message)
            : base(message)
        {
        }
    }
}

