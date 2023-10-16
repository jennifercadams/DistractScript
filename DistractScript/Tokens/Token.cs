using System;

namespace DistractScript.Tokens
{
    public class Token
    {
        public string StringValue { get; protected set; }
        public int Line { get; private set; }
        public int Column { get; private set; }

        public Token(string value, int line, int column)
        {
            StringValue = value;
            Line = line;
            Column = column;
        }
    }
}
