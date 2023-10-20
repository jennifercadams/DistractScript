using System;

namespace DistractScript.Exceptions
{
    public class VarDoesNotExistException : Exception
    {
        private const string VarDoesNotExist = "The variable '{0}' does not exist in this scope. Line {1}:{2}";

        public VarDoesNotExistException(string name, int line, int column)
            : base(String.Format(VarDoesNotExist, name, line, column)) { }
    }
}
