using System;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class TokenNode : TreeNode
    {
        public TokenNode(Token token)
            : base(token) { }
    }
}
