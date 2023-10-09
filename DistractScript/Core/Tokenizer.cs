using System;
using System.Collections.Generic;
using System.Linq;
using DistractScript.Tokens;

namespace DistractScript.Core
{
    public static class Tokenizer
    {
        public static string[] Types = { "infodump", "george", "round", "pointy" };

        public static List<Token> GenerateTokens(string text)
        {
            var splitText = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).ToList();
            var tokens = new List<Token>();

            foreach (var substring in splitText)
            {
                if (Types.Contains(substring))
                {
                    var token = new TypeToken(substring);
                    tokens.Add(token);
                }
            }

            return tokens;
        }
    }
}
