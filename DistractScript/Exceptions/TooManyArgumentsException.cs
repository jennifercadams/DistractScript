using System;

namespace DistractScript.Exceptions
{
    public class TooManyArgumentsException : Exception
    {
        public TooManyArgumentsException()
            : base(Resources.ErrorMessages.TooManyArguments) { }
    }
}
