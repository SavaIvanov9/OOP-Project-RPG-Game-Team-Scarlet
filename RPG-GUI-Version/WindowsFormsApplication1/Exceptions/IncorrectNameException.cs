namespace WindowsFormsApplication1.Exceptions
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

