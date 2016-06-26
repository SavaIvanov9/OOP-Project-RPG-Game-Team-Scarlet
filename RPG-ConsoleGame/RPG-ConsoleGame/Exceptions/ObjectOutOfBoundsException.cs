namespace SuperRpgGame.Exceptions
{
    using System;

    public class ObjectOutOfBoundsException : Exception
    {
        public ObjectOutOfBoundsException(string message)
            : base(message)
        {
            //used in GameObject Position, but currently is commented there
        }
    }
}