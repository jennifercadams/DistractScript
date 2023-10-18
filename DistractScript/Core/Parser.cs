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

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
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
                var tokenNode = new TokenNode(token);
                block.AddChild(tokenNode);
                Tokens.Remove(token);

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

        private void ValidateBlock(BlockNode block)
        {
            switch (block.Command)
            {
                case Command.DeclareEmptyVar:
                    Validator.ValidateDeclareEmptyVar(block);
                    break;
                case Command.DeclareVarWithValue:
                    Validator.ValidateDeclareVarWithValue(block);
                    break;
                case Command.AssignVar:
                    Validator.ValidateAssignVar(block);
                    break;
            }
        }
    }
}
