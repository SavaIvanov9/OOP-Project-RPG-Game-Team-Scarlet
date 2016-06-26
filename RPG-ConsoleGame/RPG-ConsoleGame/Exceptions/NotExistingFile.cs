namespace SuperRpgGame.Exceptions
{
    using System;

    public class NotExistingFile : Exception
    {
        public NotExistingFile(string message)
            : base(message)
        {
        }
    }
}


