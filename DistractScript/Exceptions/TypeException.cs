using System;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Exceptions
{
    public class TypeException : Exception
    {
        private const string InvalidType = "Invalid type: expected {0}, actual {1}. Line {2}:{3}";

        public TypeException(Type actual, Type expected, int line, int column)
            : base(String.Format(InvalidType, GetTypeName(expected), GetTypeName(actual), line, column)) { }

        private static string GetTypeName(Type type)
        {
            if (type == typeof(string))
            {
                return TypeCollection.StringType;
            }
            else if (type == typeof(bool))
            {
                return TypeCollection.BoolType;
            }
            else if (type == typeof(int))
            {
                return TypeCollection.IntType;
            }
            else if (type == typeof(decimal))
            {
                return TypeCollection.DecimalType;
            }
            else
            {
                return type.Name;
            }
        }
    }
}
