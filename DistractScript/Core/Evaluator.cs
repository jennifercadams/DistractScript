using System;
using DistractScript.Data;
using DistractScript.Exceptions;
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
            var name = block.VariableNameToken.StringValue;
            GlobalVariables.AddEmpty(name, type);
        }

        private void EvaluateDeclareVarWithValue(BlockNode block)
        {
            var name = block.VariableNameToken.StringValue;
            var literalToken = block.LiteralToken;
            GlobalVariables.AddWithValue(name, literalToken);
        }

        private void EvaluateAssignVar(BlockNode block)
        {
            var name = block.VariableNameToken.StringValue;
            var literalToken = block.LiteralToken;
            GlobalVariables.AssignValue(name, literalToken);
        }
    }
}
