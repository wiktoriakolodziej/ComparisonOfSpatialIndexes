using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.RTree
{
    internal class Point(double x, double y, string data) : ISpatial
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public string Data { get; set; } = data;
        public Rect Rect { get; set; } = new Rect(
                minX: x,
                minY: y,
                maxX: x,
                maxY: y);
    }
}
