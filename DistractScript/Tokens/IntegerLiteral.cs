using System;

namespace DistractScript.Tokens
{
    public class IntegerLiteral : LiteralToken
    {
        public int Value { get; private set; }

        public IntegerLiteral(string value, int line, int column)
            : base(value, line, column)
        {
            Value = Convert.ToInt32(value);
        }
    }
}
