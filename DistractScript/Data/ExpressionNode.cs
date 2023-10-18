using System;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class ExpressionNode : TreeNode
    {
        public ExpressionNode(Token token)
            : base(token) { }
    }
}
