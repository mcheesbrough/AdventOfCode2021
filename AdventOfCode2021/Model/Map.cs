using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class Map<T> where T: IMapPoint, ICloneable
    {
        private readonly int _width;
        private readonly int _height;

        public Map(List<T> points, int width, int height)
        {
            _width = width;
            _height = height;
            Points = points;
        }

        public List<T> AdjacentPoints(T point)
        {
            var adjacent = new List<T>();
            if (point.Coordinate.X - 1 >= 0) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X - 1 && p.Coordinate.Y == point.Coordinate.Y));
            if (point.Coordinate.X + 1 < _width) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X + 1 && p.Coordinate.Y == point.Coordinate.Y));
            if (point.Coordinate.Y - 1 >= 0) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X && p.Coordinate.Y == point.Coordinate.Y - 1));
            if (point.Coordinate.Y + 1 < _height) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X && p.Coordinate.Y == point.Coordinate.Y + 1));
            return adjacent;
        }

        public List<T> AdjacentIncludingDiagonalPoints(T point)
        {
            var adjacent = new List<T>();
            if (point.Coordinate.X - 1 >= 0) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X - 1 && p.Coordinate.Y == point.Coordinate.Y));
            if (point.Coordinate.X + 1 < _width) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X + 1 && p.Coordinate.Y == point.Coordinate.Y));
            if (point.Coordinate.Y - 1 >= 0) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X && p.Coordinate.Y == point.Coordinate.Y - 1));
            if (point.Coordinate.Y + 1 < _height) adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X && p.Coordinate.Y == point.Coordinate.Y + 1));
            if (point.Coordinate.X - 1 >= 0 && point.Coordinate.Y - 1 >= 0) 
                adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X - 1 && p.Coordinate.Y == point.Coordinate.Y - 1));
            if (point.Coordinate.X + 1 < _width && point.Coordinate.Y + 1 < _height) 
                adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X + 1 && p.Coordinate.Y == point.Coordinate.Y + 1));
            if (point.Coordinate.Y - 1 >= 0 && point.Coordinate.X + 1 < _width) 
                adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X + 1 && p.Coordinate.Y == point.Coordinate.Y - 1));
            if (point.Coordinate.Y + 1 < _height && point.Coordinate.X - 1 >= 0) 
                adjacent.Add(Points.First(p => p.Coordinate.X == point.Coordinate.X - 1 && p.Coordinate.Y == point.Coordinate.Y + 1));
            return adjacent;
        }

        public Map<T> Clone()
        {
            var clonedPoints = Points.ConvertAll(p => p.Clone());
            return new Map<T>(Points.ToList(), _width, _height);
        }

        public List<T> Points { get; }
    }
}