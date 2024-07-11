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
        public Point _position { get; set;}
        public string _data { get; set; }

        public Node(Point pos, string data)
        {
            _position = pos;
            _data = data;
        }
    }
}
