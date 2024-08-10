using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.RTree
{
    public class Rect(double minX, double minY, double maxX, double maxY)
    {
        public double MinX { get; set; } = minX;
        public double MinY { get; set; } = minY;
        public double MaxX { get; set; } = maxX;
        public double MaxY { get; set; } = maxY;
        public double Area =>
            Math.Max(MaxX - MinX, 0) * Math.Max(MaxY - MinY, 0);
        public double Margin =>
            Math.Max(MaxX - MinX, 0) + Math.Max(MaxY - MinY, 0);

        public Rect Merge(Rect other)
        {
            return new Rect(
                Math.Min(this.MinX, other.MinX),
                Math.Min(this.MinY, other.MinY),
                Math.Max(this.MaxX, other.MaxX),
                Math.Max(this.MaxY, other.MaxY));
        }
        public Rect Intersection(Rect other)
        {
            return new Rect(
                Math.Max(this.MinX, other.MinX),
                Math.Max(this.MinY, other.MinY),
                Math.Max(this.MaxX, other.MaxX),
                Math.Max(this.MaxY, other.MaxY));
        }

        public bool Contains(Rect other)
        {
            return (this.MinX <= other.MinX &&
                    this.MinY <= other.MinY &&
                    this.MaxX >= other.MaxX &&
                    this.MaxY >= other.MaxY);
        }

        public bool Intersects(Rect other)
        {
            return (this.MinX <= other.MaxX &&
                this.MinY <= other.MaxY &&
                this.MaxX >= other.MinX &&
                this.MaxY >= other.MinY);
        }

        public static Rect EmptyBounds { get; } =
            new Rect(
                minX: double.PositiveInfinity,
                minY: double.PositiveInfinity,
                maxX: double.NegativeInfinity,
                maxY: double.NegativeInfinity);

        public static Rect GetEnclosingRect(IEnumerable<ISpatial> items)
        {
            var rect = Rect.EmptyBounds;
            foreach (var data in items)
            {
                rect = rect.Merge(data.Rect);
            }
            return rect;
        }

    }
}
