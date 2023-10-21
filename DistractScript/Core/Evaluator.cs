using System;
using System.Collections.Generic;
using System.Data;
using DistractScript.Data;
using DistractScript.Tokens;
using DistractScript.Variables;

namespace DistractScript.Core
{
    public class Evaluator
    {
        public ParseTree ParseTree { get; private set; }
        private VariableCollection GlobalVariables { get; set; }

        public Evaluator(ParseTree parseTree)
        {
            ParseTree = parseTree;
            GlobalVariables = new VariableCollection();
        }

        public void Process()
        {
            foreach(var block in ParseTree.Nodes)
            {
                EvaluateBlock(block);
            }
        }

        private void EvaluateBlock(BlockNode block)
        {
            switch (block.Command)
            {
                case Command.DeclareEmptyVar:
                    EvaluateDeclareEmptyVar(block);
                    break;
                case Command.DeclareVarWithValue:
                    EvaluateDeclareVarWithValue(block);
                    break;
                case Command.AssignVar:
                    EvaluateAssignVar(block);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void EvaluateDeclareEmptyVar(BlockNode block)
        {
            var type = block.TypeToken.Type;
            var nameToken = block.VariableNameToken;
            GlobalVariables.AddEmpty(nameToken, type);
        }

        private void EvaluateDeclareVarWithValue(BlockNode block)
        {
            var type = block.TypeToken.Type;
            var nameToken = block.VariableNameToken;
            var literalToken = block.LiteralToken;
            if (literalToken == null)
            {
                literalToken = EvaluateExpression(block.ExpressionTokens, type);
            }
            GlobalVariables.AddWithValue(nameToken, literalToken);
        }

        private void EvaluateAssignVar(BlockNode block)
        {
            var type = block.TypeToken.Type;
            var nameToken = block.VariableNameToken;
            var literalToken = block.LiteralToken;
            if (literalToken == null)
            {
                literalToken = EvaluateExpression(block.ExpressionTokens, type);
            }
            GlobalVariables.AssignValue(nameToken, literalToken);
        }

        private LiteralToken EvaluateExpression(List<Token> expressionTokens, Type type)
        {
            var tokens = ReplaceVariables(expressionTokens);

            if (type == typeof(string))
            {
                return EvaluateStringConcatenation(tokens);
            }
            else if (type == typeof(int))
            {
                return EvaluateIntegerExpression(tokens);
            }
            else if (type == typeof(decimal))
            {
                return EvaluateDecimalExpression(tokens);
            }
            else
            {
                return null;
            }
        }

        private List<Token> ReplaceVariables(List<Token> expressionTokens)
        {
            var convertedTokens = new List<Token>();
            foreach (var token in expressionTokens)
            {
                if (token is VariableName nameToken)
                {
                    var literalToken = GlobalVariables.FindVariable(nameToken);
                    convertedTokens.Add(literalToken);
                }
                else
                {
                    convertedTokens.Add(token);
                }
            }

            return convertedTokens;
        }

        private StringLiteral EvaluateStringConcatenation(List<Token> expressionTokens)
        {
            var stringValue = "";
            var stringLiteralTokens = expressionTokens.FindAll(t => t is StringLiteral);
            foreach (var token in stringLiteralTokens)
            {
                stringValue += (token as StringLiteral).Value;
            }

            var value = $"\"{stringValue}\"";
            var line = expressionTokens[0].Line;
            var column = expressionTokens[0].Column;
            return new StringLiteral(value, line, column);
        }

        private IntegerLiteral EvaluateIntegerExpression(List<Token> expressionTokens)
        {
            var expressionValue = EvaluateArithmeticExpression(expressionTokens);
            var intValue = Convert.ToInt32(expressionValue);

            var value = intValue.ToString();
            var line = expressionTokens[0].Line;
            var column = expressionTokens[0].Column;
            return new IntegerLiteral(value, line, column);
        }

        private DecimalLiteral EvaluateDecimalExpression(List<Token> expressionTokens)
        {
            var expressionValue = EvaluateArithmeticExpression(expressionTokens);
            var decimalValue = Convert.ToDecimal(expressionValue);

            var value = decimalValue.ToString();
            var line = expressionTokens[0].Line;
            var column = expressionTokens[0].Column;
            return new DecimalLiteral(value, line, column);
        }

        private object EvaluateArithmeticExpression(List<Token> expressionTokens)
        {
            var stringList = new List<string>();
            foreach (var token in expressionTokens)
            {
                stringList.Add(token.StringValue);
            }
            var expressionString = String.Join(" ", stringList);

            var dataTable = new DataTable();
            return dataTable.Compute(expressionString, "");
        }
    }
}
