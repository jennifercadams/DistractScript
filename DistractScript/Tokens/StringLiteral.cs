using System;

namespace DistractScript.Tokens
{
    public class StringLiteral : LiteralToken
    {
        public string Value { get; private set; }
        public StringLiteral(string value, int line, int column)
            : base(value, line, column) 
        {
            Value = value.Trim().Substring(1, value.Length - 2);
        }
    }
}
