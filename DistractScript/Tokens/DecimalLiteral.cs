using System;

namespace DistractScript.Tokens
{
    public class DecimalLiteral : Token
    {
        public decimal Value { get; private set; }

        public DecimalLiteral(string value, int line)
            : base(value, line)
        {
            Value = Convert.ToDecimal(value);
        }
    }
}
