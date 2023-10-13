using System;
using System.Collections.Generic;

namespace DistractScript.Data
{
    public class TreeNode
    {
        public List<TreeNode> Children { get; private set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }

        public void AddChild(TreeNode childNode)
        {
            Children.Add(childNode);
        }
    }
}
