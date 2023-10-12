﻿using System;
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

            foreach (var substring in splitText)
            {
                if (KeywordCollection.Contains(substring))
                {
                    var token = new KeywordToken(substring);
                    tokens.Add(token);
                }
                else if (OperatorCollection.Contains(substring))
                {
                    var token = new OperatorToken(substring);
                    tokens.Add(token);
                }
                else if (TypeCollection.Contains(substring))
                {
                    var token = new TypeToken(substring);
                    tokens.Add(token);
                }
                else if (substring[0] == '"')
                {
                    var token = new StringLiteral(substring);
                    tokens.Add(token);
                }
                else if (substring == "true" || substring == "false")
                {
                    var token = new BoolLiteral(substring);
                    tokens.Add(token);
                }
                else if (IsInteger(substring))
                {
                    var token = new IntegerLiteral(substring);
                    tokens.Add(token);
                }
                else if (IsDecimal(substring))
                {
                    var token = new DecimalLiteral(substring);
                    tokens.Add(token);
                }
                else if (substring == ";")
                {
                    var token = new SeparatorToken(substring);
                    tokens.Add(token);
                }
                else
                {
                    var token = new VariableName(substring);
                    tokens.Add(token);
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
                if (char.IsWhiteSpace(c) && token == "")
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
                else if (c == ';')
                {
                    if (token != "")
                    {
                        splitText.Add(token);
                        token = "";
                    }
                    splitText.Add(c.ToString());
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
