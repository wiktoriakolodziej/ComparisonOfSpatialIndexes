using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.Quadtree
{
    public class Boundary
    {
        public Point topLeft { get; }
        public Point botRight { get; }

        public Boundary(Point topLeft, Point botRight)
        {
            if (topLeft._x > botRight._x)
            {
                this.topLeft = botRight;
                this.botRight = topLeft;

            }
            else
            {
                this.topLeft = topLeft;
                this.botRight = botRight;
            }
        }

        public Boundary(Point point) : this(point, point) { }


        // Check if boundary contains given point
        public bool InBoundary(Point p)
        {
            return p._x >= topLeft._x && p._x <= botRight._x
                    && p._y >= topLeft._y && p._y <= botRight._y;
        }

        // Check if boundary contains given boundary
        public bool InBoundary(Boundary b)
        {
            if (b == null) return false;
            if (topLeft._x > b.botRight._x || b.topLeft._x > botRight._x)
                return true;
            if (botRight._y > b.topLeft._y || b.botRight._y > topLeft._y)
                return true;
            return false;
        }

        // Check if given boundary contains given point
        static public bool InBoundary(Point p, Boundary b)
        {
            return p._x >= b.topLeft._x && p._x <= b.botRight._x
                   && p._y >= b.topLeft._y && p._y <= b.botRight._y;
        }

    }
}
