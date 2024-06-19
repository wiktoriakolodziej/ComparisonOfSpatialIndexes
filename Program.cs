using ComparisonOfSpatialIndexes.Quadtree;

namespace ComparisonOfSpatialIndexes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Quad center = new Quad(new Point(8, 8), new Point(0, 0));
            Node a = new Node(new Point(1, 1), 1);
            Node b = new Node(new Point(2, 5), 2);
            Node c = new Node(new Point(7, 6), 3);
            center.Insert(a);
            center.Insert(b);
            center.Insert(c);

            Console.WriteLine("Nodes in (0,0) x (8,8) rectangle: ");
            center.AreaSearch(new Boundary(new Point(8, 8), new Point(0, 0))).ForEach(x => Console.WriteLine("({0}, {1}) -> {2}", x.position.x, x.position.y, x.data));
            Console.WriteLine();
            Console.WriteLine("Node a: " + center.Search(new Point(1, 1)).data);
            Console.WriteLine("Node b: " + center.Search(new Point(2, 5)).data);
            Console.WriteLine("Node c: " + center.Search(new Point(7, 6)).data);
            Console.WriteLine("Non-existing node: " + center.Search(new Point(5, 5)).data);
        }
    }
}
