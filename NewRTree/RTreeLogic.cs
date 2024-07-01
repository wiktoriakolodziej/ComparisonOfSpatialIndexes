using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfSpatialIndexes.NewRTree
{
    public partial class RTree<T> where T : ISpatial
    {
        private List<T> DoSearch(Rect boundingBox)
        {
            //check if given area intersects with area of the RTree
            if (!Root.Rect.Intersects(boundingBox))
                return new List<T>();

            var intersections = new List<T>();
            var queue = new Queue<Node>();
            //add root node to the queue 
            queue.Enqueue(Root);

            while (queue.Count != 0)
            {
                var item = queue.Dequeue();

                //check if node is leaf
                if (item.IsLeaf)
                {
                    //add each element that intersects with the given area to the result
                    foreach (var i in item.Children)
                    {
                        if (i.Rect.Intersects(boundingBox))
                            intersections.Add((T)i);
                    }
                }
                //node is not leaf
                else
                {
                    //add each element that intersects with the given area to the queue
                    foreach (var i in item.Children)
                    {
                        if (i.Rect.Intersects(boundingBox))
                            queue.Enqueue((Node)i);
                    }
                }
            }

            return intersections;
        }
        private void Insert(ISpatial data, int depth)
        {
            //"path" of nodes to insert the data
            var path = FindCoveringArea(data.Rect, depth);

            //the last node with leafs as children
            var insertNode = path[path.Count - 1];
            insertNode.Add(data);

            while (--depth >= 0)
            {
                //check if current node has more than max entries (leafs or nodes)
                if (path[depth].Children.Count > maxEntries)
                {
                    //split path[depth] to another newNode node (erase some children from the pth[depth] and put them in newNode)
                    var newNode = SplitNode(path[depth]);
                    //if the current node is root then we must create new root and put newNode and path[depth](which was the last root node) inside it
                    if (depth == 0)
                        SplitRoot(newNode);
                    //else add newNode to the parent node of the path[depth]
                    else
                        path[depth - 1].Add(newNode);
                }
                else
                    path[depth].ResetRect();
            }
        }

        //find List of nodes that are path to the best place to insert new value (their area expands least)
        private List<Node> FindCoveringArea(Rect area, int depth)
        {
            var path = new List<Node>();
            var node = this.Root;

            while (true)
            {
                //add nodes to the path until you reach the leafs
                path.Add(node);
                if (node.IsLeaf || path.Count == depth) return path;

                //first node in the parent node's children
                var next = node.Children[0];
                var nextArea = next.Rect.Merge(area).Area;

                foreach (var i in node.Children)
                {
                    var newArea = i.Rect.Merge(area).Area;
                    //if other child's extended area is bigger 
                    if (newArea > nextArea)
                        continue;

                    //if other child's extended area is equal, but it's area before extension was bigger than chosen one's 
                    if (newArea == nextArea&& i.Rect.Area >= next.Rect.Area)
                        continue;

                    //set new chosen node
                    next = i;
                    nextArea = newArea;
                }

                node = (next as Node)!;
            }
        }

        private void SplitRoot(Node newNode) =>
            this.Root = new Node([this.Root, newNode], this.Root.Height + 1);

        private Node SplitNode(Node node)
        {
            SortChildren(node);

            var splitPoint = GetBestSplitIndex(node.Children);
            var newChildren = node.Children.Skip(splitPoint).ToList();
            node.RemoveRange(splitPoint, node.Children.Count - splitPoint);
            return new Node(newChildren, node.Height);
        }
        private void SortChildren(Node node)
        {
            node.Children.Sort(s_compareMinX);
            var splitsByX = GetPotentialSplitMargins(node.Children);
            node.Children.Sort(s_compareMinY);
            var splitsByY = GetPotentialSplitMargins(node.Children);

            if (splitsByX < splitsByY)
                node.Children.Sort(s_compareMinX);
        }

        private double GetPotentialSplitMargins(List<ISpatial> children) =>
            GetPotentialEnclosingMargins(children) +
            GetPotentialEnclosingMargins(children.AsEnumerable().Reverse().ToList());

        private double GetPotentialEnclosingMargins(List<ISpatial> children)
        {
            var rect = Rect.EmptyBounds;
            var i = 0;
            for (; i < minEntries; i++)
            {
                rect = rect.Merge(children[i].Rect);
            }

            var totalMargin = rect.Margin;
            for (; i < children.Count - minEntries; i++)
            {
                rect = rect.Merge(children[i].Rect);
                totalMargin += rect.Margin;
            }

            return totalMargin;
        }

        //select an index that splits the children in 2 groups that bounding rectangles overlap least 
        private int GetBestSplitIndex(List<ISpatial> children)
        {
            return Enumerable.Range(minEntries, children.Count - minEntries)
                .Select(i =>
                {
                    var leftEnvelope = Rect.GetEnclosingRect(children.Take(i));
                    var rightEnvelope = Rect.GetEnclosingRect(children.Skip(i));

                    var overlap = leftEnvelope.Intersection(rightEnvelope).Area;
                    var totalArea = leftEnvelope.Area + rightEnvelope.Area;
                    return new { i, overlap, totalArea };
                })
                .OrderBy(x => x.overlap)
                .ThenBy(x => x.totalArea)
                .Select(x => x.i)
                .First();
        }

        private static readonly IComparer<ISpatial> s_compareMinX =
            Comparer<ISpatial>.Create((x, y) => Comparer<double>.Default.Compare(x.Rect.MinX, y.Rect.MinX));
        private static readonly IComparer<ISpatial> s_compareMinY =
            Comparer<ISpatial>.Create((x, y) => Comparer<double>.Default.Compare(x.Rect.MinY, y.Rect.MinY));

        //gets the rectangle containing all given items
       

        private List<T> GetAllChildren(List<T> list, Node n)
        {
            if (n.IsLeaf)
            {
                list.AddRange(
                    n.Children.Cast<T>());
            }
            else
            {
                foreach (var node in n.Children.Cast<Node>())
                    GetAllChildren(list, node);
            }

            return list;
        }
    }

}
