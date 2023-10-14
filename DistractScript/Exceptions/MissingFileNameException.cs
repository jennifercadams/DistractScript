using System;
using DistractScript.Resources;

namespace DistractScript.Exceptions
{
    public class MissingFileNameException : Exception
    {
        public MissingFileNameException()
            : base(ErrorMessages.MissingFileName) { }
    }
}
