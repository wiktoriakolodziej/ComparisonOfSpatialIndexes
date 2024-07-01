using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.NewRTree
{
    internal class Point(double x, double y) : ISpatial
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public Rect Rect { get; set; } = new Rect(
                minX: x,
                minY: y,
                maxX: x,
                maxY: y);
    }
}
