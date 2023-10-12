using System;

namespace DistractScript.Tokens
{
    public class BoolLiteral : Token
    {
        public bool Value { get; private set; }

        public BoolLiteral(string value, int line)
            : base(value, line)
        {
            Value = Convert.ToBoolean(value);
        }
    }
}
