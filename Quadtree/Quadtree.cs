namespace ComparisonOfSpatialIndexes.Quadtree
{
    class Quad
    {
        // Hold details of the boundary of this node
        private readonly Boundary boundary;

        // Contains details of nodes
        private readonly List<Node> nodes = [];

        // Children of this tree
        private Quad? topLeftTree = null;
        private Quad? topRightTree = null;
        private Quad? botLeftTree = null;
        private Quad? botRightTree = null;

        //to construct a Quad provide Point of left upper and right lower corner (order doesn't matter)
        public Quad() : this(new Point(0, 0), new Point(0, 0))
        {
        }
        public Quad(Point topL, Point botR)
        {
            boundary = new Boundary(topL, botR);
        }


        public bool Insert(Node node)
        {
            //if node equals null
            //if (node.Equals(default(Node)))
            //    return false;

            //if node is in the boundaries of quadtree
            if (!boundary.InBoundary(node._position))
                return false;

            //if tree wasn't subdivided yet and hasn't reached its max capacity
            if (nodes.Count < 7 && topLeftTree == null)
            {
                nodes.Add(node);
                return true;
            }
            //if tree has reahed its max capacity but hasn't been subdivided yet
            if (topLeftTree == null) Subdivide();

            //try to insert node into every child quadtrees
            //(they would return false if node isn't in their boundaries)
            if (topLeftTree!.Insert(node)) return true;
            if (topRightTree!.Insert(node)) return true;
            if (botLeftTree!.Insert(node)) return true;
            if (botRightTree!.Insert(node)) return true;

            //if we are here that means something went wrong
            return false;

        }

        //divide current tree into 4 smaller trees and pass all nodes of this tree to its children
        private void Subdivide()
        {
            double stepX = double.Abs(boundary.botRight._x - boundary.topLeft._x) / 2;
            double stepY = double.Abs(boundary.topLeft._y - boundary.botRight._y) / 2;
            double biggY = boundary.topLeft._y > boundary.botRight._y ? boundary.topLeft._y : boundary.botRight._y;
            double biggX =  boundary.topLeft._x > boundary.botRight._x ?  boundary.topLeft._x : boundary.botRight._x;

            topLeftTree = new Quad(
                          new Point(boundary.topLeft._x, boundary.topLeft._y),
                          new Point(biggX - stepX, biggY- stepY));
            topRightTree = new Quad(
                       new Point(biggX - stepX, boundary.topLeft._y),
                       new Point(boundary.botRight._x, biggY - stepY));
            botLeftTree = new Quad(
                       new Point(boundary.topLeft._x, biggY - stepY),
                       new Point(biggX - stepX, boundary.botRight._y));
            botRightTree = new Quad(
                       new Point(biggX - stepX, biggY - stepY),
                       new Point(boundary.botRight._x, boundary.botRight._y));
            foreach (var element in nodes)
            {
                Insert(element);
            }
            nodes.Clear();
        }

        //specific example of AreaSearch()
        public Node Search(Point p)
        {
            return AreaSearch(new Boundary(p)).FirstOrDefault();
        }

        public List<Node> AreaSearch(Boundary b)
        {
            var result = new List<Node>();
            //if current boundary doesn't contain given boundary return empty list
            if (!boundary.InBoundary(b)) return result;

            //if current tree hasn't been subdivided yet search through its nodes
            //and add those in given boundary to the result
            if (topLeftTree == null)
            {
                foreach (var node in nodes)
                {
                    if (Boundary.InBoundary(node._position, b)) result.Add(node);
                }
                return result;
            }

            //if current tree has been subdivided
            //add result of searching from all its children to the result
            result.AddRange(topLeftTree!.AreaSearch(b));
            result.AddRange(topRightTree!.AreaSearch(b));
            result.AddRange(botLeftTree!.AreaSearch(b));
            result.AddRange(botRightTree!.AreaSearch(b));
            return result;

        }

       
    }
}
