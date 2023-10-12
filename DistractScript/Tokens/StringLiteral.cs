using System;

namespace DistractScript.Tokens
{
    public class StringLiteral : Token
    {
        public StringLiteral(string value, int line, int column)
            : base(value, line, column) { }
    }
}
