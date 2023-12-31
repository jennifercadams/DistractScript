﻿using System;
using System.Collections.Generic;
using DistractScript.Data;
using DistractScript.Exceptions;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Core
{
    public static class Validator
    {
        public static void ValidateDeclareEmptyVar(BlockNode block)
        {
            var nodes = block.Children;

            ValidateKeywordToken(nodes[0].Token, Keyword.DeclareVar);
            var typeToken = ValidateTypeToken(nodes[1].Token);
            var variableNameToken = ValidateVariableNameToken(nodes[2].Token, typeToken);
            ValidateSeparatorToken(nodes[3].Token, SeparatorCollection.EndStatement);

            block.SetTypeToken(typeToken);
            block.SetVariableNameToken(variableNameToken);
        }

        public static void ValidateDeclareVarWithValue(BlockNode block)
        {
            var nodes = block.Children;

            ValidateKeywordToken(nodes[0].Token, Keyword.DeclareVar);
            var typeToken = ValidateTypeToken(nodes[1].Token);
            var variableNameToken = ValidateVariableNameToken(nodes[2].Token, typeToken);
            ValidateOperatorToken(nodes[3].Token, OperatorCollection.Assignment);
            if (nodes[4] is ExpressionNode expressionNode)
            {
                var expressionTokens = ValidateExpression(expressionNode, typeToken);
                block.SetExpressionTokens(expressionTokens);
            }
            else if (nodes[4].Token is VariableName)
            {
                var nameToken = ValidateVariableNameToken(nodes[4].Token, typeToken);
                block.SetExpressionTokens(new List<Token>() { nameToken });
            }
            else
            {
                var literalToken = ValidateLiteralToken(nodes[4].Token, typeToken);
                block.SetLiteralToken(literalToken);
            }
            ValidateSeparatorToken(nodes[5].Token, SeparatorCollection.EndStatement);

            block.SetTypeToken(typeToken);
            block.SetVariableNameToken(variableNameToken);
        }

        public static void ValidateAssignVar(BlockNode block)
        {
            var nodes = block.Children;

            var variableNameToken = ValidateVariableNameToken(nodes[0].Token);
            var typeToken = new TypeToken(variableNameToken.Type, variableNameToken.Line, variableNameToken.Column);
            ValidateOperatorToken(nodes[1].Token, OperatorCollection.Assignment);
            if (nodes[2] is ExpressionNode expressionNode)
            {
                var expressionTokens = ValidateExpression(expressionNode, typeToken);
                block.SetExpressionTokens(expressionTokens);
            }
            else if (nodes[2].Token is VariableName)
            {
                var nameToken = ValidateVariableNameToken(nodes[2].Token, typeToken);
                block.SetExpressionTokens(new List<Token>() { nameToken });
            }
            else
            {
                var literalToken = ValidateLiteralToken(nodes[2].Token, typeToken);
                block.SetLiteralToken(literalToken);
            }
            ValidateSeparatorToken(nodes[3].Token, SeparatorCollection.EndStatement);

            block.SetTypeToken(typeToken);
            block.SetVariableNameToken(variableNameToken);
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

        private static VariableName ValidateVariableNameToken(Token token, TypeToken typeToken)
        {
            if (!(token is VariableName variableName))
            {
                var actual = token.StringValue;
                var expected = "variable name";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }
            else if (variableName.Type != typeToken.Type)
            {
                var actual = variableName.Type;
                var expected = typeToken.Type;
                throw new TypeException(actual, expected, token.Line, token.Column);
            }

            return variableName;
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
            if (!(token is OperatorToken operatorToken) || operatorToken.StringValue != operatorString)
            {
                var actual = token.StringValue;
                var expected = operatorString;
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return operatorToken;
        }

        private static OperatorToken ValidateOperatorToken(Token token)
        {
            if (!(token is OperatorToken operatorToken))
            {
                var actual = token.StringValue;
                var expected = "operator";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }

            return operatorToken;
        }

        private static LiteralToken ValidateLiteralToken(Token token, TypeToken typeToken)
        {
            if (!(token is LiteralToken literalToken))
            {
                var actual = token.StringValue;
                var expected = $"{typeToken.StringValue} literal";
                throw new SyntaxException(actual, expected, token.Line, token.Column);
            }
            else if (literalToken.Type != typeToken.Type)
            {
                var actual = literalToken.Type;
                var expected = typeToken.Type;
                throw new TypeException(actual, expected, token.Line, token.Column);
            }

            return literalToken;
        }

        private static List<Token> ValidateExpression(ExpressionNode expressionNode, TypeToken typeToken)
        {
            var expressionTokens = new List<Token>();

            var nodes = expressionNode.Children;
            for (var i = 0; i < nodes.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (nodes[i].Token is VariableName variableName)
                    {
                        var variableNameToken = ValidateVariableNameToken(variableName, typeToken);
                        expressionTokens.Add(variableNameToken);
                    }
                    else
                    {
                        var literalToken = ValidateLiteralToken(nodes[i].Token, typeToken);
                        expressionTokens.Add(literalToken);
                    }
                }
                else
                {
                    var operatorToken = ValidateOperatorToken(nodes[i].Token);
                    expressionTokens.Add(operatorToken);
                }
            }

            for (var i = 0; i < expressionTokens.Count; i++)
            {
                if (!(expressionTokens[i] is OperatorToken operatorToken))
                    continue;

                if (typeToken.Type == typeof(bool))
                {
                    throw new OperatorException(operatorToken.StringValue, typeToken.StringValue);
                }
                else if (typeToken.Type == typeof(string) || operatorToken.Operator != Operator.Sum)
                {
                    throw new OperatorException(operatorToken.StringValue, typeToken.StringValue);
                }
            }

            return expressionTokens;
        }
    }
}
