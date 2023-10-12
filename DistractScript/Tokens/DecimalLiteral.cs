using System;

namespace DistractScript.Tokens
{
    public class DecimalLiteral : Token
    {
        public decimal Value { get; private set; }

        public DecimalLiteral(string value)
            : base(value)
        {
            Value = Convert.ToDecimal(value);
        }
    }
}
