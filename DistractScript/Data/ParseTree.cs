using System;
using System.Collections.Generic;

namespace DistractScript.Data
{
    public class ParseTree
    {
        public List<BlockNode> Nodes { get; private set; }

        public ParseTree()
        {
            Nodes = new List<BlockNode>();
        }

        public void AddNode(BlockNode node)
        {
            Nodes.Add(node);
        }
    }
}
