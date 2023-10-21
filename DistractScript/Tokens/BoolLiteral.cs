using System;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Tokens
{
    public class BoolLiteral : LiteralToken
    {
        public bool Value { get; private set; }

        public BoolLiteral(string value, int line, int column)
            : base(value, line, column)
        {
            Type = typeof(bool);
            Value = Convert.ToBoolean(value);
        }

        public BoolLiteral(bool value, int line, int column)
            : base("", line, column)
        {
            Type = typeof(bool);
            Value = value;

            if (value == true)
                StringValue = LiteralCollection.True;
            else
                StringValue = LiteralCollection.False;
        }
    }
}
