using System;

namespace DistractScript.Tokens
{
    public class StringLiteral : Token
    {
        public StringLiteral(string value, int line)
            : base(value, line) { }
    }
}
