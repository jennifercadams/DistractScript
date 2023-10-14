using System;
using System.Collections.Generic;
using DistractScript.Data;
using DistractScript.Exceptions;
using DistractScript.Tokens;

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
            var keywordToken = Tokens[0] as KeywordToken;
            if (keywordToken == null)
            {
                throw new SyntaxException(Tokens[0].StringValue, Tokens[0].Line, Tokens[0].Column);
            }

            var block = new BlockNode(keywordToken.Keyword);
            var blockComplete = false;
            while (!blockComplete)
            {
                var token = Tokens[0];
                var tokenNode = new TokenNode(token);
                block.AddChild(tokenNode);
                Tokens.Remove(token);

                if (token is SeparatorToken || Tokens.Count == 0)
                {
                    blockComplete = true;
                }
            }

            block.SetCommand();

            return block;
        }
    }
}
