using System;

namespace DistractScript.Tokens
{
    public class VariableName : Token
    {
        public VariableName(string value, int line)
            : base(value, line) { }
    }
}
