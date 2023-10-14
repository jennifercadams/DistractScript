﻿using System;

namespace DistractScript.Tokens
{
    public class DecimalLiteral : LiteralToken
    {
        public decimal Value { get; private set; }

        public DecimalLiteral(string value, int line, int column)
            : base(value, line, column)
        {
            Type = typeof(decimal);
            Value = Convert.ToDecimal(value);
        }
    }
}
