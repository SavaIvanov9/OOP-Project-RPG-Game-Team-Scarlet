namespace SuperRpgGame.Exceptions
{
    using System;

    public class OutOfAmount : Exception
    {
        public OutOfAmount(string message)
            : base(message)
        {
        }
    }
}