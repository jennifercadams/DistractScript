using System;

namespace DistractScript.Exceptions
{
    public class VarDoesNotExistException : Exception
    {
        public const string VarDoesNotExist = "The variable '{0}' does not exist in this scope";

        public VarDoesNotExistException(string name)
            : base(String.Format(VarDoesNotExist, name)) { }
    }
}
