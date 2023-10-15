using System;
using System.Collections.Generic;
using System.Linq;
using DistractScript.Core;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class BlockNode : TreeNode
    {
        public Keyword Keyword { get; private set; }
        public Command Command { get; private set; }

        public BlockNode(KeywordToken token)
            : base(token)
        {
            Keyword = token.Keyword;
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
    }
}
