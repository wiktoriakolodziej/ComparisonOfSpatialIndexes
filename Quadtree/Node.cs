using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.Quadtree
{
    // The objects that we want stored in the quadtree
    struct Node
    {
        public Point position;
        public int data;

        public Node(Point _pos, int _data)
        {
            position = _pos;
            data = _data;
        }
    }
}
