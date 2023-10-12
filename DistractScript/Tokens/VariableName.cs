using System;

namespace DistractScript.Tokens
{
    public class VariableName : Token
    {
        public VariableName(string value, int line, int column)
            : base(value, line, column) { }
    }
}
