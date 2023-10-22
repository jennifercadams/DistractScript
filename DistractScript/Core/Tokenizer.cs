using System;
using System.Collections.Generic;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Core
{
    public static class Tokenizer
    {
        public static List<Token> GenerateTokens(string text)
        {
            var splitText = Split(text);
            var tokens = new List<Token>();
            var variables = new Dictionary<string, Type>();
            var line = 1;
            var column = 0;

            for (var i = 0; i < splitText.Count; i++)
            {
                var tokenString = splitText[i].Trim();

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
                else if (tokenString == LiteralCollection.True || tokenString == LiteralCollection.False)
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
                else if (tokenString == SeparatorCollection.EndStatement)
                {
                    token = new SeparatorToken(tokenString, line, column);
                }
                else
                {
                    var prevToken = tokens[tokens.Count - 1];
                    Type type;
                    if (prevToken is TypeToken typeToken)
                    {
                        type = typeToken.Type;
                        if (variables.ContainsKey(tokenString))
                        {
                            variables[tokenString] = type;
                        }
                        else
                        {
                            variables.Add(tokenString, type);
                        }
                    }
                    else
                    {
                        type = variables[tokenString];
                    }
                    token = new VariableName(tokenString, type, line, column);
                }
                tokens.Add(token);

                column += splitText[i].Length;
                if (splitText[i].Contains("\n"))
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
                else if (c == SeparatorCollection.EndStatement[0])
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

            if (token != "")
            {
                splitText.Add(token);
            }

            return splitText;
        }

        private static bool IsInteger(string stringValue)
        {
            return int.TryParse(stringValue, out _);
        }

        private static bool IsDecimal(string stringValue)
        {
            return decimal.TryParse(stringValue, out _);
        }
    }
}
