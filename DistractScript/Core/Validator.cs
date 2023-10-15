using System;
using DistractScript.Data;
using DistractScript.Exceptions;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Core
{
    public enum Command
    {
        DeclareEmptyVar,
        DeclareVarWithValue
    }

    public static class Validator
    {
        public static void ValidateDeclareEmptyVar(BlockNode block)
        {
            var nodes = block.Children;

            ValidateKeywordToken(nodes[0].Token, Keyword.DeclareVar);
            ValidateTypeToken(nodes[1].Token);
            ValidateVariableNameToken(nodes[2].Token);
            ValidateSeparatorToken(nodes[3].Token, SeparatorCollection.EndStatement);
        }

        public static void ValidateDeclareVarWithValue(BlockNode block)
        {
            var nodes = block.Children;

            ValidateKeywordToken(nodes[0].Token, Keyword.DeclareVar);
            var typeToken = ValidateTypeToken(nodes[1].Token);
            var variableNameToken = ValidateVariableNameToken(nodes[2].Token);
            ValidateOperatorToken(nodes[3].Token, OperatorCollection.Assignment);
            ValidateLiteralToken(nodes[4].Token, typeToken, variableNameToken.StringValue);
            ValidateSeparatorToken(nodes[5].Token, SeparatorCollection.EndStatement);
        }

        private static KeywordToken ValidateKeywordToken(Token token, Keyword keyword)
        {
            if (!(token is KeywordToken keywordToken) || keywordToken.Keyword != keyword)
            {
                var actual = token.StringValue;
                throw new SyntaxException(actual, token.Line, token.Column);
            }

            return keywordToken;
        }

        private static TypeToken ValidateTypeToken(Token token)
        {
            if (!(token is TypeToken typeToken))
            {
                var actual = token.StringValue;
                var expected = "type";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return typeToken;
        }

        private static VariableName ValidateVariableNameToken(Token token)
        {
            if (!(token is VariableName variableName))
            {
                var actual = token.StringValue;
                var expected = "variable name";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return variableName;
        }

        private static SeparatorToken ValidateSeparatorToken(Token token, string separator)
        {
            if (!(token is SeparatorToken separatorToken) || token.StringValue != separator)
            {
                var actual = token.StringValue;
                var expected = separator;
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return separatorToken;
        }

        private static OperatorToken ValidateOperatorToken(Token token, string operatorString)
        {
            if (!(token is OperatorToken operatorToken) || operatorToken.Operator != Operator.Assignment)
            {
                var actual = token.StringValue;
                var expected = operatorString;
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return operatorToken;
        }

        private static LiteralToken ValidateLiteralToken(Token token, TypeToken typeToken, string variableName)
        {
            if (!(token is LiteralToken literalToken))
            {
                var actual = token.StringValue;
                var expected = $"{typeToken.StringValue} literal";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }
            else if (literalToken.Type != typeToken.Type)
            {
                var expected = typeToken.Type.Name;
                var actual = literalToken.Type.Name;
                throw new TypeException(variableName, expected, actual, token.Line, token.Column);
            }

            return literalToken;
        }
    }
}
