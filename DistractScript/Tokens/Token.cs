using System;

namespace DistractScript.Tokens
{
    public class Token
    {
        public string StringValue { get; private set; }
        public int Line { get; private set; }

        public Token(string value, int line)
        {
            StringValue = value;
            Line = line;
        }
    }
}
