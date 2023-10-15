using System;

namespace DistractScript.Exceptions
{
    public class TypeException : Exception
    {
        private const string InvalidType = "Value of invalid type assigned to {0}: expected {1}, actual {2}. Line {3}:{4}";

        public TypeException(string variableName, string expected, string actual, int line, int column)
            : base(String.Format(InvalidType, variableName, expected, actual, line, column))
        { }
    }
}
