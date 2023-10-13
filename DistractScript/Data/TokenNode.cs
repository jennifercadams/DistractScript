using System;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class TokenNode : TreeNode
    {
        public Token Token { get; private set; }

        public TokenNode(Token token)
        {
            Token = token;
        }
    }
}
