using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialSearch
{
    public class KdNode
    {
        public Box Box;
        public KdNode? Left;
        public KdNode? Right;
        public List<Point>? Points; 

        public bool IsLeaf => Points != null;

        public KdNode(Box box)
        {
            Box = box;
        }
    }
    public class Box
    {
        public readonly double MinLat, MaxLat, MinLon, MaxLon;

        public Box(List<Point> points)
        {
            MinLat = points.Min(p => p.Latitude);
            MaxLat = points.Max(p => p.Latitude);
            MinLon = points.Min(p => p.Longitude);
            MaxLon = points.Max(p => p.Longitude);
        }
        public Box(double minLat, double maxLat, double minLon, double maxLon)
        {
            MinLat = minLat; 
            MaxLat = maxLat; 
            MinLon = minLon; 
            MaxLon = maxLon;
        }

        public bool Intersects(Box other) => 
            MinLat <= other.MaxLat && MaxLat >= other.MinLat && 
            MinLon <= other.MaxLon && MaxLon >= other.MinLon;
        
    }
    public class KdTree
    {
        private KdNode? _root;
        private const int MaxPointsInLeaf = 50;

        public void Build(List<Point> points) => _root = BuildRecursive(points, 0);
       

        private KdNode? BuildRecursive(List<Point> points, int depth)
        {
            if (points == null || points.Count == 0) return null;

            var box = new Box(points);
            var node = new KdNode(box);

            if (points.Count <= MaxPointsInLeaf)
            {
                node.Points = points;
                return node;
            }

            int axis = depth % 2; 

            var sortedPoints = axis == 0 ? points.OrderBy(p => p.Longitude).ToList() : points.OrderBy(p => p.Latitude).ToList();

            int median = sortedPoints.Count / 2;

            node.Left = BuildRecursive(sortedPoints.Take(median).ToList(), depth + 1);
            node.Right = BuildRecursive(sortedPoints.Skip(median).ToList(), depth + 1);

            return node;
        }

        public List<Point> SearchInRadius(Point target, double radiusKm)
        {
            var result = new List<Point>();
            if (_root == null) return result;

            double latDegreeInKm = 111.32;
            double lonDegreeInKm = 111.32 * Math.Cos(target.Latitude * Math.PI / 180.0);

            double deltaLat = radiusKm / latDegreeInKm;
            double deltaLon = radiusKm / lonDegreeInKm;

            var searchBox = new Box(
                target.Latitude - deltaLat,
                target.Latitude + deltaLat,
                target.Longitude - deltaLon,
                target.Longitude + deltaLon
            );

            SearchRecursive(_root, target, radiusKm, searchBox, result);
            return result;
        }

        private void SearchRecursive(KdNode node, Point target, double radiusKm, Box searchBox, List<Point> result)
        {
            if (!node.Box.Intersects(searchBox)) return;

            if (node.IsLeaf)
            {
                foreach (var p in node.Points!)
                {
                    if (Haversin.HaversinDistance(target, p) <= radiusKm)
                    {
                        result.Add(p);
                    }
                }
                return;
            }

            if (node.Left != null) SearchRecursive(node.Left, target, radiusKm, searchBox, result);
            if (node.Right != null) SearchRecursive(node.Right, target, radiusKm, searchBox, result);
        }
    }
}
