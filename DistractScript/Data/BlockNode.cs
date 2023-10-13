using System;
using DistractScript.Tokens;

namespace DistractScript.Data
{
    public class BlockNode : TreeNode
    {
        public Keyword Keyword { get; private set; }

        public BlockNode(Keyword keyword)
        {
            Keyword = keyword;
        }
    }
}
