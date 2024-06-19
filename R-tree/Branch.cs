using ComparisonOfSpatialIndexes.Quadtree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.R_tree
{
    //TKey is boundary
    //TValue is data
    //branches don't contain values but Nodes
    internal class Branch
    { 
        public Boundary Key { get; set; }
        public Node Node { get; set; }

        public Branch(Boundary key, Node node)
        {
            Key = key;
            Node = node;
        }
    }
}
