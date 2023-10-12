using System;
using System.Collections.Generic;
using DistractScript.Tokens;
using DistractScript.Collections.TokenCollections;
using System.Text.RegularExpressions;

namespace DistractScript.Core
{
    public static class Tokenizer
    {
        public static List<Token> GenerateTokens(string text)
        {
            var splitText = Split(text);
            var tokens = new List<Token>();
            var line = 1;

            foreach (var substring in splitText)
            {
                if (substring == "\n")
                {
                    line++;
                    continue;
                }

                Token token;
                if (KeywordCollection.Contains(substring))
                {
                    token = new KeywordToken(substring, line);
                }
                else if (OperatorCollection.Contains(substring))
                {
                    token = new OperatorToken(substring, line);
                }
                else if (TypeCollection.Contains(substring))
                {
                    token = new TypeToken(substring, line);
                }
                else if (substring[0] == '"')
                {
                    token = new StringLiteral(substring, line);
                }
                else if (substring == "true" || substring == "false")
                {
                    token = new BoolLiteral(substring, line);
                }
                else if (IsInteger(substring))
                {
                    token = new IntegerLiteral(substring, line);
                }
                else if (IsDecimal(substring))
                {
                    token = new DecimalLiteral(substring, line);
                }
                else if (substring == ";")
                {
                    token = new SeparatorToken(substring, line);
                }
                else
                {
                    token = new VariableName(substring, line);
                }
                tokens.Add(token);
            }

            return tokens;
        }

        private static List<string> Split(string text)
        {
            var splitText = new List<string>();

            string token = "";
            for (var i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c == '\n' || c == ';')
                {
                    if (token != "")
                    {
                        splitText.Add(token);
                        token = "";
                    }
                    splitText.Add(c.ToString());
                }
                else if (char.IsWhiteSpace(c) && token == "")
                {
                    continue;
                }
                else if (char.IsWhiteSpace(c) && token != "")
                {
                    splitText.Add(token);
                    token = "";
                }
                else if (c == '"')
                {
                    var endIndex = text.IndexOf('"', i + 1);
                    var stringLiteral = text.Substring(i, endIndex - i + 1);
                    splitText.Add(stringLiteral);
                    i = endIndex;
                    token = "";
                }
                else
                {
                    token += c;
                }
            }

            return splitText;
        }

        private static bool IsInteger(string stringValue)
        {
            var integerPattern = @"^\d+$";
            return Regex.IsMatch(stringValue, integerPattern);
        }

        private static bool IsDecimal(string stringValue)
        {
            var decimalPattern = @"^\d+\.\d+$";
            return Regex.IsMatch(stringValue, decimalPattern);
        }
    }
}
