using System;
using DistractScript.Resources;

namespace DistractScript.Exceptions
{
    public class TooManyArgumentsException : Exception
    {
        public TooManyArgumentsException()
            : base(ErrorMessages.TooManyArguments) { }
    }
}
