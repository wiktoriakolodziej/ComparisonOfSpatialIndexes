using ComparisonOfSpatialIndexes.Quadtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.R_tree
{
    //Node consists of Entries (Leafes or Branches) and level in the tree (0 = leaf)
    internal class Node
    {
        public int Level { get; set; }
        private List<Entry> entries = [];

        public Node(int level)
        {
            Level = level;
        }
        public void AddLeaf(int level, Leaf item, Node node)
        {
            var boundary = item.Key;
        }
    }
}
