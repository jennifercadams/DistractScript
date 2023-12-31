﻿using System;
using System.Collections.Generic;
using System.Linq;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public enum Command
    {
        None,
        DeclareEmptyVar,
        DeclareVarWithValue,
        AssignVar
    }

    public class BlockNode : TreeNode
    {
        public Keyword Keyword { get; private set; }
        public Command Command { get; private set; }
        public TypeToken TypeToken { get; private set; }
        public VariableName VariableNameToken { get; private set; }
        public LiteralToken LiteralToken { get; private set; }
        public List<Token> ExpressionTokens { get; private set; }

        public BlockNode(KeywordToken token)
            : base(token)
        {
            Keyword = token.Keyword;
        }

        public BlockNode(VariableName token)
            : base(token)
        {
            VariableNameToken = token;
        }

        public void SetCommand()
        {
            switch (Keyword)
            {
                case Keyword.DeclareVar:
                    var operatorTokens = FindChildTokensByType<OperatorToken>();
                    var hasAssign = operatorTokens.Any(t => t.Operator == Operator.Assignment);
                    if (hasAssign)
                    {
                        Command = Command.DeclareVarWithValue;
                    }
                    else
                    {
                        Command = Command.DeclareEmptyVar;
                    }
                    break;
                case Keyword.None:
                    Command = Command.AssignVar;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private List<T> FindChildTokensByType<T>() where T : Token
        {
            var tokens = new List<T>();
            foreach (var child in Children)
            {
                var tokenNode = child as TokenNode;
                if (tokenNode != null && tokenNode.Token.GetType() == typeof(T))
                {
                    tokens.Add(tokenNode.Token as T);
                }
            }

            return tokens;
        }

        public void SetTypeToken(TypeToken token)
        {
            TypeToken = token;
        }

        public void SetVariableNameToken(VariableName token)
        {
            VariableNameToken = token;
        }

        public void SetLiteralToken(LiteralToken token)
        {
            LiteralToken = token;
        }

        public void SetExpressionTokens(List<Token> tokens)
        {
            ExpressionTokens = tokens;
        }
    }
}
