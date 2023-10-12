﻿using System;

namespace DistractScript.Tokens
{
    public class DecimalLiteral : Token
    {
        public decimal Value { get; private set; }

        public DecimalLiteral(string value, int line, int column)
            : base(value, line, column)
        {
            Value = Convert.ToDecimal(value);
        }
    }
}
