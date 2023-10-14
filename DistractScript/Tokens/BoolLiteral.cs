using System;

namespace DistractScript.Tokens
{
    public class BoolLiteral : LiteralToken
    {
        public bool Value { get; private set; }

        public BoolLiteral(string value, int line, int column)
            : base(value, line, column)
        {
            Value = Convert.ToBoolean(value);
        }
    }
}
