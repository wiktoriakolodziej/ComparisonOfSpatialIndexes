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
            if (topLeft.x > botRight.x)
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
            return p.x >= topLeft.x && p.x <= botRight.x
                    && p.y >= topLeft.y && p.y <= botRight.y;
        }

        // Check if boundary contains given boundary
        public bool InBoundary(Boundary b)
        {
            if (b == null) return false;
            if (topLeft.x > b.botRight.x || b.topLeft.x > botRight.x)
                return true;
            if (botRight.y > b.topLeft.y || b.botRight.y > topLeft.y)
                return true;
            return false;
        }

        // Check if given boundary contains given point
        static public bool InBoundary(Point p, Boundary b)
        {
            return p.x >= b.topLeft.x && p.x <= b.botRight.x
                   && p.y >= b.topLeft.y && p.y <= b.botRight.y;
        }

    }
}
