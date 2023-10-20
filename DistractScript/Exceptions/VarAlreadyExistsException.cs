using System;

namespace DistractScript.Exceptions
{
    public class VarAlreadyExistsException : Exception
    {
        private const string VarAlreadyExists = "A variable named '{0}' is already defined in this scope. Line {1}:{2}";

        public VarAlreadyExistsException(string name, int line, int column)
            : base(String.Format(VarAlreadyExists, name, line, column)) { }
    }
}
