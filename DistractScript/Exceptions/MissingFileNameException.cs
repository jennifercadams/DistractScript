using System;

namespace DistractScript.Exceptions
{
    public class MissingFileNameException : Exception
    {
        public MissingFileNameException()
            : base(Resources.ErrorMessages.MissingFileName) { }
    }
}
