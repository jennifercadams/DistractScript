using System;

namespace DistractScript.Exceptions
{
    public class OperatorException : Exception
    {
        private const string InvalidOperator = "Operator {0} cannot be used with type {1}";

        public OperatorException(string operatorString, string typeString)
            : base(String.Format(InvalidOperator, operatorString, typeString)) { }
    }
}
