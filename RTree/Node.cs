using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.RTree
{
    public class Node : ISpatial
    {
        public Rect Rect { get; set; }
        public List<ISpatial> Children { get; }
        public int Height { get; }
        public bool IsLeaf => Height == 1;
        public Node(List<ISpatial> items, int height)
        {
            this.Height = height;
            this.Children = items;
            ResetRect();
        }
        public void Add(ISpatial item)
        {
            Children.Add(item);
            Rect = Rect.Merge(item.Rect);
        }
        public void Remove(ISpatial item)
        {
            Children.Remove(item);
            ResetRect();
        }
        public void RemoveRange(int index, int count)
        {
            Children.RemoveRange(index, count);
            ResetRect();
        }
        internal void ResetRect()
        {
            Rect = Rect.GetEnclosingRect(Children); 
        }

    }
}
