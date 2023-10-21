using System;
using System.Collections.Generic;
using DistractScript.Data;
using DistractScript.Exceptions;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Core
{
    public class Parser
    {
        public List<Token> Tokens { get; private set; }
        public List<string> VariableNames { get; private set; }

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
            VariableNames = new List<string>();
        }

        public ParseTree BuildParseTree()
        {
            var tree = new ParseTree();

            while (Tokens.Count > 0)
            {
                var node = ParseNextBlock();
                tree.AddNode(node);
            }

            return tree;
        }

        private BlockNode ParseNextBlock()
        {
            var block = CreateBlock();
            var blockComplete = false;
            while (!blockComplete)
            {
                var token = Tokens[0];

                if ((token is LiteralToken || token is VariableName) 
                    && Tokens[1] is OperatorToken operatorToken 
                    && operatorToken.Operator != Operator.Assignment)
                {
                    var expressionNode = ParseExpression();
                    block.AddChild(expressionNode);
                }
                else
                {
                    var tokenNode = new TokenNode(token);
                    block.AddChild(tokenNode);
                    Tokens.Remove(token);
                }

                if (token.StringValue == SeparatorCollection.EndStatement || Tokens.Count == 0)
                {
                    blockComplete = true;
                }
            }

            block.SetCommand();
            ValidateBlock(block);

            return block;
        }

        private BlockNode CreateBlock()
        {
            if (Tokens[0] is KeywordToken keywordToken)
            {
                return new BlockNode(keywordToken);
            }
            else if (Tokens[0] is VariableName variableNameToken)
            {
                return new BlockNode(variableNameToken);
            }
            else
            {
                throw new SyntaxException(Tokens[0].StringValue, Tokens[0].Line, Tokens[0].Column);
            }
        }

        private ExpressionNode ParseExpression()
        {
            var expressionNode = new ExpressionNode(Tokens[0]);
            
            var expressionComplete = false;
            while (!expressionComplete)
            {
                var token = Tokens[0];
                var tokenNode = new TokenNode(token);
                expressionNode.AddChild(tokenNode);
                Tokens.Remove(token);

                if (Tokens[0].StringValue == SeparatorCollection.EndStatement || Tokens.Count == 0)
                {
                    expressionComplete = true;
                }
            }

            return expressionNode;
        }

        private void ValidateBlock(BlockNode block)
        {
            switch (block.Command)
            {
                case Command.DeclareEmptyVar:
                    Validator.ValidateDeclareEmptyVar(block);
                    ValidateNewVar(block.VariableNameToken);
                    break;
                case Command.DeclareVarWithValue:
                    Validator.ValidateDeclareVarWithValue(block);
                    ValidateNewVar(block.VariableNameToken);
                    break;
                case Command.AssignVar:
                    Validator.ValidateAssignVar(block);
                    ValidateExistingVar(block.VariableNameToken);
                    break;
            }
        }

        private void ValidateNewVar(VariableName variableName)
        {
            if (VariableNames.Contains(variableName.StringValue))
            {
                throw new VarAlreadyExistsException(variableName.StringValue, variableName.Line, variableName.Column);
            }
            else
            {
                VariableNames.Add(variableName.StringValue);
            }
        }

        private void ValidateExistingVar(VariableName variableName)
        {
            if (!VariableNames.Contains(variableName.StringValue))
            {
                throw new VarDoesNotExistException(variableName.StringValue, variableName.Line, variableName.Column);
            }
        }
    }
}
