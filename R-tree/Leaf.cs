using ComparisonOfSpatialIndexes.Quadtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.R_tree
{
    //Leafs contain Values
    internal class Leaf
    {
        public Boundary Key { get; set; }
        public int Value { get; set; }
    }
    
}
