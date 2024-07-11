using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.Quadtree
{
    public struct Point
    {
        public double _x { get; set; }
        public double _y { get; set; }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}
