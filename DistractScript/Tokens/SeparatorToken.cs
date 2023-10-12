using System;

namespace DistractScript.Tokens
{
    public class SeparatorToken : Token
    {
        public SeparatorToken(string value, int line, int column)
            : base(value, line, column) { }
    }
}
