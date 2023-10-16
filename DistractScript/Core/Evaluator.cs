using System;
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
                default:
                    throw new NotImplementedException();
            }
        }

        private void EvaluateDeclareEmptyVar(BlockNode block)
        {
            var type = block.TypeToken.Type;
            var name = block.VariableNameToken.StringValue;

            if (type == typeof(string))
            {
                var newVariable = new Variable<string>(name);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(bool))
            {
                var newVariable = new Variable<bool>(name);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(int))
            {
                var newVariable = new Variable<int>(name);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(decimal))
            {
                var newVariable = new Variable<decimal>(name);
                GlobalVariables.Add(newVariable);
            }
        }

        private void EvaluateDeclareVarWithValue(BlockNode block)
        {
            var type = block.TypeToken.Type;
            var name = block.VariableNameToken.StringValue;
            var literalToken = block.LiteralToken;

            if (type == typeof(string))
            {
                var value = (literalToken as StringLiteral).Value;
                var newVariable = new Variable<string>(name, value);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(bool))
            {
                var value = (literalToken as BoolLiteral).Value;
                var newVariable = new Variable<bool>(name, value);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(int))
            {
                var value = (literalToken as IntegerLiteral).Value;
                var newVariable = new Variable<int>(name, value);
                GlobalVariables.Add(newVariable);
            }
            else if (type == typeof(decimal))
            {
                var value = (literalToken as DecimalLiteral).Value;
                var newVariable = new Variable<decimal>(name, value);
                GlobalVariables.Add(newVariable);
            }
        }
    }
}
