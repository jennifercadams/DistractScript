using System;

namespace DistractScript.Tokens
{
    public class BoolLiteral : Token
    {
        public bool Value { get; private set; }

        public BoolLiteral(string value)
            : base(value)
        {
            Value = Convert.ToBoolean(value);
        }
    }
}
