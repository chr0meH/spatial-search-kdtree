using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace SpatialSearch;

public static class SpatialSearch
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        string FilePath = "ukraine_poi.csv";
        Console.Write("Enter Latitude, ex. 50,6065: ");
        double targetLat = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);

        Console.Write("Enter Longitude, ex. 30,4543: ");
        double targetLon = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);

        Console.Write("Enter radius to check, ex. 2: ");
        double radiusKm = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);

        var points = new List<Point>();
        var lines = File.ReadAllLines(FilePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var entries = line.Split(';').ToList();
            if (entries.Count >= 2)
            {
                points.Add(new Point(entries));
            }
        }

        Point targetPoint = new Point(targetLat, targetLon);
        var foundPoints = new List<Point>();
        var sw = new Stopwatch();
        sw.Start();
        foreach (var point in points)
        {
            double distance = Haversin.HaversinDistance(targetPoint, point);

            if (distance <= radiusKm)
            {
                foundPoints.Add(point);
            }
        }

        sw.Stop();
        Console.WriteLine("Next points were found in given area:");
        Console.WriteLine($"Found points: {foundPoints.Count}");
        foreach (var p in foundPoints)
        {
            Console.WriteLine(p.ToString());
        }

        Console.WriteLine($"\nElapsed time: {sw.Elapsed}");
        KdTree kdTree = new();
        kdTree.Build(points);
        var swTreeSearch = new Stopwatch();
        swTreeSearch.Start();
        var treeFoundPoints = kdTree.SearchInRadius(targetPoint, radiusKm);
        swTreeSearch.Stop();

        foreach (var p in treeFoundPoints)
        {
            Console.WriteLine(p.ToString());
        }

        Console.WriteLine($"Found points: {treeFoundPoints.Count}");
        Console.WriteLine($"\nKD-Tree search elapsed time: {swTreeSearch.Elapsed}");
    }
}