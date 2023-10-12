using System;

namespace DistractScript.Tokens
{
    public class StringLiteral : Token
    {
        public StringLiteral(string value)
            : base(value) { }
    }
}
