using System;

namespace DistractScript.Exceptions
{
    public class TooManyArgumentsException : Exception
    {
        private const string TooManyArguments = "Sorry, DistractScript can't multitask. Enter exactly one filename.";

        public TooManyArgumentsException()
            : base(TooManyArguments) { }
    }
}
