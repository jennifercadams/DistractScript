using System;
using System.Collections.Generic;
using DistractScript.Data;
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



            return tree;
        }
    }
}
