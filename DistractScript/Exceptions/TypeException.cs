using System;

namespace DistractScript.Exceptions
{
    public class TypeException : Exception
    {
        private const string InvalidType = "Invalid type: expected {0}, actual {1}. Line {2}:{3}";

        public TypeException(string expected, string actual, int line, int column)
            : base(String.Format(InvalidType, expected, actual, line, column)) { }
    }
}
