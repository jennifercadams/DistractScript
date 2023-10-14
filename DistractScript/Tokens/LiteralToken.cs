using System;

namespace DistractScript.Tokens
{
    public abstract class LiteralToken : Token
    {
        public LiteralToken(string value, int line, int column)
            : base(value, line, column) { }
    }
}
