using System;
using System.Collections.Generic;
using System.Linq;
using DistractScript.Tokens;
using DistractScript.TokenCollections;

namespace DistractScript.Core
{
    public static class Tokenizer
    {
        public static List<Token> GenerateTokens(string text)
        {
            var splitText = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).ToList();
            var tokens = new List<Token>();

            foreach (var substring in splitText)
            {
                if (KeywordCollection.Contains(substring))
                {
                    var token = new KeywordToken(substring);
                    tokens.Add(token);
                }
                if (TypeCollection.Contains(substring))
                {
                    var token = new TypeToken(substring);
                    tokens.Add(token);
                }
            }

            return tokens;
        }
    }
}
