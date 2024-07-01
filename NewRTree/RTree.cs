using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.NewRTree
{
    public partial class RTree<T> where T : ISpatial
    {
        private int maxEntries;
        private int minEntries = 3;
        private readonly IEqualityComparer<T> comparer;
        public int Count { get; private set; }

        public Node Root { get; private set; }
        public Rect Rect { get; set; }
        public RTree()
            : this(EqualityComparer<T>.Default) { }
        public RTree(int maxEntries)
            : this(EqualityComparer<T>.Default, maxEntries) { }

        public RTree(IEqualityComparer<T> comparer, int maxEntries = 9)
        {
            this.comparer = comparer;
            this.maxEntries = maxEntries;
            
            this.Clear();
        }
        public void Clear()
        {
            this.Root = new Node(new List<ISpatial>(), 1);
            this.Count = 0;
        }
        public IReadOnlyList<T> Search() =>
            GetAllChildren(new List<T>(), this.Root);

        public IReadOnlyList<T> Search(Rect boundingBox) =>
            DoSearch(boundingBox);
        public void Insert(T item)
        {
            Insert(item, this.Root.Height);
            this.Count++;
        }
        public bool Delete(T item) =>
            DoDelete(Root, item);

        private bool DoDelete(Node node, T item)
        {
            if (!node.Rect.Contains(item.Rect))
                return false;

            if (node.IsLeaf)
            {
                var cnt = node.Children.RemoveAll(i => comparer.Equals((T)i, item));
                if (cnt != 0)
                {
                    Count -= cnt;
                    node.ResetRect();
                    return true;
                }
                else
                    return false;
            }

            var flag = false;
            foreach (Node n in node.Children)
            {
                flag |= DoDelete(n, item);
            }

            if (flag)
                node.ResetRect();
            return flag;
        }

    }
}
