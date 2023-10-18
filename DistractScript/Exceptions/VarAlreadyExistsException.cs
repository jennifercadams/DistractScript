using System;

namespace DistractScript.Exceptions
{
    public class VarAlreadyExistsException : Exception
    {
        private const string VarAlreadyExists = "A variable named '{0}' is already defined in this scope";

        public VarAlreadyExistsException(string name)
            : base(String.Format(VarAlreadyExists, name)) { }
    }
}
