using System;
using System.Collections.Generic;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class TreeNode
    {
        public Token Token { get; private set; }
        public List<TreeNode> Children { get; private set; }

        public TreeNode(Token token)
        {
            Token = token;
            Children = new List<TreeNode>();
        }

        public void AddChild(TreeNode childNode)
        {
            Children.Add(childNode);
        }
    }
}
