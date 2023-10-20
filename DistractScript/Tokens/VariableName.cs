using System;

namespace DistractScript.Tokens
{
    public class VariableName : Token
    {
        public Type Type { get; private set; }

        public VariableName(string value, Type type, int line, int column)
            : base(value, line, column)
        {
            Type = type;
        }
    }
}
