using ComparisonOfSpatialIndexes.Quadtree;
using ComparisonOfSpatialIndexes.RTree;
using PointR = ComparisonOfSpatialIndexes.RTree.Point;
using PointQ = ComparisonOfSpatialIndexes.Quadtree.Point;
using NodeR = ComparisonOfSpatialIndexes.RTree.Node;
using NodeQ = ComparisonOfSpatialIndexes.Quadtree.Node;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ComparisonOfSpatialIndexes
{
    internal class Program
    {
        static void Main()
        {
           

            List<string> filePaths = [];
            filePaths.Add("Europe.txt");
            filePaths.Add("Asia.txt");
            filePaths.Add("North-America.txt");
            filePaths.Add("South-America.txt");
            filePaths.Add("Oceania.txt");
            filePaths.Add("Africa.txt");

            var dict = new Dictionary<Tuple<double,double>, string>();

            var pattern = @"^(\S+)\s+(\d+\.\d+)\s+(\d+\.\d+)$";

            var regex = new Regex(pattern);
            foreach (var file in filePaths)
            {
                try
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        Match match = regex.Match(line);
                        if (!match.Success)
                            continue;
                        var name = match.Groups[1].Value;
                        var coordX = double.Parse(match.Groups[2].Value);
                        var coordY = double.Parse(match.Groups[3].Value);
                        dict.TryAdd(new(coordX, coordY), name);
                    }
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine($"Couldn't find file {ex.FileName}");
                    return;
                }
            }

            Stopwatch stopwatch = new Stopwatch();

            Quad quadTree = new Quad(new PointQ(179.97, 71.59), new PointQ(-179.97, -54.93));
            var quadNodes = new List<NodeQ>();
            quadNodes.AddRange(dict.Select(element => new NodeQ(new PointQ(element.Key.Item1, element.Key.Item2), element.Value)));

            stopwatch.Start();
            foreach (var node in quadNodes)
            {
                quadTree.Insert(node);
            }
            stopwatch.Stop();

            Console.WriteLine($"QuadTree: time of inserting: {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Reset();
            stopwatch.Start();
            var count = quadTree.AreaSearch(new Boundary(new PointQ(179.97, 71.59), new PointQ(-179.97, -54.93))).Count;
            stopwatch.Stop();

            Console.WriteLine($"QuadTree: elements in whole area: {count}");
            Console.WriteLine($"QuadTree: time of searching: {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Reset();
            stopwatch.Start();
            count = quadTree.AreaSearch(new Boundary(new PointQ(50, 20), new PointQ(-50, -20))).Count;
            stopwatch.Stop();

            Console.WriteLine($"QuadTree:  elements in (50, 20) x (-50, -20) rectangle: {count}");
            Console.WriteLine($"QuadTree: time of searching: {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine();

            var rTRee = new RTree<PointR>();
            var rPoints = new List<PointR>();
            rPoints.AddRange(dict.Select(element => new PointR(element.Key.Item1, element.Key.Item2, element.Value)));
           
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var point in rPoints)
            {
                rTRee.Insert(point);
            }
            stopwatch.Stop();
            Console.WriteLine($"RTree: time of inserting: {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Reset();
            stopwatch.Start();
            count = rTRee.Search().Count;
            stopwatch.Stop();
            Console.WriteLine($"RTree: elements in whole area: {count}");
            Console.WriteLine($"RTree: time of searching: {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Reset();
            stopwatch.Start();
            count = rTRee.Search(new Rect(-50, -20, 50 ,20)).Count;
            stopwatch.Stop();
            Console.WriteLine($"RTree: elements in (50, 20) x (-50, -20) rectangle: {count}");
            Console.WriteLine($"RTree: time of searching: {stopwatch.Elapsed.TotalMilliseconds} ms");


        }
    }
}
