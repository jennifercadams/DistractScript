using System;
using DistractScript.Data;

namespace DistractScript.Core
{
    public class Evaluator
    {
        public ParseTree ParseTree { get; private set; }

        public Evaluator(ParseTree parseTree)
        {
            ParseTree = parseTree;
        }

        public void Process()
        {

        }
    }
}
