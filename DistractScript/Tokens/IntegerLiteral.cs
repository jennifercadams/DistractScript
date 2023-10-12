using System;

namespace DistractScript.Tokens
{
    public class IntegerLiteral : Token
    {
        public int Value { get; private set; }

        public IntegerLiteral(string value, int line)
            : base(value, line)
        {
            Value = Convert.ToInt32(value);
        }
    }
}
