using System;

namespace DistractScript.Tokens
{
    public class Token
    {
        public string StringValue { get; private set; }

        public Token(string value)
        {
            StringValue = value;
        }
    }
}
