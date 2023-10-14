using System;

namespace DistractScript.Tokens
{
    public abstract class LiteralToken : Token
    {
        public Type Type { get; protected set; }

        public LiteralToken(string value, int line, int column)
            : base(value, line, column) { }
    }
}
