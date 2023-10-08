using System;
using System.Collections.Generic;

namespace DistractScript
{
    public static class Tokenizer
    {
        public static List<string> Split(string text)
        {
            var tokens = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(tokens);
        }
    }
}
