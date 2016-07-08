namespace WindowsFormsApplication1.Exceptions
{
    using System;

    public class IncorrectChoiceException : Exception
    {
        public IncorrectChoiceException(string message)
            : base(message)
        {
        }
    }
}