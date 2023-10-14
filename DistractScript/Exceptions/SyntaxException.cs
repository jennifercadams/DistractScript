using System;

namespace DistractScript.Exceptions
{
    public class SyntaxException : Exception
    {
        public const string ActualWithExpected = "Unexpected '{0}'; expected '{1}'. Line {2}:{3}";
        public const string ActualOnly = "Unexpected '{0}'. Line {1}:{2}";

        public SyntaxException(string actual, string expected, int line, int col)
            : base(String.Format(ActualWithExpected, actual, expected, line, col)) 
        { }

        public SyntaxException(string actual, int line, int col)
            : base(String.Format(ActualOnly, actual, line, col))
        { }
    }
}
