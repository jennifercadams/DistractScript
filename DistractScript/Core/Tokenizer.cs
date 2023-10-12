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
            var column = 0;

            foreach (var substring in splitText)
            {
                var tokenString = substring.Trim();

                Token token;
                if (KeywordCollection.Contains(tokenString))
                {
                    token = new KeywordToken(tokenString, line, column);
                }
                else if (OperatorCollection.Contains(tokenString))
                {
                    token = new OperatorToken(tokenString, line, column);
                }
                else if (TypeCollection.Contains(tokenString))
                {
                    token = new TypeToken(tokenString, line, column);
                }
                else if (tokenString[0] == '"')
                {
                    token = new StringLiteral(tokenString, line, column);
                }
                else if (tokenString == "true" || tokenString == "false")
                {
                    token = new BoolLiteral(tokenString, line, column);
                }
                else if (IsInteger(tokenString))
                {
                    token = new IntegerLiteral(tokenString, line, column);
                }
                else if (IsDecimal(tokenString))
                {
                    token = new DecimalLiteral(tokenString, line, column);
                }
                else if (tokenString == ";")
                {
                    token = new SeparatorToken(tokenString, line, column);
                }
                else
                {
                    token = new VariableName(tokenString, line, column);
                }
                tokens.Add(token);

                column += substring.Length;
                if (substring.Contains("\n"))
                {
                    column = 0;
                    line++;
                }
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
                if (c == '\n')
                {
                    token += c;
                    splitText.Add(token);
                    token = "";
                }
                else if (char.IsWhiteSpace(c) && (i == text.Length - 1 || !char.IsWhiteSpace(text[i + 1])))
                {
                    token += c;
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
                else if (c == ';')
                {
                    if (token != "")
                    {
                        splitText.Add(token);
                    }
                    token = c.ToString();
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
