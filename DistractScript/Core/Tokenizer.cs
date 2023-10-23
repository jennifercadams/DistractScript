using System;
using System.Collections.Generic;
using System.Linq;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Core
{
    public class Tokenizer
    {
        private string Text { get; set; }
        private List<string> SplitText { get; set; }
        private List<Token> Tokens { get; set; }
        private Dictionary<string, Type> Variables { get; set; }
        private int Line { get; set; }
        private int Column { get; set; }

        public Tokenizer(string text)
        {
            Text = text;
            SplitText = new List<string>();
            Tokens = new List<Token>();
            Variables = new Dictionary<string, Type>();
            Line = 1;
            Column = 1;
        }

        public List<Token> GenerateTokens()
        {
            SplitText = Split(Text);

            for (var i = 0; i < SplitText.Count; i++)
            {
                var substring = SplitText[i];
                var tokenString = SplitText[i].Trim();

                Token token;
                if (tokenString.Length == 0)
                {
                    if (substring.Contains("\n"))
                    {
                        HandleNewLine(substring);
                    }
                    continue;
                }
                if (KeywordCollection.Contains(tokenString))
                {
                    token = new KeywordToken(tokenString, Line, Column);
                }
                else if (OperatorCollection.Contains(tokenString))
                {
                    token = new OperatorToken(tokenString, Line, Column);
                }
                else if (TypeCollection.Contains(tokenString))
                {
                    token = new TypeToken(tokenString, Line, Column);
                }
                else if (tokenString[0] == '"')
                {
                    token = new StringLiteral(tokenString, Line, Column);
                }
                else if (tokenString == LiteralCollection.True || tokenString == LiteralCollection.False)
                {
                    token = new BoolLiteral(tokenString, Line, Column);
                }
                else if (IsInteger(tokenString))
                {
                    token = new IntegerLiteral(tokenString, Line, Column);
                }
                else if (IsDecimal(tokenString))
                {
                    token = new DecimalLiteral(tokenString, Line, Column);
                }
                else if (tokenString == SeparatorCollection.EndStatement)
                {
                    token = new SeparatorToken(tokenString, Line, Column);
                }
                else
                {
                    token = CreateVariableNameToken(tokenString);
                }
                Tokens.Add(token);

                Column += substring.Length;
                if (substring.Contains("\n"))
                {
                    HandleNewLine(substring);
                }
            }

            return Tokens;
        }

        private List<string> Split(string text)
        {
            var splitText = new List<string>();

            string token = "";
            for (var i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsWhiteSpace(c) && (i == text.Length - 1 || !char.IsWhiteSpace(text[i + 1])))
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

        private void HandleNewLine(string substring)
        {
            var newLines = substring.Count(c => c == '\n');
            Column = 1;
            Line += newLines;
        }

        private bool IsInteger(string stringValue)
        {
            return int.TryParse(stringValue, out _);
        }

        private bool IsDecimal(string stringValue)
        {
            return decimal.TryParse(stringValue, out _);
        }

        private VariableName CreateVariableNameToken(string tokenString)
        {
            var prevToken = Tokens[Tokens.Count - 1];
            Type type;
            if (prevToken is TypeToken typeToken)
            {
                type = typeToken.Type;
                if (Variables.ContainsKey(tokenString))
                {
                    Variables[tokenString] = type;
                }
                else
                {
                    Variables.Add(tokenString, type);
                }
            }
            else
            {
                type = Variables[tokenString];
            }
            return new VariableName(tokenString, type, Line, Column);
        }
    }
}
