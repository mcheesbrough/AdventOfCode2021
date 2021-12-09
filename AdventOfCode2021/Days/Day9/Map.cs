using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day9
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;

        public Map(List<HeatMapPoint> points, int width, int height)
        {
            _width = width;
            _height = height;
            Points = points;
        }

        public List<HeatMapPoint> AdjacentPoints(HeatMapPoint point)
        {
            var adjacent = new List<HeatMapPoint>();
            if (point.Point.X - 1 >= 0) adjacent.Add(Points.First(p => p.Point.X == point.Point.X - 1 && p.Point.Y == point.Point.Y));
            if (point.Point.X + 1 < _width) adjacent.Add(Points.First(p => p.Point.X == point.Point.X + 1 && p.Point.Y == point.Point.Y));
            if (point.Point.Y - 1 >= 0) adjacent.Add(Points.First(p => p.Point.X == point.Point.X && p.Point.Y == point.Point.Y - 1));
            if (point.Point.Y + 1 < _height) adjacent.Add(Points.First(p => p.Point.X == point.Point.X && p.Point.Y == point.Point.Y + 1));
            return adjacent;
        }

        public List<HeatMapPoint> Points { get; }
    }
}